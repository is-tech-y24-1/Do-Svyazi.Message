using Do_Svyazi.Message.Application.Abstractions.Exceptions;

namespace Do_Svyazi.Message.Application.CQRS.Exceptions;

public class ForeignMessageException : InvalidRequestException
{
    public ForeignMessageException(Guid chatId, Guid messageId)
        : base($"Message with id {messageId} does not belong to chat with id {chatId}") { }
}