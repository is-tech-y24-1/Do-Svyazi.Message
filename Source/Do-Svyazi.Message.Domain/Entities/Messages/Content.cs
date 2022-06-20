using RichEntity.Annotations;

namespace Do_Svyazi.Message.Domain.Entities;

public partial class Content : IEntity<Guid>
{
    public Content(Uri uri, ContentType type)
        : this(Guid.NewGuid())
    {
        Uri = uri;
        Type = type;
    }

    public Uri Uri { get; protected init; }

    public ContentType Type { get; protected init; }
}