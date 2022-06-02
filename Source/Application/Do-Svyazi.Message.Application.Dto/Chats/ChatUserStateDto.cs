using Do_Svyazi.Message.Application.Dto.Messages;

namespace Do_Svyazi.Message.Application.Dto.Chats;

public record ChatUserStateDto(Guid UserId, Guid ChatId, int UnreadMessageCount, MessageDto? LastMessage);