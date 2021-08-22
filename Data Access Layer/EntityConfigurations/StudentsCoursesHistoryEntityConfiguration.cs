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
    class StudentsCoursesHistoryEntityConfiguration : IEntityTypeConfiguration<StudentsCoursesHistory>
    {
        public void Configure(EntityTypeBuilder<StudentsCoursesHistory> entity)
        {
            entity.Property(x => x.CourseCode).IsRequired();
            entity.Property(x => x.Grade).IsRequired(false);
            entity.HasOne(x => x.Student).WithMany(x => x.StudentsCoursesHistories).HasForeignKey(x => x.StudentId);
            entity.HasOne(x => x.Course).WithMany(x => x.StudentsCoursesHistories).HasForeignKey(x => new { x.CourseCode, x.DepartmentId });
        }
    }
}
