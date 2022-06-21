using Do_Svyazi.Message.Application.CQRS.Chats.Queries;
using Do_Svyazi.Message.Application.CQRS.Messages.Commands;
using Do_Svyazi.Message.Application.CQRS.Messages.Queries;
using Do_Svyazi.Message.Application.Dto.Chats;
using Do_Svyazi.Message.Application.Dto.Messages;
using Do_Svyazi.Message.Client.Tcp.Interfaces;
using Do_Svyazi.Message.Server.Http.Extensions;
using Do_Svyazi.Message.Server.Tcp.Hubs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Http.Controllers;

[Route("chats")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHubContext<ChatHub, IChatClient> _context;

    public ChatController(IMediator mediator, IHubContext<ChatHub, IChatClient> context)
    {
        _mediator = mediator;
        _context = context;
    }

    [HttpGet("{chatId}/messages/unread")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> GetCountOfUnreadMessages([FromRoute] Guid chatId)
    {
        var user = HttpContext.GetUserModel();
        var query = new GetChatUserState.Query(user.Id, chatId);
        var response = await _mediator.Send(query, HttpContext.RequestAborted);

        return Ok(response.ChatUserState.UnreadMessageCount);
    }

    [HttpGet("{chatId}/state")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ChatUserStateDto>> GetChatState([FromRoute] Guid chatId)
    {
        var user = HttpContext.GetUserModel();
        var query = new GetChatUserState.Query(user.Id, chatId);
        var response = await _mediator.Send(query, HttpContext.RequestAborted);

        return Ok(response.ChatUserState);
    }

    [HttpDelete("{chatId}/messages/{messageId}/delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<OkResult> DeleteMessage([FromRoute] Guid chatId, [FromRoute] Guid messageId)
    {
        var user = HttpContext.GetUserModel();
        var command = new DeleteMessage.Command(user.Id, messageId);
        await _mediator.Send(command, HttpContext.RequestAborted);

        await _context.Clients.Group(chatId.ToString()).OnMessageDeleted(messageId);
        return Ok();
    }

    [HttpGet("{chatId}/messages/{messageId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MessageDto>> GetMessage([FromRoute] Guid chatId, [FromRoute] Guid messageId)
    {
        var user = HttpContext.GetUserModel();
        var query = new GetMessage.Query(user.Id, messageId);
        var response = await _mediator.Send(query, HttpContext.RequestAborted);
        
        await _context.Clients.Group(chatId.ToString()).OnMessageReceived(response.Message);

        return Ok(response.Message);
    }

    [HttpPost("{chatId}/messages/send")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MessageDto>> AddMessage(
        [FromRoute] Guid chatId,
        string text,
        DateTime postDateTime,
        IReadOnlyCollection<ContentDto> contents)
    {
        var user = HttpContext.GetUserModel();
        var command = new AddMessage.Command(user.Id, chatId, text, postDateTime, contents);
        var response = await _mediator.Send(command, HttpContext.RequestAborted);
        
        await _context.Clients.Group(chatId.ToString()).OnMessageReceived(response.Message);
        return Ok(response.Message);
    }
    
    [HttpPut("{chatId}/messages/{messageId}/update-text")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<OkResult> UpdateMessageText([FromRoute] Guid chatId, [FromRoute] Guid messageId, string text)
    {
        var user = HttpContext.GetUserModel();
        var command = new UpdateMessage.Command(user.Id, messageId, text);
        await _mediator.Send(command, HttpContext.RequestAborted);
        
        await _context.Clients.Group(chatId.ToString()).OnMessageUpdated(messageId);
        return Ok();
    }
    
    [HttpPut("{chatId}/messages/{messageId}/update-content")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<OkResult> UpdateMessageContent([FromRoute] Guid chatId,
        [FromRoute] Guid messageId, 
        IReadOnlyCollection<ContentDto> addedContents,
        IReadOnlyCollection<MessageContentDto> removedContents)
    {
        var user = HttpContext.GetUserModel();
        var query = new GetMessage.Query(user.Id, messageId);
        await _mediator.Send(query, HttpContext.RequestAborted);
        
        var command = new UpdateMessageContent.Command(user.Id, messageId, addedContents, removedContents);
        await _mediator.Send(command, HttpContext.RequestAborted);

        await _context.Clients.Group(chatId.ToString()).OnMessageUpdated(messageId);
        return Ok();
    }

    [HttpPost("{chatId}/messages/send-forwarded")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MessageDto>> AddForwardedMessage([FromRoute] Guid chatId,
        Guid messageId,
        string text,
        DateTime postDateTime,
        IReadOnlyCollection<ContentDto> contents)
    {
        var user = HttpContext.GetUserModel();
        var command = new AddForwardedMessage.Command(user.Id, chatId, messageId, text, postDateTime, contents);
        var response = await _mediator.Send(command, HttpContext.RequestAborted);
        
        await _context.Clients.Group(chatId.ToString()).OnMessageReceived(response.Message);
        return Ok(response.Message);
    }
}