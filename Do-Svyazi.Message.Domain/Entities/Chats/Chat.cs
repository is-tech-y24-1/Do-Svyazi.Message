namespace Do_Svyazi.Message.Domain.Entities;

public class Chat
{
    public Guid Id { get; }

    public Chat(Guid id)
    {
        Id = id;
    }
}