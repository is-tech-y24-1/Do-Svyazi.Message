namespace Do_Svyazi.Message.Application.Abstractions.Tools;

public class UnauthorizedException : ApplicationException
{
    public UnauthorizedException() { }
    
    public UnauthorizedException(string? message, Exception? innerException) : base(message, innerException) { }
    
    public UnauthorizedException(string? message) : base(message) { }
}