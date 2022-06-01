using Do_Svyazi.Message.Application.Abstractions.Exceptions;

namespace Do_Svyazi.Message.Application.CQRS.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(Guid userId)
        : base($"User with id {userId} was not found.") { }
}