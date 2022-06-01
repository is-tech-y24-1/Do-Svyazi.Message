using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Do_Svyazi.Message.Application.Abstractions.EntityManagers;
using Do_Svyazi.Message.Application.CQRS.Exceptions;
using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Commands;

public static class SetMessageRead
{
    public record Command(Guid UserId, Guid ChatId, Guid MessageId) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly IMessageDatabaseContext _context;
        private readonly IChatUserManager _chatUserManager;

        public Handler(IMessageDatabaseContext context, IChatUserManager chatUserManager)
        {
            _context = context;
            _chatUserManager = chatUserManager;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var (userId, chatId, messageId) = request;

            var chatUser = await _chatUserManager
                .GetChatUser(chatId, userId, cancellationToken)
                .ConfigureAwait(false);

            var message = await _context.Messages
                .FindAsync(new object[] { messageId }, cancellationToken)
                .ConfigureAwait(false);
            
            if (message is null)
                throw new MessageNotFoundException(messageId);

            if (!message.Sender.Chat.Equals(chatUser.Chat))
                throw new ForeignMessageException(chatId, messageId);
            
            if (chatUser.LastReadMessage is not null &&
                chatUser.LastReadMessage.PostDateTime >= message.PostDateTime)
                return Unit.Value;
            
            chatUser.LastReadMessage = message;
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            
            return Unit.Value;
        }
    }
}