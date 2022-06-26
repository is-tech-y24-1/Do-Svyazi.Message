using Do_Svyazi.Message.Application.CQRS.Extensions;
using Do_Svyazi.Message.Application.Services.Extensions;
using Do_Svyazi.Message.DataAccess.Extensions;
using Do_Svyazi.Message.Integrations.Extensions;
using Do_Svyazi.Message.Mapping.Extensions;
using Do_Svyazi.Message.Server.Http.Extensions;
using Do_Svyazi.Message.Server.Tcp.Extensions;
using Do_Svyazi.Message.Server.WebAPI.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var databaseConnectionString = builder.Configuration.GetConnectionString("Database");
// 7052BF80-5506-4D51-9F1A-C9316ED3839D
// Application
// "Database": "Host=localhost:5433;Database=Do-Svyazi.Message;Username=postgres;Password=postgres;"
builder.Services.AddApplicationServices();
builder.Services.AddCqrs();

// Infrastructure
builder.Services.AddDataAccess
(
    c => c
        .UseSqlite(databaseConnectionString)
        // .UseNpgsql(databaseConnectionString)
        .UseLazyLoadingProxies()
        .EnableSensitiveDataLogging()
);
builder.Services.AddIntegrations();
builder.Services.AddMapping();

// Server
builder.Services.AddHttpServer();
builder.Services.AddTcpServer();
builder.Services.AddCustomAuthentication();

builder.Services.AddEndpointsApiExplorer();

builder.AddSwaggerConfiguration();

var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();

app.UseHttpServer();
app.UseTcpServer();

app.MapControllers();

app.Run();