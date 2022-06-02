namespace Do_Svyazi.Message.Application.Abstractions.Exceptions.NotFound;

public class ContentNotFoundException : NotFoundException
{
    public ContentNotFoundException(IEnumerable<Guid> contentIds)
        : base($"Contents were not found with ids: {string.Join(", ", contentIds)}") { }
}