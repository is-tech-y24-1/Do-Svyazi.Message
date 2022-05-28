using System.Runtime.Serialization;

namespace Do_Svyazi.Message.Domain.Tools;

public class DomainException : Exception
{
    public DomainException()
    {
    }

    public DomainException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DomainException(string? message) : base(message)
    {
    }
}