using Do_Svyazi.Message.Application.Dto.Messages;
using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Queries;

public static class GetChatMessages
{
    public record Query(Guid UserId, Guid ChatId, DateTime Cursor, int Count) : IRequest<Response>;

    public record Response(IReadOnlyCollection<MessageDto> Messages, DateTime Cursor);

    // public class Handler : IRequestHandler<Query, Response>
    // {
    //     public async Task<Response> Handle(Query request, CancellationToken cancellationToken) { }
    // }
}