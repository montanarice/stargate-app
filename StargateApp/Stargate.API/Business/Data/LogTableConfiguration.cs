using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StargateAPI.Business.Data;

public class LogTableConfiguration : IEntityTypeConfiguration<LogTableEntry>
{
    public void Configure(EntityTypeBuilder<LogTableEntry> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}