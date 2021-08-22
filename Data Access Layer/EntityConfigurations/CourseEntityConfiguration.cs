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
    class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> entity)
        {
            entity.HasOne(x => x.Department).WithMany(x => x.Courses).HasForeignKey(x => x.DepartmentId);
            entity.HasKey(x => new { x.Code, x.DepartmentId });
            entity.Property(x => x.Code).IsRequired();
            entity.Property(x => x.Name).IsRequired();
            entity.HasCheckConstraint("CHK_LengthOfCodeOfCourse", "LEN(Code) >= 5");
            entity.HasCheckConstraint("CHK_CreditRangeOfCourse", "Credit BETWEEN 0.5 AND 5.0");
        }
    }
}
