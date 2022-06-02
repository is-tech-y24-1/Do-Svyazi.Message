namespace Do_Svyazi.Message.Application.Abstractions.Exceptions.Unauthorized;

public class AuthenticationException : UnauthorizedException
{
    public AuthenticationException()
        : base("User is not touched") { }
}