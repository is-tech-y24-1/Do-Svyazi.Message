namespace Do_Svyazi.Message.Application.Abstractions.Services;

public interface IMessageService
{
    Task<Domain.Entities.Message> AuthorizeMessageToEdit(Guid userId, Guid messageId, CancellationToken cancellationToken);
}