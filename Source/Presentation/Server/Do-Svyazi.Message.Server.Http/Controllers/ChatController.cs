using System.Security.Claims;
using Do_Svyazi.Message.Application.CQRS.Chats.Queries;
using Do_Svyazi.Message.Application.CQRS.Messages.Commands;
using Do_Svyazi.Message.Application.CQRS.Messages.Queries;
using Do_Svyazi.Message.Application.Dto.Chats;
using Do_Svyazi.Message.Application.Dto.Messages;
using Do_Svyazi.Message.Sdk.Tcp.Interfaces;
using Do_Svyazi.Message.Server.Http.Models;
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

    private Guid UserId => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [HttpGet("{chatId}/messages/unread")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> GetCountOfUnreadMessages([FromRoute] Guid chatId)
    {
        var query = new GetChatUserState.Query(UserId, chatId);
        var response = await _mediator.Send(query, HttpContext.RequestAborted);

        return Ok(response.ChatUserState.UnreadMessageCount);
    }

    [HttpGet("{chatId}/state")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ChatUserStateDto>> GetChatState([FromRoute] Guid chatId)
    {
        var query = new GetChatUserState.Query(UserId, chatId);
        var response = await _mediator.Send(query, HttpContext.RequestAborted);

        return Ok(response.ChatUserState);
    }

    [HttpDelete("{chatId}/messages/{messageId}/delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<OkResult> DeleteMessage([FromRoute] Guid chatId, [FromRoute] Guid messageId)
    {
        var command = new DeleteMessage.Command(UserId, messageId);
        await _mediator.Send(command, HttpContext.RequestAborted);

        await _context.Clients.Group(chatId.ToString()).OnMessageDeleted(messageId);
        return Ok();
    }

    [HttpGet("{chatId}/messages/{messageId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MessageDto>> GetMessage([FromRoute] Guid chatId, [FromRoute] Guid messageId)
    {
        var query = new GetMessage.Query(UserId, messageId);
        var response = await _mediator.Send(query, HttpContext.RequestAborted);
        
        await _context.Clients.Group(chatId.ToString()).OnMessageReceived(response.Message);

        return Ok(response.Message);
    }

    [HttpPost("{chatId}/messages/send")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MessageDto>> AddMessage(
        [FromRoute] Guid chatId,
        string text,
        IReadOnlyCollection<ContentDto> contents)
    {
        var command = new AddMessage.Command(UserId, chatId, text, contents);
        var response = await _mediator.Send(command, HttpContext.RequestAborted);
        
        await _context.Clients.Group(chatId.ToString()).OnMessageReceived(response.Message);
        return Ok(response.Message);
    }
    
    [HttpPut("{chatId}/messages/{messageId}/update-text")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<OkResult> UpdateMessageText([FromRoute] Guid chatId, [FromRoute] Guid messageId, string text)
    {
        var command = new UpdateMessage.Command(UserId, messageId, text);
        await _mediator.Send(command, HttpContext.RequestAborted);
        
        await _context.Clients.Group(chatId.ToString()).OnMessageUpdated(messageId);
        return Ok();
    }

    [HttpPut("{chatId}/messages/{messageId}/update-content")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<OkResult> UpdateMessageContent(
        [FromRoute] Guid chatId,
        [FromRoute] Guid messageId,
        UpdateMessageContentRequest request)
    {
        var query = new GetMessage.Query(UserId, messageId);
        await _mediator.Send(query, HttpContext.RequestAborted);
        
        var command = new UpdateMessageContent.Command(UserId, messageId, request.AddedContents, request.RemovedContents);
        await _mediator.Send(command, HttpContext.RequestAborted);

        await _context.Clients.Group(chatId.ToString()).OnMessageUpdated(messageId);
        return Ok();
    }

    [HttpPost("{chatId}/messages/send-forwarded")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MessageDto>> AddForwardedMessage([FromRoute] Guid chatId,
        Guid messageId,
        string text,
        IReadOnlyCollection<ContentDto> contents)
    {
        var command = new AddForwardedMessage.Command(UserId, chatId, messageId, text, contents);
        var response = await _mediator.Send(command, HttpContext.RequestAborted);
        
        await _context.Clients.Group(chatId.ToString()).OnMessageReceived(response.Message);
        return Ok(response.Message);
    }
}