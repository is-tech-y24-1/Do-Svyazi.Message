namespace Do_Svyazi.Message.Domain.Entities;

public class UserChatState
{
    public User User { get; }
    public Chat Chat { get; }
    public Message? LastReadMessage { get; }

    public UserChatState(User user, Chat chat, Message? lastReadMessage)
    {
        User = user;
        Chat = chat;
        LastReadMessage = lastReadMessage;
    }
}