using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Commands;

public static class SetMessageRead
{
    public record Command(Guid UserId, Guid ChatId, Guid MessageId) : IRequest;

    // public class Handler : IRequestHandler<Command>
    // {
    //     public async Task<Unit> Handle(Command request, CancellationToken cancellationToken) { }
    // }
}