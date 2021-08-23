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
    class StudentCourseHistoryEntityConfiguration : IEntityTypeConfiguration<StudentCourseHistory>
    {
        public void Configure(EntityTypeBuilder<StudentCourseHistory> entity)
        {
            entity.Property(x => x.CourseCode).IsRequired();
            entity.Property(x => x.Grade).IsRequired(false);
            entity.HasOne(x => x.GradeLetter).WithMany(x => x.StudentCourseHistories).HasForeignKey(x => x.Grade);
            entity.HasOne(x => x.Student).WithMany(x => x.StudentCourseHistories).HasForeignKey(x => x.StudentId);
            entity.HasOne(x => x.Course).WithMany(x => x.StudentCourseHistories).HasForeignKey(x => new { x.CourseCode, x.DepartmentId });
        }
    }
}
