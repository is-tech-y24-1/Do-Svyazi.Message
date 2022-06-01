using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Do_Svyazi.Message.Application.Abstractions.Exceptions.NotFound;
using Do_Svyazi.Message.Application.Abstractions.Integrations;
using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Commands;

public static class DeleteMessage
{
    public record Command(Guid UserId, Guid MessageId) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly IMessageDatabaseContext _context;
        private readonly IAuthorizationService _authorizationService;

        public Handler(IMessageDatabaseContext context, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var (userId, messageId) = request;

            var user = await _context.Users
                .FindAsync(new object[] { userId }, cancellationToken)
                .ConfigureAwait(false);

            if (user is null)
                throw new UserNotFoundException(userId);

            var message = await _context.Messages
                .FindAsync(new object[] { messageId }, cancellationToken)
                .ConfigureAwait(false);
            
            if (message is null)
                throw new MessageNotFoundException(messageId);

            if (!message.Sender.User.Equals(user))
            {
                await _authorizationService
                    .AuthorizeMessageDeleteAsync(user, message.Sender.Chat, cancellationToken)
                    .ConfigureAwait(false);
            }
            
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            
            return Unit.Value;
        }
    }
}