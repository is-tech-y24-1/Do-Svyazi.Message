namespace Do_Svyazi.Message.Application.Abstractions.Exceptions.Unauthorized;

public class UnauthenticatedException : UnauthorizedException
{
    public UnauthenticatedException()
        : base("User is not attached") { }
}