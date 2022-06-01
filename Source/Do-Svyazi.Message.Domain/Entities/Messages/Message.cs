using Do_Svyazi.Message.Domain.Tools;

namespace Do_Svyazi.Message.Domain.Entities;

public class Message
{
    private readonly List<Content> _contents;

    public Message(ChatUser sender, string text, DateTime postDateTime)
    {
        Id = Guid.NewGuid();
        Sender = sender;
        Text = text;
        PostDateTime = postDateTime;
        _contents = new List<Content>();
    }

#pragma warning disable CS8618
    protected Message() { }
#pragma warning restore CS8618

    public Guid Id { get; protected init; }
    public virtual ChatUser Sender { get; protected init; }
    public string Text { get; protected init; }
    public DateTime PostDateTime { get; protected init; }
    public virtual IReadOnlyCollection<Content> Contents => _contents.AsReadOnly();

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