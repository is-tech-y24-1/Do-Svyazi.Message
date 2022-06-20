using RichEntity.Annotations;

namespace Do_Svyazi.Message.Domain.Entities;

public partial class ChatUser : IEntity<Guid>
{
    private readonly List<Message> _userMessages;

    public ChatUser(User user, Chat chat)
        : this(Guid.NewGuid())
    {
        User = user;
        Chat = chat;
        _userMessages = new List<Message>();
    }

    public virtual User User { get; protected init; }

    public virtual Chat Chat { get; protected init; }

    public virtual Message? LastReadMessage { get; set; }

    public virtual IReadOnlyCollection<Message> UserMessages => _userMessages.AsReadOnly();
}