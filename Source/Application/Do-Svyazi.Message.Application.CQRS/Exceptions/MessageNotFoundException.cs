using Do_Svyazi.Message.Application.Abstractions.Exceptions;

namespace Do_Svyazi.Message.Application.CQRS.Exceptions;

public class MessageNotFoundException : NotFoundException
{
    public MessageNotFoundException(Guid messageId)
        : base($"Message with id {messageId} was not found.") { }
}