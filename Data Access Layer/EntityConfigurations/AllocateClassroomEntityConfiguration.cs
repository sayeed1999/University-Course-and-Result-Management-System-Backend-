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
    public class AllocateClassroomEntityConfiguration : IEntityTypeConfiguration<AllocateClassroom>
    {
        public void Configure(EntityTypeBuilder<AllocateClassroom> entity)
        {
            entity.HasOne(x => x.Day).WithMany(x => x.AllocateClassrooms).HasForeignKey(x => x.DayId);
            entity.HasOne(x => x.Room).WithMany(x => x.AllocateClassrooms).HasForeignKey(x => x.RoomId);
            entity.Property(x => x.DayId).IsRequired();
            entity.Property(x => x.RoomId).IsRequired();
            entity.Property(x => x.CourseCode).IsRequired();
            entity.HasOne(x => x.Course).WithMany(x => x.AllocateClassrooms).HasForeignKey(x => x.CourseCode).HasPrincipalKey(x => x.Code);
        }
    }
}
