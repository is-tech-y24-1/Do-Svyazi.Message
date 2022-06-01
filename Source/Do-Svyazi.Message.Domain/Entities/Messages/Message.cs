namespace Do_Svyazi.Message.Domain.Entities;

public class Message
{
    private readonly List<Content> _contents;

    public Message(ChatUser sender, string text, DateTime postDateTime, IEnumerable<Content> contents)
    {
        Id = Guid.NewGuid();
        Sender = sender;
        Text = text;
        PostDateTime = postDateTime;
        _contents = contents.ToList();
    }

#pragma warning disable CS8618
    protected Message() { }
#pragma warning restore CS8618

    public Guid Id { get; protected init; }
    public virtual ChatUser Sender { get; protected init; }
    public string Text { get; protected set; }
    public DateTime PostDateTime { get; protected init; }
    public virtual IReadOnlyCollection<Content> Contents => _contents.AsReadOnly();

    public void UpdateText(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        
        Text = text;
    }

    public void UpdateContent(IEnumerable<Content> contents)
    {
        ArgumentNullException.ThrowIfNull(contents);

        _contents.Clear();
        _contents.AddRange(contents);
    }
}