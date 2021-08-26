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
    public class SemisterEntityConfiguration : IEntityTypeConfiguration<Semister>
    {
        public void Configure(EntityTypeBuilder<Semister> entity)
        {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
                entity.HasData(
                    new Semister { Id = 1, Name = "1st" },
                    new Semister { Id = 2, Name = "2nd" },
                    new Semister { Id = 3, Name = "3rd" },
                    new Semister { Id = 4, Name = "4th" },
                    new Semister { Id = 5, Name = "5th" },
                    new Semister { Id = 6, Name = "6th" },
                    new Semister { Id = 7, Name = "7th" },
                    new Semister { Id = 8, Name = "8th" }
                );

        }
    }
}
