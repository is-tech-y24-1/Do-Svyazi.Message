using Do_Svyazi.Message.Application.Abstractions.Exceptions.Unauthorized;
using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using Do_Svyazi.Message.Application.CQRS.Messages.Queries;
using Do_Svyazi.Message.Application.CQRS.Users.Queries;
using Do_Svyazi.Message.Application.Dto.Messages;
using Do_Svyazi.Message.Client.Tcp.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        var httpContext = Context.GetHttpContext();

        var user = GetUserModel(httpContext);

        var response = await _mediator.Send(new GetUserChatIds.Query(user.Id));

        IEnumerable<Task> tasks = response.ChatIds
            .Select(c => Groups.AddToGroupAsync(Context.ConnectionId, c.ToString()));

        await Task.WhenAll(tasks);
    }

    public async IAsyncEnumerable<MessageDto> GetMessages(Guid chatId, DateTime cursor, int count)
    {
        var httpContext = Context.GetHttpContext();

        var user = GetUserModel(httpContext);

        var query = new GetChatMessages.Query(user.Id, chatId, cursor, count);

        var response = await _mediator.Send(query);
        foreach (var message in response.Messages)
        {
            yield return message;
        }
    }

    private static UserModel GetUserModel(HttpContext? context)
    {
        if (context?.Items["User"] is UserModel userModel)
        {
            return userModel;
        }

        throw new UnauthenticatedException();
    }
}