namespace Do_Svyazi.Message.Domain.Entities;

public class User
{
    public User(Guid id)
    {
        Id = id;
    }

    protected User() { }

    public Guid Id { get; protected init; }
}