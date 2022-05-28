namespace Do_Svyazi.Message.Domain.Entities;

public class Message
{
    public Guid Id { get; }
    public User User { get; }
    public Chat Chat { get; }
    public string Text { get; }
    public DateTime PostDateTime { get; }
    public Content[] Content { get; }

    protected Message(User user, Chat chat, string text, DateTime postDateTime, Content[] content)
    {
        Id = Guid.NewGuid();
        User = user;
        Chat = chat;
        Text = text;
        PostDateTime = postDateTime;
        Content = content;
    }
}