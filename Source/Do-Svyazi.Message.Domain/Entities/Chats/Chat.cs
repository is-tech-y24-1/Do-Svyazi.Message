namespace Do_Svyazi.Message.Domain.Entities;

public class Chat
{
    public Chat(Guid id)
    {
        Id = id;
    }

    protected Chat() { }

    public Guid Id { get; protected init; }
}