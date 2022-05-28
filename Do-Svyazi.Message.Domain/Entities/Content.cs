namespace Do_Svyazi.Message.Domain.Entities;

public class Content
{
    private Uri Uri { get; }
    private string Name { get; }
    private ContentType Type { get; }

    public Content(Uri uri, string name, ContentType type)
    {
        Uri = uri;
        Name = name;
        Type = type;
    }
}