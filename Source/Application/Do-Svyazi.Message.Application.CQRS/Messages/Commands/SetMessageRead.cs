using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Do_Svyazi.Message.Application.Abstractions.Exceptions.InvalidRequest;
using Do_Svyazi.Message.Application.Abstractions.Exceptions.NotFound;
using Do_Svyazi.Message.Application.Abstractions.Services;
using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Commands;

public static class SetMessageRead
{
    public record Command(Guid UserId, Guid ChatId, Guid MessageId) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly IMessageDatabaseContext _context;
        private readonly IChatUserService _chatUserService;
        private readonly IMessageService _messageService;

        public Handler(IMessageDatabaseContext context, IChatUserService chatUserService, IMessageService messageService)
        {
            _context = context;
            _chatUserService = chatUserService;
            _messageService = messageService;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var (userId, chatId, messageId) = request;

            var chatUser = await _chatUserService
                .GetChatUser(chatId, userId, cancellationToken)
                .ConfigureAwait(false);

            var message = await _messageService
                .GetMessageAsync(messageId, cancellationToken)
                .ConfigureAwait(false);

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