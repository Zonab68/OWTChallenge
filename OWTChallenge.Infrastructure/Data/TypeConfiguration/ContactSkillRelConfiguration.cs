using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OWTChallenge.Core.Entities;

namespace OWTChallenge.Infrastructure.Data.TypeConfiguration;

public class ContactSkillRelConfiguration : IEntityTypeConfiguration<ContactSkillRel>
{
    public void Configure(EntityTypeBuilder<ContactSkillRel> builder)
    {
        builder.ToTable("ContactSkillRel");
        
        builder.HasKey(e => new { e.ContactId, e.SkillId, e.LevelId})
                    .HasName("PK_ContactSkillRel");

        builder.HasOne(d => d.Contact)
                    .WithMany(p => p.ContactSkillRels)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactSkill_Contact");

        builder.HasOne(d => d.Level)
                    .WithMany(p => p.ContactSkillRels)
                    .HasForeignKey(d => d.LevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactSkill_Level");

        builder.HasOne(d => d.Skill)
                    .WithMany(p => p.ContactSkillRels)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactSkill_Skill");
            }
}
