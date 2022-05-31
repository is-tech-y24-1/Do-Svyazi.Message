namespace Do_Svyazi.Message.Application.Abstractions.Tools;

public class ApplicationNotFoundException : System.ApplicationException
{
    public ApplicationNotFoundException() { }
    
    public ApplicationNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
    
    public ApplicationNotFoundException(string? message) : base(message) { }
}