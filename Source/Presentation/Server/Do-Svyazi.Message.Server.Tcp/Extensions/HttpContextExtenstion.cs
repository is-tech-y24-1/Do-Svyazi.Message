﻿using Do_Svyazi.Message.Application.Abstractions.Exceptions.Unauthorized;
using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using Microsoft.AspNetCore.Http;

namespace Do_Svyazi.Message.Server.Tcp.Extensions;

public static class HttpContextExtenstion
{
    public static UserModel GetUserModel(this HttpContext? context)
    {
        if (context?.Items["User"] is UserModel userModel)
        {
            return userModel;
        }

        throw new UnauthenticatedException();

    }
}