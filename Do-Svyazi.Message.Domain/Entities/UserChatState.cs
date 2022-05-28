namespace Do_Svyazi.Message.Domain.Entities;

public class UserChatState
{
    private Guid UserId { get; }
    private Guid ChatId { get; }

    private Message? LastReadMessage { get; }


    public UserChatState(Guid userId, Guid chatId, Message? lastReadMessage)
    {
        UserId = userId;
        ChatId = chatId;
        LastReadMessage = lastReadMessage;
    }
}