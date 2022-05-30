using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Do_Svyazi.Message.DataAccess.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Domain.Entities.Message>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Message> builder)
    {
        builder.HasOne(b => b.Sender);
        builder.Navigation(b => b.Contents).HasField("_contents");
    }
}