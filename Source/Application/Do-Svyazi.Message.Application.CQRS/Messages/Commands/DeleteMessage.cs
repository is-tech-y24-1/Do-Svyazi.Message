using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Commands;

public static class DeleteMessage
{
    public record Command(Guid UserId, Guid MessageId) : IRequest;

    // public class Handler : IRequestHandler<Command>
    // {
    //     public async Task<Unit> Handle(Command request, CancellationToken cancellationToken) { }
    // }
}