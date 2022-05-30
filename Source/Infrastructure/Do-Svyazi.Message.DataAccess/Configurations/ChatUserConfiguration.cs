using Do_Svyazi.Message.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Do_Svyazi.Message.DataAccess.Configurations;

public class ChatUserConfiguration : IEntityTypeConfiguration<ChatUser>
{
    public void Configure(EntityTypeBuilder<ChatUser> builder)
    {
        builder.HasOne(b => b.Chat);
        builder.HasOne(b => b.User);
        builder.HasOne(b => b.LastReadMessage).WithMany();
        builder.Navigation(b => b.UserMessages).HasField("_userMessages");
    }
}