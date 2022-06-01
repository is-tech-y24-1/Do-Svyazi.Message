using Do_Svyazi.Message.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Do_Svyazi.Message.DataAccess.Configurations;

public class ForwardedMessageConfiguration : IEntityTypeConfiguration<ForwardedMessage>
{
    public void Configure(EntityTypeBuilder<ForwardedMessage> builder)
    {
        builder.HasBaseType<Domain.Entities.Message>();
        builder.HasOne(b => b.Message);
    }
}