namespace Do_Svyazi.Message.Domain.Entities;

public class ChatUser
{
    private readonly List<Message> _userMessages;

    public ChatUser(User user, Chat chat)
    {
        Id = Guid.NewGuid();
        User = user;
        Chat = chat;
        _userMessages = new List<Message>();
    }

#pragma warning disable CS8618
    protected ChatUser() { }
#pragma warning restore CS8618

    public Guid Id { get; protected init; }
    public virtual User User { get; protected init; }
    public virtual Chat Chat { get; protected init; }
    public virtual Message? LastReadMessage { get; set; }
    public virtual IReadOnlyCollection<Message> UserMessages => _userMessages.AsReadOnly();
}