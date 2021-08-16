using Entity_Layer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=sayeeds-coding-\sqlexpress;Database=UniversityCourseAndResultManagementSystem;trusted_connection=SSPI");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /// Table: Departments

            builder.Entity<Department>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Code).IsRequired().HasMaxLength(7);
                entity.HasIndex(x => x.Code).IsUnique();
                entity.HasCheckConstraint("CHK_LengthOfCode", "len(code) >= 2 and len(code) <= 7");
                entity.Property(x => x.Name).IsRequired().HasMaxLength(255);
                entity.HasIndex(x => x.Name).IsUnique();
                entity.HasCheckConstraint("CHK_LengthOfDeptName", "len(name) >= 3");
                entity.HasData(
                    new Department { Id = 1, Code = "EEE", Name = "Electronics & Electrical Engineering" },
                    new Department { Id = 2, Code = "CSE", Name = "Computer Science & Engineering" },
                    new Department { Id = 3, Code = "CE", Name = "Civil Engineering" },
                    new Department { Id = 4, Code = "ME", Name = "Mechanical Engineering" },
                    new Department { Id = 5, Code = "MTE", Name = "Mechatronics Engineering" },
                    new Department { Id = 6, Code = "IPE", Name = "Industrial Production & Engineering" }
                );
            });

        }
    }
}