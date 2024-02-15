﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StargateAPI.Business.Data;

public class AstronautDutyConfiguration : IEntityTypeConfiguration<AstronautDuty>
{
    public void Configure(EntityTypeBuilder<AstronautDuty> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}