using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OWTChallenge.Core.Entities;

namespace OWTChallenge.Infrastructure.Data.TypeConfiguration;
public class LevelsConfiguration : IEntityTypeConfiguration<Level>
{
    public void Configure(EntityTypeBuilder<Level> builder)
    {
        builder.ToTable("Level");
        builder.Property(e => e.Name).HasMaxLength(50);
    }

}
