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
    public class VIEW_Department_Config : IEntityTypeConfiguration<VIEW_Department>
    {
        public void Configure(EntityTypeBuilder<VIEW_Department> builder)
        {
            builder.HasNoKey();
            builder.ToView("VIEW_GetDepartments");
        }
    }
}
