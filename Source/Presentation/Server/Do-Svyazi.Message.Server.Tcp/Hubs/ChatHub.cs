using Do_Svyazi.Message.Application.CQRS.Messages.Queries;
using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using Do_Svyazi.Message.Application.Dto.Messages;
using Do_Svyazi.Message.Client.Tcp.Interfaces;
using Do_Svyazi.Message.Server.Tcp.Extensions;
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
        var user = Context.GetHttpContext().GetUserModel();

        var response = await _mediator.Send(new GetUserChatIds.Query(user.Id));

        IEnumerable<Task> tasks = response.ChatIds
            .Select(c => Groups.AddToGroupAsync(Context.ConnectionId, c.ToString()));

        await Task.WhenAll(tasks);
    }

    public async IAsyncEnumerable<MessageDto> GetMessages(Guid chatId, DateTime cursor, int count)
    {
        var user = Context.GetHttpContext().GetUserModel();

        var query = new GetChatMessages.Query(user.Id, chatId, cursor, count);

        var response = await _mediator.Send(query);
        foreach (var message in response.Messages)
        {
            yield return message;
        }
    }
}