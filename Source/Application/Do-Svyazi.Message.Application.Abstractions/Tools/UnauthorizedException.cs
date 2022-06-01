﻿namespace Do_Svyazi.Message.Application.Abstractions.Tools;

public abstract class UnauthorizedException : ApplicationException
{
    protected UnauthorizedException() { }

    protected UnauthorizedException(string? message, Exception? innerException) : base(message, innerException) { }

    protected UnauthorizedException(string? message) : base(message) { }
}