namespace Do_Svyazi.Message.Domain.Entities;

public class ForwardedMessage : Message
{
    public Message Message { get; }

    protected ForwardedMessage(User user, Chat chat, string text, DateTime postDateTime, Content[] content,
        Message message) : base(user, chat, text, postDateTime, content)
    {
        Message = message;
    }
}