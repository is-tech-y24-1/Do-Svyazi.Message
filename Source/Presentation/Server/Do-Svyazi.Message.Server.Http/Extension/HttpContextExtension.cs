using Do_Svyazi.Message.Application.Abstractions.Integrations.Models;
using Microsoft.AspNetCore.Http;

namespace Do_Svyazi.Message.Server.Http.Extension;

public static class HttpContextExtension
{
    public static UserModel GetUserModel(this HttpContext context)
    {
        return (UserModel) context.Items["User"];
    }
}