using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Do_Svyazi.Message.Application.Abstractions.Services;
using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Commands;

public static class UpdateMessage
{
    public record Command(Guid UserId, Guid MessageId, string NewText) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly IMessageDatabaseContext _context;
        private readonly IMessageService _messageService;

        public Handler(IMessageDatabaseContext context, IMessageService messageService)
        {
            _context = context;
            _messageService = messageService;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var (userId, messageId, newText) = request;

            var message = await _messageService
                .AuthorizeMessageToEditAsync(userId, messageId, cancellationToken)
                .ConfigureAwait(false);

            message.UpdateText(newText);
            _context.Messages.Update(message);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}