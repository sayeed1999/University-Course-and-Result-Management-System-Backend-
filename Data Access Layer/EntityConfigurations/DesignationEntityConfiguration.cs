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
    public class DesignationEntityConfiguration : IEntityTypeConfiguration<Designation>
    {
        public void Configure(EntityTypeBuilder<Designation> entity)
        {
            entity.Property(x => x.Name).IsRequired();
            entity.HasIndex(x => x.Name).IsUnique();
            entity.HasData(
                new Designation { Id = 1, Name = "Lecturer" },
                new Designation { Id = 2, Name = "Assistant Lecturer" },
                new Designation { Id = 3, Name = "Professor" },
                new Designation { Id = 4, Name = "Associate Professor" }
            );
        }
    }
}
