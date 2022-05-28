using Do_Svyazi.Message.Domain.Tools;

namespace Do_Svyazi.Message.Domain.Entities;

public class Message
{
    private List<Content> _content;

    public Message(User sender, Chat chat, string text, DateTime postDateTime)
    {
        Id = Guid.NewGuid();
        Sender = sender;
        Chat = chat;
        Text = text;
        PostDateTime = postDateTime;
    }

    public Guid Id { get; }
    public User Sender { get; }
    public Chat Chat { get; }
    public string Text { get; }
    public DateTime PostDateTime { get; }
    public IReadOnlyCollection<Content> Content => _content;

    public void AddContent(Content newContent)
    {
        _content.Add(newContent);
    }

    public void RemoveContent(Content removableContent)
    {
        if (removableContent is null)
        {
            throw new DomainException("No content to remove");
        }

        if (!_content.Remove(removableContent))
        {
            throw new DomainException("No such content to delete");
        }
    }
}