using Do_Svyazi.Message.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Do_Svyazi.Message.Application.Abstractions.DataAccess;

public interface IMessageDatabaseContext
{
    DbSet<Chat> Chats { get; }
    DbSet<ChatUser> ChatUsers { get; }
    DbSet<User> Users { get; }
    
    DbSet<Content> Contents { get; }
    DbSet<Domain.Entities.Message> Messages { get; }
}