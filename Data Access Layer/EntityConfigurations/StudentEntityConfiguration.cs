using Entity_Layer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.EntityConfigurations
{
    public class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> entity)
        {
            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.Email).IsRequired();
            entity.HasIndex(x => x.Email).IsUnique();
            entity.HasCheckConstraint("CHK_StudentEmailInCorrectFormat", "Email like '%_@_%._%'");
            entity.Property(x => x.Contact).IsRequired();
            entity.Property(x => x.Address).IsRequired();
            entity.Property(x => x.RegistrationNumber).IsRequired();
            entity.HasIndex(x => x.RegistrationNumber).IsUnique();
            entity.HasCheckConstraint("CHK_RegistrationNumberMinLength", "LEN(RegistrationNumber) between 11 and 13");
        }
    }
}
