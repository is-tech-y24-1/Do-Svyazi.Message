using System.Security.Claims;
using Do_Svyazi.Message.Application.CQRS.Chats.Queries;
using Do_Svyazi.Message.Application.CQRS.Messages.Queries;
using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using Do_Svyazi.Message.Application.Dto.Messages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Hubs;

[Authorize]
public class ChatHub : Hub
{
    private readonly IMediator _mediator;

    public override async Task OnConnectedAsync()
    {
        var userIdClaim = Context.User?.Claims
            .Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value)
            .First();

        var chatIds = await _mediator.Send(new GetUserChatIds.Query(Guid.Parse(userIdClaim)));

        foreach (var chatId in chatIds.ChatIds)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        await base.OnConnectedAsync();
    }

    public async IAsyncEnumerable<MessageDto> GetMessages(Guid groupId)
    {
        var userIdClaim = Context.User?.Claims
            .Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value)
            .First();

        var userId = Guid.Parse(userIdClaim);

        var userChatState = await _mediator.Send(new GetChatUserState.Query(userId, groupId));

        var cursor = userChatState.ChatUserState.LastMessage.PostDateTime;
        var amountOfUnread = userChatState.ChatUserState.UnreadMessageCount;

        var messages = await _mediator.Send(new GetChatMessages.Query(userId, groupId, cursor, amountOfUnread));
        foreach (var message in messages.Messages)
        {
            yield return message;
        }
    }
}