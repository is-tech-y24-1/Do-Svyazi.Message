namespace Do_Svyazi.Message.Domain.Entities;

public class UserChatState
{
    private Guid UserId { get; }
    private Guid ChatId { get; }

    private Message? LastReadMessage { get; }
}