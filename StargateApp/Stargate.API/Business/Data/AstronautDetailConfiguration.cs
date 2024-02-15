using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StargateAPI.Business.Data;

public class AstronautDetailConfiguration : IEntityTypeConfiguration<AstronautDetail>
{
    public void Configure(EntityTypeBuilder<AstronautDetail> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}