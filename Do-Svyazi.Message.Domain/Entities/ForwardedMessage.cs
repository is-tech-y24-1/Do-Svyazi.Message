namespace Do_Svyazi.Message.Domain.Entities;

public class ForwardedMessage : Message
{
    private Guid MessageId { get; }

    public ForwardedMessage(Guid id, Guid userId, Guid chatId, string text, DateTime postDateTime, Content[] content) : base(id, userId, chatId, text, postDateTime, content)
    {
    }
}