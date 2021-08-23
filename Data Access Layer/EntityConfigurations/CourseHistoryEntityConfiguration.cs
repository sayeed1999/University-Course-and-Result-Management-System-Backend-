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
    public class CourseHistoryEntityConfiguration : IEntityTypeConfiguration<CourseHistory>
    {
        public void Configure(EntityTypeBuilder<CourseHistory> entity)
        {
            entity.HasKey(x => x.Id);
            entity.HasOne(x => x.Department).WithMany(x => x.CourseHistories).HasForeignKey(x => x.DepartmentId);
            entity.HasIndex(x => new { x.Code, x.DepartmentId });
            entity.Property(x => x.Code).IsRequired();
        }

    }
}
