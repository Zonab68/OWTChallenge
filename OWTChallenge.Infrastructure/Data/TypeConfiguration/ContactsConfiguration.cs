using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OWTChallenge.Core.Entities;

namespace OWTChallenge.Infrastructure.Data.TypeConfiguration;

public class ContactsConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contact");

        builder.Property(e => e.Email).HasMaxLength(150);

        builder.Property(e => e.FirstName).HasMaxLength(50);

        builder.Property(e => e.LastName).HasMaxLength(50);

        builder.Property(e => e.PhoneNumber).HasMaxLength(50);
        
    }

}
