using Do_Svyazi.Message.Application.Abstractions.Exceptions;

namespace Do_Svyazi.Message.Application.CQRS.Exceptions;

public class ChatUserNotFoundException : NotFoundException
{
    public ChatUserNotFoundException(Guid chatId, Guid userId)
        : base($"Chat user with chat id {chatId} and user id {userId} was not found") { }
}