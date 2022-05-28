namespace Do_Svyazi.Message.Domain.Entities;

public class Chat
{
    public Guid Id { get; }
    public User[] Users { get; }

    public Chat(User[] users)
    {
        Id = Guid.NewGuid();
        Users = users;
    }
}