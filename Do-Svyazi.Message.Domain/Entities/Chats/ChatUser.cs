namespace Do_Svyazi.Message.Domain.Entities;

public class ChatUser
{
    public User User { get; }
    public Chat Chat { get; }
    private List<Message> _userMessages;
    public Message? LastReadMessage { get; set; }
    public IReadOnlyCollection<Message> UserMessages => _userMessages;

    public ChatUser(User user, Chat chat, List<Message> userMessages)
    {
        User = user;
        Chat = chat;
        _userMessages = userMessages;
    }
}