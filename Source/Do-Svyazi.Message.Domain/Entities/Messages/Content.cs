namespace Do_Svyazi.Message.Domain.Entities;

public class Content
{
    public Content(Uri uri, ContentType type)
    {
        Id = Guid.NewGuid();
        Uri = uri;
        Type = type;
    }

#pragma warning disable CS8618
    protected Content() { }
#pragma warning restore CS8618

    public Guid Id { get; protected init; }
    public Uri Uri { get; protected init; }
    public ContentType Type { get; protected init; }
}