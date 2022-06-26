using Do_Svyazi.Message.Application.Dto.Messages;

namespace Do_Svyazi.Message.Server.Http.Models;

public record UpdateMessageContentRequest(
    IReadOnlyCollection<ContentDto> AddedContents,
    IReadOnlyCollection<MessageContentDto> RemovedContents);