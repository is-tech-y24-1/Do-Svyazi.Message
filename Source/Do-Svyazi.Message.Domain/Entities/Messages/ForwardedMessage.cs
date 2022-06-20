namespace Do_Svyazi.Message.Domain.Entities;

public partial class ForwardedMessage : Message
{
    public ForwardedMessage(
        ChatUser sender,
        string text,
        DateTime postDateTime,
        IEnumerable<Content> contents,
        Message message) : base(sender, text, postDateTime, contents)
    {
        Message = message;
    }

    public virtual Message Message { get; protected init; }
}