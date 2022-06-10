namespace Do_Svyazi.Message.Application.Abstractions.Exceptions.Unauthorized;

public class UnauthorizedChatUserException : UnauthorizedException
{
    public UnauthorizedChatUserException(Guid userId, Guid chatId)
        : base($"User with id {userId} is not authorized for actions with chat with id {chatId}") { }
}