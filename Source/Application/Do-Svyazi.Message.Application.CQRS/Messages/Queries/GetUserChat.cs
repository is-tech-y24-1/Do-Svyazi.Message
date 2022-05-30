using Do_Svyazi.Message.Application.Dto.Chats;
using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Queries;

public static class GetUserChat
{
    public record Query(Guid UserId, Guid ChatId) : IRequest<Response>;

    public record Response(ChatUserDto ChatUser);

    // public class Handler : IRequestHandler<Query, Response>
    // {
    //     public async Task<Response> Handle(Query request, CancellationToken cancellationToken) { }
    // }
}