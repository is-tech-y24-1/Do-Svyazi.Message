namespace Do_Svyazi.Message.Domain.Entities;

public class Content
{
    public Guid Id { get; }
    public Uri Uri { get; }
    public string Name { get; }
    public ContentType Type { get; }

    public Content(Uri uri, string name, ContentType type)
    {
        Id = Guid.NewGuid();
        Uri = uri;
        Name = name;
        Type = type;
    }
}