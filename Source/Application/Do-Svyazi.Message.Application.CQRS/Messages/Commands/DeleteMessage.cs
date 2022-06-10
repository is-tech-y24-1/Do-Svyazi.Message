using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Do_Svyazi.Message.Application.Abstractions.Exceptions.NotFound;
using Do_Svyazi.Message.Application.Abstractions.Integrations;
using Do_Svyazi.Message.Application.Abstractions.Services;
using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Commands;

public static class DeleteMessage
{
    public record Command(Guid UserId, Guid MessageId) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly IMessageDatabaseContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;

        public Handler(
            IMessageDatabaseContext context,
            IAuthorizationService authorizationService,
            IUserService userService,
            IMessageService messageService)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userService = userService;
            _messageService = messageService;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var (userId, messageId) = request;

            var user = await _userService
                .GetUserAsync(userId, cancellationToken)
                .ConfigureAwait(false);

            var message = await _messageService
                .GetMessageAsync(messageId, cancellationToken)
                .ConfigureAwait(false);

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