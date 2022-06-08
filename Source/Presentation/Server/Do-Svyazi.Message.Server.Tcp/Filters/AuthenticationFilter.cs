﻿using System.Security.Claims;
using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using Microsoft.AspNetCore.SignalR;

namespace Do_Svyazi.Message.Server.Tcp.Filters;

public class AuthenticationFilter : IHubFilter
{
    public async Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
    {
        var userIdClaim = context.Context.User?.Claims
            .Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value)
            .First();
        
        var user = new UserModel(Guid.Parse(userIdClaim));

        context.Context.Items["User"] = user;
        
        await next(context);
    }
}