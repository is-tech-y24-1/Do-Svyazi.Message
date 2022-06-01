using Do_Svyazi.Message.Application.Dto.Messages;

namespace Do_Svyazi.Message.Application.Dto.Chats;

public record ChatUserDto(Guid ChatId, Guid UserId, MessageDto? LastReadMessage, MessageDto? LastMessage);