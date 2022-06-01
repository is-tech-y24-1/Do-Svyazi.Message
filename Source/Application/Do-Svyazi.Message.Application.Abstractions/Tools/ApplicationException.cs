using Do_Svyazi.Message.Domain.Tools;

namespace Do_Svyazi.Message.Application.Abstractions.Tools;

public abstract class ApplicationException : DomainException
{
    protected ApplicationException() { }

    protected ApplicationException(string? message, Exception? innerException) : base(message, innerException) { }

    protected ApplicationException(string? message) : base(message) { }
}