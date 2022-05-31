using Do_Svyazi.Message.Domain.Tools;

namespace Do_Svyazi.Message.Application.Abstractions.Tools;

public class ApplicationException : DomainException
{
    public ApplicationException() { }
    
    public ApplicationException(string? message, Exception? innerException) : base(message, innerException) { }
    
    public ApplicationException(string? message) : base(message) { }
}