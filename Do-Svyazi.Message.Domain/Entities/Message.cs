namespace Do_Svyazi.Message.Domain.Entities;

public class Message
{
    private Guid Id { get; }
    private Guid UserId { get; }
    private Guid ChatId { get; }
    private string Text { get; }
    private DateTime PostDateTime { get; }
    private Content[] Content { get; }
}