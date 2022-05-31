namespace Do_Svyazi.Message.Application.Abstractions.Tools;

public class ApplicationUnauthorizedException : System.ApplicationException
{
    public ApplicationUnauthorizedException() { }
    
    public ApplicationUnauthorizedException(string? message, Exception? innerException) : base(message, innerException) { }
    
    public ApplicationUnauthorizedException(string? message) : base(message) { }
}