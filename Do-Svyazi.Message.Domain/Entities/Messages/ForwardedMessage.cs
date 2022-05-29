namespace Do_Svyazi.Message.Domain.Entities;

public class ForwardedMessage : Message
{
    public Message Message { get; }

    public ForwardedMessage(
        ChatUser sender,
        string text,
        DateTime postDateTime,
        Message message) : base(sender, text, postDateTime)
    {
        Message = message;
    }
}