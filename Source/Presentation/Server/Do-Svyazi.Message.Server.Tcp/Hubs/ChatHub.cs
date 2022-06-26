using Do_Svyazi.Message.Application.CQRS.Messages.Queries;
using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using Do_Svyazi.Message.Application.Dto.Messages;
using Do_Svyazi.Message.Sdk.Tcp.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Hubs;

[Authorize]
public class ChatHub : Hub<IChatClient>
{
    private readonly IMediator _mediator;

    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task OnConnectedAsync()
    {
        if (Context.UserIdentifier is null) 
            return;

        var userId = Guid.Parse(Context.UserIdentifier);

        var query = new GetUserChatIds.Query(userId);
        var response = await _mediator.Send(query);

        IEnumerable<Task> tasks = response.ChatIds
            .Select(c => Groups.AddToGroupAsync(Context.ConnectionId, c.ToString()));

        await Task.WhenAll(tasks);
    }

    public async IAsyncEnumerable<MessageDto> GetMessages(Guid chatId, DateTime cursor, int count)
    {
        if (Context.UserIdentifier is null) 
            yield break;

        var userId = Guid.Parse(Context.UserIdentifier);

        var query = new GetChatMessages.Query(userId, chatId, cursor, count);
        var response = await _mediator.Send(query);

        foreach (var message in response.Messages)
        {
            yield return message;
        }
    }
}