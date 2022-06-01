namespace Do_Svyazi.Message.Domain.Entities;

public class ForwardedMessage : Message
{
    public ForwardedMessage(
        ChatUser sender,
        string text,
        DateTime postDateTime,
        Message message) : base(sender, text, postDateTime)
    {
        Message = message;
    }

#pragma warning disable CS8618
    protected ForwardedMessage() { }
#pragma warning restore CS8618

    public virtual Message Message { get; protected init; }
}