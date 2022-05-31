namespace Do_Svyazi.Message.Application.Abstractions.Tools;

public class ApplicationInvalidRequestException : ApplicationException
{
    public ApplicationInvalidRequestException() { }
    
    public ApplicationInvalidRequestException(string? message, Exception? innerException) : base(message, innerException) { }
    
    public ApplicationInvalidRequestException(string? message) : base(message) { }
}