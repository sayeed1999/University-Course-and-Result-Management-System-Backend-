using Entity_Layer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Semister> Semisters { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentsCourses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<AllocateClassroom> AllocateClassrooms { get; set; }
        public DbSet<CourseHistory> CoursesHistories { get; set; }
        public DbSet<StudentCourseHistory> StudentCourseHistories { get; set; }
        public DbSet<AllocateClassroomHistory> AllocateClassroomHistories { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuRole> MenuWiseRolePermissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=sayeeds-coding-\sqlexpress; Database=UniversityCourseAndResultManagementSystem(temp); trusted_connection=SSPI; MultipleActiveResultSets=True");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // builder.ApplyConfiguration(new DepartmentEntityConfiguration());

        }
    }
}