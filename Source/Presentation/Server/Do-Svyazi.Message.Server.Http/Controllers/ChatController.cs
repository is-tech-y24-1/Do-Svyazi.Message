﻿using Do_Svyazi.Message.Application.CQRS.Chats.Queries;
using Do_Svyazi.Message.Application.Dto.Chats;
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
    public async Task<ActionResult<int>> GetCountOfUnreadMessages(Guid userId, Guid chatId)
    {
        var response = await _mediator.Send(new GetChatUserState.Query(userId, chatId));

        return Ok(response.ChatUserState.UnreadMessageCount);
    }
    
    [HttpGet("{chatId}/state")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ChatUserStateDto>> GetChatState(Guid userId, Guid chatId)
    {
        var response = await _mediator.Send(new GetChatUserState.Query(userId, chatId));

        return Ok(response.ChatUserState);
    }
}