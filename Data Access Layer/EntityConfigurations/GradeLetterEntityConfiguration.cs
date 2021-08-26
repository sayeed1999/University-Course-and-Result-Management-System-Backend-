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
    public class GradeLetterEntityConfiguration : IEntityTypeConfiguration<GradeLetter>
    {
        public void Configure(EntityTypeBuilder<GradeLetter> entity)
        {
            entity.HasKey(x => x.Grade);
            entity.HasData(
                new GradeLetter() { Grade = "A+" },
                new GradeLetter() { Grade = "A" },
                new GradeLetter() { Grade = "A-" },
                new GradeLetter() { Grade = "B+" },
                new GradeLetter() { Grade = "B" },
                new GradeLetter() { Grade = "B-" },
                new GradeLetter() { Grade = "C+" },
                new GradeLetter() { Grade = "C" },
                new GradeLetter() { Grade = "C-" },
                new GradeLetter() { Grade = "D+" },
                new GradeLetter() { Grade = "D" },
                new GradeLetter() { Grade = "D-" },
                new GradeLetter() { Grade = "F" }
            );
        }
    }
}
