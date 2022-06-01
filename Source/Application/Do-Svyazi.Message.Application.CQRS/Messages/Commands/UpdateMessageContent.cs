using Do_Svyazi.Message.Application.Dto.Messages;
using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Commands;

public static class UpdateMessageContent
{
    public record Command(Guid UserId, Guid MessageId, IReadOnlyCollection<ContentDto> NewContent) : IRequest;

    // public class Handler : IRequestHandler<Command>
    // {
    //     public async Task<Unit> Handle(Command request, CancellationToken cancellationToken) { }
    // }
}