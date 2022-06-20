using Do_Svyazi.Message.Domain.Tools;
using RichEntity.Annotations;

namespace Do_Svyazi.Message.Domain.Entities;

public partial class Message : IEntity<Guid>
{
    private readonly List<Content> _contents;

    public Message(ChatUser sender, string text, DateTime postDateTime, IEnumerable<Content> contents)
        : this(Guid.NewGuid())
    {
        Sender = sender;
        Text = text;
        PostDateTime = postDateTime;
        _contents = contents.ToList();
    }

    public virtual ChatUser Sender { get; protected init; }

    public string Text { get; protected set; }

    public DateTime PostDateTime { get; protected init; }

    public virtual IReadOnlyCollection<Content> Contents => _contents.AsReadOnly();

    public void UpdateText(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        Text = text;
    }

    public void AddContent(Content content)
        => _contents.Add(content);

    public void RemoveContent(Content content)
    {
        if (!_contents.Remove(content))
            throw new MissingContentException(content, this);
    }
}