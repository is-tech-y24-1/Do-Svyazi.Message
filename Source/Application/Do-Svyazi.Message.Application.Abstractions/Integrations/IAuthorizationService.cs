using Do_Svyazi.Message.Domain.Entities;

namespace Do_Svyazi.Message.Application.Abstractions.Integrations;

public interface IAuthorizationService
{
    Task AuthorizeMessageSendAsync(ChatUser chatUser, CancellationToken cancellationToken);
    Task AuthorizeMessageReadAsync(ChatUser chatUser, CancellationToken cancellationToken);
    Task AuthorizeMessageEditAsync(ChatUser chatUser, CancellationToken cancellationToken);
    Task AuthorizeMessageDeleteAsync(ChatUser chatUser, CancellationToken cancellationToken);
}