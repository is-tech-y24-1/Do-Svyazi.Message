using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Do_Svyazi.Message.Application.Abstractions.Exceptions.NotFound;
using Do_Svyazi.Message.Application.Abstractions.Integrations;
using Do_Svyazi.Message.Application.Abstractions.Services;

namespace Do_Svyazi.Message.Services;

public class MessageService : IMessageService
{
    private readonly IMessageDatabaseContext _context;
    private readonly IAuthorizationService _authorizationService;

    public MessageService(IMessageDatabaseContext context, IAuthorizationService authorizationService)
    {
        _context = context;
        _authorizationService = authorizationService;
    }


    public async Task<Domain.Entities.Message> AuthorizeMessageToEdit(Guid userId, Guid messageId, CancellationToken cancellationToken)
    {
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
                .AuthorizeMessageEditAsync(user, message.Sender.Chat, cancellationToken)
                .ConfigureAwait(false);
        }

        return message;
    }
}