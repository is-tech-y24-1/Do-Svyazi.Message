namespace Do_Svyazi.Message.Domain.Entities;

public class Content
{
    public Guid Id { get; }
    public Uri Uri { get; }
    public ContentType Type { get; }

    public Content(Uri uri, ContentType type)
    {
        Id = Guid.NewGuid();
        Uri = uri;
        Type = type;
    }
}