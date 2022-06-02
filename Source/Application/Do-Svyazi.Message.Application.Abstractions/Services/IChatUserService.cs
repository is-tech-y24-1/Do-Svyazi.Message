using Do_Svyazi.Message.Domain.Entities;

namespace Do_Svyazi.Message.Application.Abstractions.Services;

public interface IChatUserService
{
    Task<ChatUser> GetChatUser(Guid chatId, Guid userId, CancellationToken cancellationToken);
}