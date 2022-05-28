namespace Do_Svyazi.Message.Domain.Entities;

public class ForwardedMessage : Message
{
    public Message Message { get; }

    protected ForwardedMessage(User sender, Chat chat, string text, DateTime postDateTime,
        Message message) : base(sender, chat, text, postDateTime)
    {
        Message = message;
    }
}