namespace Do_Svyazi.Message.Domain.Tools;

public abstract class DomainException : Exception
{
    protected DomainException() { }

    protected DomainException(string? message, Exception? innerException) : base(message, innerException) { }

    protected DomainException(string? message) : base(message) { }
}