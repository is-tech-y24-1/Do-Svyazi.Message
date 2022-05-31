namespace Do_Svyazi.Message.Application.Dto.Messages;

public record MessageDto
(
    Guid Id,
    Guid SenderId,
    string Text,
    DateTime PostDateTime,
    IReadOnlyCollection<ContentDto> Contents
);

public record ForwardedMessageDto
(
    Guid Id,
    Guid SenderId,
    string Text,
    DateTime PostDateTime,
    IReadOnlyCollection<ContentDto> Contents,
    Guid ForwardedMessageId
) : MessageDto(Id, SenderId, Text, PostDateTime, Contents);