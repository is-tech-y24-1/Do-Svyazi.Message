namespace Do_Svyazi.Message.Application.Abstractions.Tools;

public class InvalidRequestException : ApplicationException
{
    public InvalidRequestException() { }
    
    public InvalidRequestException(string? message, Exception? innerException) : base(message, innerException) { }
    
    public InvalidRequestException(string? message) : base(message) { }
}