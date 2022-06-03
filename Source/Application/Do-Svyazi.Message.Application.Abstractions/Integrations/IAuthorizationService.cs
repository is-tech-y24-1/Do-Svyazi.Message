using Do_Svyazi.Message.Domain.Entities;

namespace Do_Svyazi.Message.Application.Abstractions.Integrations;

public interface IAuthorizationService
{
    Task AuthorizeMessageSendAsync(User user, Chat chat, CancellationToken cancellationToken);
    Task AuthorizeMessageReadAsync(User user, Chat chat, CancellationToken cancellationToken);
    Task AuthorizeMessageEditAsync(User user, Chat chat, CancellationToken cancellationToken);
    Task AuthorizeMessageDeleteAsync(User user, Chat chat, CancellationToken cancellationToken);
}