using Do_Svyazi.Message.Application.CQRS.Messages.Queries;
using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using Do_Svyazi.Message.Application.Dto.Messages;
using Do_Svyazi.Message.Server.Tcp.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Hubs;

[Authorize]
public class ChatHub : Hub
{
    private readonly IMediator _mediator;

    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task OnConnectedAsync()
    {
        var user = Context.GetHttpContext().GetUserModel();

        var chatIds = await _mediator.Send(new GetUserChatIds.Query(user.Id));

        IEnumerable<Task> tasks = chatIds.ChatIds
            .Select(c => Groups.AddToGroupAsync(Context.ConnectionId, c.ToString()));

        await Task.WhenAll(tasks);
    }

    public async IAsyncEnumerable<MessageDto> GetMessages(Guid groupId, DateTime cursor, int count)
    {
        var user = Context.GetHttpContext().GetUserModel();

        var getChatMessagesQuery = new GetChatMessages.Query(user.Id, groupId, cursor, count);

        var messages = await _mediator.Send(getChatMessagesQuery);
        foreach (var message in messages.Messages)
        {
            yield return message;
        }
    }
}