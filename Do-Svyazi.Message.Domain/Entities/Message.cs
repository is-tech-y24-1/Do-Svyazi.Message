namespace Do_Svyazi.Message.Domain.Entities;

public class Message
{
    private Guid Id { get; }
    private Guid UserId { get; }
    private Guid ChatId { get; }
    private string Text { get; }
    private DateTime PostDateTime { get; }
    private Content[] Content { get; }

    protected Message(Guid id, Guid userId, Guid chatId, string text, DateTime postDateTime, Content[] content)
    {
        Id = id;
        UserId = userId;
        ChatId = chatId;
        Text = text;
        PostDateTime = postDateTime;
        Content = content;
    }
}