namespace Do_Svyazi.Message.Domain.Entities;

public class User
{
    public Guid Id { get; }
    public string Name { get; }

    public User(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}