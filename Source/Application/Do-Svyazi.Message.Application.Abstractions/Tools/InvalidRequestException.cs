namespace Do_Svyazi.Message.Application.Abstractions.Tools;

abstract class InvalidRequestException : ApplicationException
{
    protected InvalidRequestException() { }

    protected InvalidRequestException(string? message, Exception? innerException) : base(message, innerException) { }

    protected InvalidRequestException(string? message) : base(message) { }
}