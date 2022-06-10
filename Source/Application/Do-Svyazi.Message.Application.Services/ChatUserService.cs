using System.Linq.Expressions;
using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Do_Svyazi.Message.Application.Abstractions.Exceptions.Unauthorized;
using Do_Svyazi.Message.Application.Abstractions.Integrations;
using Do_Svyazi.Message.Application.Abstractions.Services;
using Do_Svyazi.Message.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Do_Svyazi.Message.Application.Services;

public class ChatUserService : IChatUserService
{
    private readonly IMessageDatabaseContext _context;
    private readonly IUserService _userService;
    private readonly IUserModuleService _userModuleService;

    public ChatUserService(IMessageDatabaseContext context, IUserModuleService userModuleService, IUserService userService)
    {
        _context = context;
        _userModuleService = userModuleService;
        _userService = userService;
    }

    public async Task<ChatUser> GetChatUser(Guid chatId, Guid userId, CancellationToken cancellationToken)
    {
        Expression<Func<ChatUser, bool>> ChatUserEqualityExpression()
            => c => c.Chat.Id.Equals(chatId) && c.User.Id.Equals(userId);

        var chatUser = await _context.ChatUsers
            .SingleOrDefaultAsync(ChatUserEqualityExpression(), cancellationToken)
            .ConfigureAwait(false);

        if (chatUser is not null)
            return chatUser;

        var isUserChatMember = await _userModuleService
            .IsUserChatMemberAsync(userId, chatId, cancellationToken)
            .ConfigureAwait(false);

        if (!isUserChatMember)
            throw new UnauthorizedChatUserException(userId, chatId);

        var user = await _userService.GetUserAsync(userId, cancellationToken).ConfigureAwait(false);

        var chat = await _context.Chats
            .FindAsync(new object[] { chatId }, cancellationToken)
            .ConfigureAwait(false);

        if (chat is null)
        {
            chat = new Chat(chatId);
            _context.Chats.Add(chat);
        }
        
        chatUser = new ChatUser(user, chat);
        _context.ChatUsers.Add(chatUser);

        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        return chatUser;
    }
}