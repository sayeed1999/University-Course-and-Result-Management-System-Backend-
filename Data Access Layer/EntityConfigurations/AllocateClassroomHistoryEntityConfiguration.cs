using Entity_Layer;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.EntityConfigurations
{
    public class AllocateClassroomHistoryEntityConfiguration : IEntityTypeConfiguration<AllocateClassroomHistory>
    {
        public void Configure(EntityTypeBuilder<AllocateClassroomHistory> entity)
        {
            entity.HasOne(x => x.Day).WithMany(x => x.AllocateClassroomHistories).HasForeignKey(x => x.DayId);
            entity.HasOne(x => x.Room).WithMany(x => x.AllocateClassroomHistories).HasForeignKey(x => x.RoomId);
            entity.Property(x => x.DayId).IsRequired();
            entity.Property(x => x.RoomId).IsRequired();
            entity.Property(x => x.CourseCode).IsRequired();
            entity.HasOne(x => x.Course).WithMany(x => x.AllocateClassroomHistories).HasForeignKey(x => x.CourseCode).HasPrincipalKey(x => x.Code);
        }
    }
}
