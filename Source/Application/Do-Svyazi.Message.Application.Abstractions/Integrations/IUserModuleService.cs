namespace Do_Svyazi.Message.Application.Abstractions.Integrations;

public interface IUserModuleService
{
    Task<bool> IsUserChatMemberAsync(Guid userId, Guid chatId, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Guid>> GetUserChatIdsAsync(Guid userId, CancellationToken cancellationToken);
}