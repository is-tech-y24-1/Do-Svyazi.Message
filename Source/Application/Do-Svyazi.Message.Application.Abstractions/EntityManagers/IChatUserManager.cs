using Do_Svyazi.Message.Domain.Entities;

namespace Do_Svyazi.Message.Application.Abstractions.EntityManagers;

public interface IChatUserManager
{
    Task<ChatUser> GetChatUser(Guid chatId, Guid userId, CancellationToken cancellationToken);
}