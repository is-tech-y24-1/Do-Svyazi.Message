using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Commands;

public static class UpdateMessage
{
    public record Command(Guid UserId, Guid MessageId, string NewText) : IRequest;

    // public class Handler : IRequestHandler<Command>
    // {
    //     public async Task<Unit> Handle(Command request, CancellationToken cancellationToken) { }
    // }
}