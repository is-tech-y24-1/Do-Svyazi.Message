namespace Do_Svyazi.Message.Application.Abstractions.Tools;

public class NotFoundException : ApplicationException
{
    public NotFoundException() { }
    
    public NotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
    
    public NotFoundException(string? message) : base(message) { }
}