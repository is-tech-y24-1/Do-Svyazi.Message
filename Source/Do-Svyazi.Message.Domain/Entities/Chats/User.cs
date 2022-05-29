namespace Do_Svyazi.Message.Domain.Entities;

public class User
{
    public Guid Id { get; }

    public User(Guid id)
    {
        Id = id;
    }
}