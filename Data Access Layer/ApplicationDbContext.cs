using Data_Access_Layer.EntityConfigurations;
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
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; } // it references Semister, so even if i dont create Semisters table here, it will auto create in the db!
        public DbSet<Semister> Semisters { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentsCourses { get; set; }
        public DbSet<GradeLetter> GradeLetters { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<AllocateClassroom> AllocateClassrooms { get; set; }
        public DbSet<CourseHistory> CoursesHistories { get; set; }
        public DbSet<StudentCourseHistory> StudentCourseHistories { get; set; }
        public DbSet<AllocateClassroomHistory> AllocateClassroomHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=sayeeds-coding-\sqlexpress;Database=UniversityCourseAndResultManagementSystem;trusted_connection=SSPI");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new DepartmentEntityConfiguration());
            builder.ApplyConfiguration(new SemisterEntityConfiguration());
            builder.ApplyConfiguration(new CourseEntityConfiguration());
            builder.ApplyConfiguration(new DesignationEntityConfiguration());
            builder.ApplyConfiguration(new TeacherEntityConfiguration());
            builder.ApplyConfiguration(new StudentEntityConfiguration());
            builder.ApplyConfiguration(new StudentCourseEntityConfiguration());
            builder.ApplyConfiguration(new GradeLetterEntityConfiguration());
            builder.ApplyConfiguration(new DayEntityConfiguration());
            builder.ApplyConfiguration(new RoomEntityConfiguration());
            builder.ApplyConfiguration(new AllocateClassroomEntityConfiguration());
            builder.ApplyConfiguration(new AllocateClassroomHistoryEntityConfiguration());
            builder.ApplyConfiguration(new CourseHistoryEntityConfiguration());
            builder.ApplyConfiguration(new StudentCourseHistoryEntityConfiguration());
        }
    }
}