namespace Do_Svyazi.Message.Application.Abstractions.Exceptions.NotFound;

public class MessageNotFoundException : NotFoundException
{
    public MessageNotFoundException(Guid messageId)
        : base($"Message with id {messageId} was not found.") { }
}