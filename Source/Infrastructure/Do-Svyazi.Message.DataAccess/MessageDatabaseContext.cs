using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Do_Svyazi.Message.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Do_Svyazi.Message.DataAccess;

public sealed class MessageDatabaseContext : DbContext, IMessageDatabaseContext
{
    public MessageDatabaseContext(DbContextOptions<MessageDatabaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Chat> Chats { get; private init; } = null!;
    public DbSet<ChatUser> ChatUsers { get; private init; } = null!;
    public DbSet<User> Users { get; private init; } = null!;

    public DbSet<Content> Contents { get; private init; } = null!;
    public DbSet<Domain.Entities.Message> Messages { get; private init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);
    }
}