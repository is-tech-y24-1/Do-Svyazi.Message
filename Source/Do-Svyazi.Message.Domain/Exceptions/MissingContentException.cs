using Do_Svyazi.Message.Domain.Entities;

namespace Do_Svyazi.Message.Domain.Tools;

public class MissingContentException : DomainException
{
    public MissingContentException(Content content, Entities.Message message)
        : base($"Message {message} does not contain a content {content}") { }
}