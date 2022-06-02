namespace Do_Svyazi.Message.Application.Abstractions.Exceptions.NotFound;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(Guid userId)
        : base($"User with id {userId} was not found.") { }
}