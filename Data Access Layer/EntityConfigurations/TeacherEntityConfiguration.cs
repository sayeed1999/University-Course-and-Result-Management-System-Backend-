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
    public class TeacherEntityConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> entity)
        {
            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.Email).IsRequired();
            entity.HasIndex(x => x.Email).IsUnique();
            entity.HasCheckConstraint("CHK_TeacherEmailInCorrectFormat", "Email like '%_@_%._%'"); // Email like '%_@_%.com' can't track dks.mte@ruet.ac.bd !!
            entity.HasCheckConstraint("CHK_TeacherContactInCorrectFormat", "LEN(CAST(Contact as varchar(max))) between 6 and 15");
            entity.HasOne(a => a.Department).WithMany(b => b.Teachers).HasForeignKey(x => x.DepartmentId);
            entity.HasOne(a => a.Designation).WithMany(a => a.Teachers).HasForeignKey(a => a.DesignationId);
            entity.HasCheckConstraint("CHK_CreditToBeTakenByTeacher", "CreditToBeTaken !< 0");
            entity.HasCheckConstraint("CHK_RemainingCreditOfTeacher", "RemainingCredit BETWEEN 0 AND CreditToBeTaken");
        }
    }
}
