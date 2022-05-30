﻿namespace Do_Svyazi.Message.Domain.Tools;

public class DomainException : Exception
{
    public DomainException() { }

    public DomainException(string? message, Exception? innerException) : base(message, innerException) { }

    public DomainException(string? message) : base(message) { }
}