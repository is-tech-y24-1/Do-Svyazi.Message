using Do_Svyazi.Message.Server.WebAPI.Utility;
using Microsoft.OpenApi.Models;

namespace Do_Svyazi.Message.Server.WebAPI.Extensions;

public static class SwaggerExtensions
{
    public static void AddSwaggerConfiguration(
        this WebApplicationBuilder builder,
        SwaggerPolymorphismProvider? polymorphismProvider = null)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddSwaggerGen(o =>
            {
                if (polymorphismProvider is not null)
                {
                    o.UseAllOfForInheritance();
                    o.UseOneOfForPolymorphism();
                    o.SelectSubTypesUsing(polymorphismProvider.Resolve);
                }
                
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }
    }

    public static void UseSwaggerConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}