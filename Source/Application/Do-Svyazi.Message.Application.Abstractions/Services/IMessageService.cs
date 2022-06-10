namespace Do_Svyazi.Message.Application.Abstractions.Services;

public interface IMessageService
{
    Task<Message.Domain.Entities.Message> GetMessageAsync(Guid id, CancellationToken cancellationToken);
    Task<Domain.Entities.Message> AuthorizeMessageToEditAsync(Guid userId, Guid messageId, CancellationToken cancellationToken);
}