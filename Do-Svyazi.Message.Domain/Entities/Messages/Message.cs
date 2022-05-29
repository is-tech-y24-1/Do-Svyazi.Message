using Do_Svyazi.Message.Domain.Tools;

namespace Do_Svyazi.Message.Domain.Entities;

public class Message
{
    private List<Content> _contents;

    public Message(ChatUser sender, string text, DateTime postDateTime)
    {
        Id = Guid.NewGuid();
        Sender = sender;
        Text = text;
        PostDateTime = postDateTime;
        _contents = new List<Content>();
    }

    public Guid Id { get; }
    public ChatUser Sender { get; }
    public string Text { get; }
    public DateTime PostDateTime { get; }
    public IReadOnlyCollection<Content> Contents => _contents.AsReadOnly();

    public void AddContent(Content newContent)
    {
        _contents.Add(newContent);
    }

    public void RemoveContent(Content removableContent)
    {
        if (removableContent is null)
        {
            throw new DomainException("No content to remove");
        }

        if (!_contents.Remove(removableContent))
        {
            throw new DomainException("No such content to delete");
        }
    }
}