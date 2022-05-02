using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OWTChallenge.Infrastructure.Data.TypeConfiguration;
using OWTChallenge.Core.Entities;
using OWTChallenge.SharedKernel.Interfaces;

namespace OWTChallenge.Infrastructure.Data;

public partial class OWTChallengeContext : DbContext
{
    public OWTChallengeContext()
    {
    }

    public OWTChallengeContext(DbContextOptions<OWTChallengeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contact> Contacts { get; set; } = null!;
    public virtual DbSet<ContactSkillRel> ContactSkillRels { get; set; } = null!;
    public virtual DbSet<Level> Levels { get; set; } = null!;
    public virtual DbSet<Skill> Skills { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-KFACIPO\\SQLEXPRESS2019;Initial Catalog=OWTChallenge;Integrated Security=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region TODEL
        //modelBuilder.Entity<Contact>(entity =>
        //{
        //    entity.Property(e => e.Email).HasMaxLength(150);

        //    entity.Property(e => e.FirstName).HasMaxLength(50);

        //    entity.Property(e => e.FullName).HasMaxLength(100);

        //    entity.Property(e => e.LastName).HasMaxLength(50);

        //    entity.Property(e => e.PhoneNumber).HasMaxLength(50);
        //});

        //modelBuilder.Entity<ContactSkillRel>(entity =>
        //{
        //    entity.HasKey(e => new { e.ContactId, e.SkillId, e.LevelId })
        //        .HasName("PK_ContactSkillRel");

        //    entity.ToTable("ContactSkillRel");

        //    entity.HasOne(d => d.Contact)
        //        .WithMany(p => p.ContactSkillRels)
        //        .HasForeignKey(d => d.ContactId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_ContactSkill_Contact");

        //    entity.HasOne(d => d.Level)
        //        .WithMany(p => p.ContactSkillRels)
        //        .HasForeignKey(d => d.LevelId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_ContactSkill_Level");

        //    entity.HasOne(d => d.Skill)
        //        .WithMany(p => p.ContactSkillRels)
        //        .HasForeignKey(d => d.SkillId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_ContactSkill_Skill");
        //});

        //modelBuilder.Entity<Level>(entity =>
        //{
        //    entity.Property(e => e.Name).HasMaxLength(50);
        //});

        //modelBuilder.Entity<Skill>(entity =>
        //{
        //    entity.Property(e => e.Name).HasMaxLength(100);
        //});
        #endregion
        modelBuilder.ApplyConfiguration(new ContactsConfiguration());
        modelBuilder.ApplyConfiguration(new SkillsConfiguration());
        modelBuilder.ApplyConfiguration(new LevelsConfiguration());
        modelBuilder.ApplyConfiguration(new ContactSkillRelConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
