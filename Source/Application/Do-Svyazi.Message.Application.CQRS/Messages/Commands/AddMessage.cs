using Do_Svyazi.Message.Application.Dto.Messages;
using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Commands;

public static class AddMessage
{
    public record Command(
        Guid UserId,
        Guid ChatId,
        string Text,
        DateTime PostDateTime,
        IReadOnlyCollection<ContentDto> Contents) : IRequest<Response>;

    public record Response(MessageDto Message);

    // public class Handler : IRequestHandler<Command, Response>
    // {
    //     public async Task<Response> Handle(Command request, CancellationToken cancellationToken) { }
    // }
}