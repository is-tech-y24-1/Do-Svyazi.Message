using Do_Svyazi.Message.Application.CQRS.Chats.Queries;
using Do_Svyazi.Message.Application.Dto.Chats;
using Do_Svyazi.Message.Server.Http.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Do_Svyazi.Message.Server.Http.Controllers;

[Route("chats")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatController(IMediator mediator)
    {
        _mediator = mediator;
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
}