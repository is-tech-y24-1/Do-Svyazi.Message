namespace Do_Svyazi.Message.Application.Abstractions.Exceptions;

public abstract class InvalidRequestException : ApplicationException
{
    protected InvalidRequestException() { }

    protected InvalidRequestException(string? message, Exception? innerException) : base(message, innerException) { }

    protected InvalidRequestException(string? message) : base(message) { }
}