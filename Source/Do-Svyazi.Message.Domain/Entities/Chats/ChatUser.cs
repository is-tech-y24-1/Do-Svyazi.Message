namespace Do_Svyazi.Message.Domain.Entities;

public class ChatUser
{
    private List<Message> _userMessages;

    public ChatUser(User user, Chat chat)
    {
        User = user;
        Chat = chat;
        _userMessages = new List<Message>();
    }
    
    public User User { get; }
    public Chat Chat { get; }
    public Message? LastReadMessage { get; set; }
    public IReadOnlyCollection<Message> UserMessages => _userMessages.AsReadOnly();
}