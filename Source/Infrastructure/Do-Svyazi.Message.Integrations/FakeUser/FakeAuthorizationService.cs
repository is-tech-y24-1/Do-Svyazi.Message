using Do_Svyazi.Message.Application.Abstractions.Integrations;
using Do_Svyazi.Message.Domain.Entities;

namespace Do_Svyazi.Message.Integrations.FakeUser;

public class FakeAuthorizationService : IAuthorizationService
{
    public Task AuthorizeMessageSendAsync(User user, Chat chat, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task AuthorizeMessageReadAsync(User user, Chat chat, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task AuthorizeMessageEditAsync(User user, Chat chat, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task AuthorizeMessageDeleteAsync(User user, Chat chat, CancellationToken cancellationToken)
        => Task.CompletedTask;
}