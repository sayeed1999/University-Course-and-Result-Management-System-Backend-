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
        public DbSet<Course> Courses { get; set; } // it references Semister, so even if i dont create Semisters table here, it will auto create in the db!
        public DbSet<Semister> Semisters { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentsCourses { get; set; }
        public DbSet<GradeLetter> GradeLetters { get; set; }

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

            /// Table : Semisters

            builder.Entity<Semister>(entity =>
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
            });

            /// Table : Courses

            builder.Entity<Course>(entity =>
            {
                entity.HasOne(x => x.Department).WithMany(x => x.Courses).HasForeignKey(x => x.DepartmentId);
                entity.HasKey(x => new { x.Code, x.DepartmentId });
                entity.Property(x => x.Code).IsRequired();
                entity.Property(x => x.Name).IsRequired();
                // Credit, SemisterId, DepartmentId by default required for their data types..
                // Description is the only field in this table nullable..
                entity.HasCheckConstraint("CHK_LengthOfCodeOfCourse", "LEN(Code) >= 5");
                entity.HasCheckConstraint("CHK_CreditRangeOfCourse", "Credit BETWEEN 0.5 AND 5.0");
            });

            /// Table : Designations

            builder.Entity<Designation>(entity =>
            {
                entity.Property(x => x.Name).IsRequired();
                entity.HasIndex(x => x.Name).IsUnique();
                entity.HasData(
                    new Designation { Id = 1, Name = "Lecturer" },
                    new Designation { Id = 2, Name = "Assistant Lecturer" },
                    new Designation { Id = 3, Name = "Professor" },
                    new Designation { Id = 4, Name = "Associate Professor" }
                );
            });

            /// Table : Teachers

            builder.Entity<Teacher>(entity =>
            {
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Email).IsRequired();
                entity.HasIndex(x => x.Email).IsUnique();
                entity.HasCheckConstraint("CHK_TeacherEmailInCorrectFormat", "Email like '%_@_%._%'"); // Email like '%_@_%.com' can't track dks.mte@ruet.ac.bd !!
                entity.HasCheckConstraint("CHK_TeacherContactInCorrectFormat", "LEN(CAST(Contact as varchar(max))) between 6 and 15");
                entity.HasOne(a => a.Department).WithMany(b => b.Teachers).HasForeignKey(x => x.DepartmentId);
                entity.HasCheckConstraint("CHK_CreditToBeTakenByTeacher", "CreditToBeTaken !< 0");
                entity.HasCheckConstraint("CHK_RemainingCreditOfTeacher", "RemainingCredit BETWEEN 0 AND CreditToBeTaken");
            });

            /// Table : Students

            builder.Entity<Student>(entity =>
            {
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Email).IsRequired();
                entity.HasIndex(x => x.Email).IsUnique();
                entity.HasCheckConstraint("CHK_StudentEmailInCorrectFormat", "Email like '%_@_%._%'");
                entity.Property(x => x.Contact).IsRequired();
                entity.Property(x => x.Address).IsRequired();
                entity.Property(x => x.RegistrationNumber).IsRequired();
                entity.HasIndex(x => x.RegistrationNumber).IsUnique();
                entity.HasCheckConstraint("CHK_RegistrationNumberMinLength", "LEN(RegistrationNumber) between 11 and 13");
            });

            /// Table : StudentsCourses

            builder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(x => new { x.DepartmentId, x.CourseCode, x.StudentId });
                entity.Property(x => x.CourseCode).IsRequired();
                entity.Property(x => x.Grade).IsRequired(false);
                entity.HasOne(x => x.Student).WithMany(x => x.StudentsCourses).HasForeignKey(x => x.StudentId);
                entity.HasOne(x => x.Course).WithMany(x => x.StudentsCourses).HasForeignKey(x => new { x.CourseCode, x.DepartmentId });
            });

            /// Table : GradeLetter

            builder.Entity<GradeLetter>(entity =>
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
            });
        }
    }
}