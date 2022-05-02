using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OWTChallenge.Core.Entities;

namespace OWTChallenge.Infrastructure.Data.TypeConfiguration;

public class SkillsConfiguration : IEntityTypeConfiguration<Skill>
{ 
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("Skill");
        builder.Property(e => e.Name).HasMaxLength(100);
    }

}
