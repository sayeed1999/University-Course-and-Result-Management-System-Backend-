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
    public class StudentCourseEntityConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> entity)
        {
            entity.HasKey(x => new { x.DepartmentId, x.CourseCode, x.StudentId });
            entity.Property(x => x.CourseCode).IsRequired();
            entity.Property(x => x.Grade).IsRequired(false);
            entity.HasOne(x => x.GradeLetter).WithMany(x => x.StudentsCourses).HasForeignKey(x => x.Grade);
            entity.HasOne(x => x.Student).WithMany(x => x.StudentsCourses).HasForeignKey(x => x.StudentId);
            entity.HasOne(x => x.Course).WithMany(x => x.StudentsCourses).HasForeignKey(x => new { x.CourseCode, x.DepartmentId });
        }
    }
}
