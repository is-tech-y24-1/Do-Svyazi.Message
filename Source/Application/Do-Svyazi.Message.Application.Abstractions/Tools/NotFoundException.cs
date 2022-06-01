﻿namespace Do_Svyazi.Message.Application.Abstractions.Tools;

public abstract class NotFoundException : ApplicationException
{
    protected NotFoundException() { }

    protected NotFoundException(string? message, Exception? innerException) : base(message, innerException) { }

    protected NotFoundException(string? message) : base(message) { }
}