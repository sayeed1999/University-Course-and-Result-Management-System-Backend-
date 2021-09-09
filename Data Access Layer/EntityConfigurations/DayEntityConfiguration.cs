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
    public class DayEntityConfiguration : IEntityTypeConfiguration<Day>
    {
        public void Configure(EntityTypeBuilder<Day> entity)
        {
            entity.HasKey(x => x.Name);
            entity.Property(x => x.Name).IsRequired().HasMaxLength(3);
            entity.HasData(
                new Day { Name = "Sun" },
                new Day { Name = "Mon" },
                new Day { Name = "Tue" },
                new Day { Name = "Wed" },
                new Day { Name = "Thu" },
                new Day { Name = "Fri" },
                new Day { Name = "Sat" }
            );
        }
    }
}
