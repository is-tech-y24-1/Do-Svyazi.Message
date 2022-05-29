namespace Do_Svyazi.Message.Domain.Entities;

public class ForwardedMessage : Message
{
    public Message Message { get; }

    public ForwardedMessage(
        ChatUser sender,
        string text,
        DateTime postDateTime,
        List<Content> contents,
        Message message) : base(sender, text, postDateTime, contents)
    {
        Message = message;
    }
}