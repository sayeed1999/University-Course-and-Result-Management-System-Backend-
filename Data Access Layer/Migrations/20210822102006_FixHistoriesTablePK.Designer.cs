﻿// <auto-generated />
using System;
using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data_Access_Layer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210822102006_FixHistoriesTablePK")]
    partial class FixHistoriesTablePK
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entity_Layer.AllocateClassroom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CourseCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DayId")
                        .IsRequired()
                        .HasColumnType("nvarchar(3)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoomId")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("To")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CourseCode");

                    b.HasIndex("DayId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("RoomId");

                    b.ToTable("AllocateClassrooms");
                });

            modelBuilder.Entity("Entity_Layer.AllocateClassroomHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CourseCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseCode1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("CourseDepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("DayId")
                        .IsRequired()
                        .HasColumnType("nvarchar(3)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoomId")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("To")
                        .HasColumnType("datetime2");

                    b.Property<int>("UnallocatingRoomsCountId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("RoomId");

                    b.HasIndex("UnallocatingRoomsCountId");

                    b.HasIndex("CourseCode1", "CourseDepartmentId");

                    b.ToTable("AllocateClassroomHistories");
                });

            modelBuilder.Entity("Entity_Layer.Course", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<float>("Credit")
                        .HasColumnType("real");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("SemisterId")
                        .HasColumnType("tinyint");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Code", "DepartmentId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("SemisterId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");

                    b.HasCheckConstraint("CHK_LengthOfCodeOfCourse", "LEN(Code) >= 5");

                    b.HasCheckConstraint("CHK_CreditRangeOfCourse", "Credit BETWEEN 0.5 AND 5.0");
                });

            modelBuilder.Entity("Entity_Layer.CourseHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<byte>("SemisterId")
                        .HasColumnType("tinyint");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int");

                    b.Property<int>("UnassignCoursesCountId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("SemisterId");

                    b.HasIndex("TeacherId");

                    b.HasIndex("UnassignCoursesCountId");

                    b.ToTable("CoursesHistory");
                });

            modelBuilder.Entity("Entity_Layer.Day", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("Name");

                    b.ToTable("Days");

                    b.HasData(
                        new
                        {
                            Name = "Sun"
                        },
                        new
                        {
                            Name = "Mon"
                        },
                        new
                        {
                            Name = "Tue"
                        },
                        new
                        {
                            Name = "Wed"
                        },
                        new
                        {
                            Name = "Thu"
                        },
                        new
                        {
                            Name = "Fri"
                        },
                        new
                        {
                            Name = "Sat"
                        });
                });

            modelBuilder.Entity("Entity_Layer.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Departments");

                    b.HasCheckConstraint("CHK_LengthOfCode", "len(code) >= 2 and len(code) <= 7");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "EEE",
                            Name = "Electronics & Electrical Engineering"
                        },
                        new
                        {
                            Id = 2,
                            Code = "CSE",
                            Name = "Computer Science & Engineering"
                        },
                        new
                        {
                            Id = 3,
                            Code = "CE",
                            Name = "Civil Engineering"
                        },
                        new
                        {
                            Id = 4,
                            Code = "ME",
                            Name = "Mechanical Engineering"
                        },
                        new
                        {
                            Id = 5,
                            Code = "MTE",
                            Name = "Mechatronics Engineering"
                        },
                        new
                        {
                            Id = 6,
                            Code = "IPE",
                            Name = "Industrial Production & Engineering"
                        });
                });

            modelBuilder.Entity("Entity_Layer.Designation", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Designations");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            Name = "Lecturer"
                        },
                        new
                        {
                            Id = (byte)2,
                            Name = "Assistant Lecturer"
                        },
                        new
                        {
                            Id = (byte)3,
                            Name = "Professor"
                        },
                        new
                        {
                            Id = (byte)4,
                            Name = "Associate Professor"
                        });
                });

            modelBuilder.Entity("Entity_Layer.GradeLetter", b =>
                {
                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Grade");

                    b.ToTable("GradeLetters");

                    b.HasData(
                        new
                        {
                            Grade = "A+"
                        },
                        new
                        {
                            Grade = "A"
                        },
                        new
                        {
                            Grade = "A-"
                        },
                        new
                        {
                            Grade = "B+"
                        },
                        new
                        {
                            Grade = "B"
                        },
                        new
                        {
                            Grade = "B-"
                        },
                        new
                        {
                            Grade = "C+"
                        },
                        new
                        {
                            Grade = "C"
                        },
                        new
                        {
                            Grade = "C-"
                        },
                        new
                        {
                            Grade = "D+"
                        },
                        new
                        {
                            Grade = "D"
                        },
                        new
                        {
                            Grade = "D-"
                        },
                        new
                        {
                            Grade = "F"
                        });
                });

            modelBuilder.Entity("Entity_Layer.Room", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = "A-101"
                        },
                        new
                        {
                            Id = "A-102"
                        },
                        new
                        {
                            Id = "A-103"
                        },
                        new
                        {
                            Id = "A-104"
                        },
                        new
                        {
                            Id = "B-101"
                        },
                        new
                        {
                            Id = "B-102"
                        },
                        new
                        {
                            Id = "B-103"
                        },
                        new
                        {
                            Id = "B-104"
                        },
                        new
                        {
                            Id = "C-101"
                        },
                        new
                        {
                            Id = "C-102"
                        },
                        new
                        {
                            Id = "C-103"
                        },
                        new
                        {
                            Id = "C-104"
                        },
                        new
                        {
                            Id = "D-101"
                        },
                        new
                        {
                            Id = "D-102"
                        },
                        new
                        {
                            Id = "D-103"
                        },
                        new
                        {
                            Id = "D-104"
                        });
                });

            modelBuilder.Entity("Entity_Layer.Semister", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Semisters");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            Name = "1st"
                        },
                        new
                        {
                            Id = (byte)2,
                            Name = "2nd"
                        },
                        new
                        {
                            Id = (byte)3,
                            Name = "3rd"
                        },
                        new
                        {
                            Id = (byte)4,
                            Name = "4th"
                        },
                        new
                        {
                            Id = (byte)5,
                            Name = "5th"
                        },
                        new
                        {
                            Id = (byte)6,
                            Name = "6th"
                        },
                        new
                        {
                            Id = (byte)7,
                            Name = "7th"
                        },
                        new
                        {
                            Id = (byte)8,
                            Name = "8th"
                        });
                });

            modelBuilder.Entity("Entity_Layer.Student", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Contact")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RegistrationNumber")
                        .IsUnique();

                    b.ToTable("Students");

                    b.HasCheckConstraint("CHK_StudentEmailInCorrectFormat", "Email like '%_@_%._%'");

                    b.HasCheckConstraint("CHK_RegistrationNumberMinLength", "LEN(RegistrationNumber) between 11 and 13");
                });

            modelBuilder.Entity("Entity_Layer.StudentCourse", b =>
                {
                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("CourseCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DepartmentId", "CourseCode", "StudentId");

                    b.HasIndex("Grade");

                    b.HasIndex("StudentId");

                    b.HasIndex("CourseCode", "DepartmentId");

                    b.ToTable("StudentsCourses");
                });

            modelBuilder.Entity("Entity_Layer.StudentsCoursesHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CourseCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.Property<int>("UnassignCoursesCountId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Grade");

                    b.HasIndex("StudentId");

                    b.HasIndex("UnassignCoursesCountId");

                    b.HasIndex("CourseCode", "DepartmentId");

                    b.ToTable("StudentsCoursesHistories");
                });

            modelBuilder.Entity("Entity_Layer.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Contact")
                        .HasColumnType("bigint");

                    b.Property<float>("CreditToBeTaken")
                        .HasColumnType("real");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<byte>("DesignationId")
                        .HasColumnType("tinyint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("RemainingCredit")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("DesignationId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Teachers");

                    b.HasCheckConstraint("CHK_TeacherEmailInCorrectFormat", "Email like '%_@_%._%'");

                    b.HasCheckConstraint("CHK_TeacherContactInCorrectFormat", "LEN(CAST(Contact as varchar(max))) between 6 and 15");

                    b.HasCheckConstraint("CHK_CreditToBeTakenByTeacher", "CreditToBeTaken !< 0");

                    b.HasCheckConstraint("CHK_RemainingCreditOfTeacher", "RemainingCredit BETWEEN 0 AND CreditToBeTaken");
                });

            modelBuilder.Entity("Entity_Layer.UnallocatingRoomsCount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("UnallocatingRoomsCounts");
                });

            modelBuilder.Entity("Entity_Layer.UnassignCoursesCount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("UnassignCoursesCounts");
                });

            modelBuilder.Entity("Entity_Layer.AllocateClassroom", b =>
                {
                    b.HasOne("Entity_Layer.Course", "Course")
                        .WithMany("AllocateClassrooms")
                        .HasForeignKey("CourseCode")
                        .HasPrincipalKey("Code")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity_Layer.Day", "Day")
                        .WithMany("AllocateClassrooms")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity_Layer.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity_Layer.Room", "Room")
                        .WithMany("AllocateClassrooms")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Day");

                    b.Navigation("Department");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Entity_Layer.AllocateClassroomHistory", b =>
                {
                    b.HasOne("Entity_Layer.Day", "Day")
                        .WithMany()
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity_Layer.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity_Layer.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity_Layer.UnallocatingRoomsCount", "UnallocatingRoomsCount")
                        .WithMany()
                        .HasForeignKey("UnallocatingRoomsCountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity_Layer.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseCode1", "CourseDepartmentId");

                    b.Navigation("Course");

                    b.Navigation("Day");

                    b.Navigation("Department");

                    b.Navigation("Room");

                    b.Navigation("UnallocatingRoomsCount");
                });

            modelBuilder.Entity("Entity_Layer.Course", b =>
                {
                    b.HasOne("Entity_Layer.Department", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity_Layer.Semister", "Semister")
                        .WithMany()
                        .HasForeignKey("SemisterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity_Layer.Teacher", "Teacher")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId");

                    b.Navigation("Department");

                    b.Navigation("Semister");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Entity_Layer.CourseHistory", b =>
                {
                    b.HasOne("Entity_Layer.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity_Layer.Semister", "Semister")
                        .WithMany()
                        .HasForeignKey("SemisterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity_Layer.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId");

                    b.HasOne("Entity_Layer.UnassignCoursesCount", "UnassignCoursesCount")
                        .WithMany()
                        .HasForeignKey("UnassignCoursesCountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Semister");

                    b.Navigation("Teacher");

                    b.Navigation("UnassignCoursesCount");
                });

            modelBuilder.Entity("Entity_Layer.Student", b =>
                {
                    b.HasOne("Entity_Layer.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Entity_Layer.StudentCourse", b =>
                {
                    b.HasOne("Entity_Layer.GradeLetter", "GradeLetter")
                        .WithMany()
                        .HasForeignKey("Grade");

                    b.HasOne("Entity_Layer.Student", "Student")
                        .WithMany("StudentsCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity_Layer.Course", "Course")
                        .WithMany("StudentsCourses")
                        .HasForeignKey("CourseCode", "DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("GradeLetter");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Entity_Layer.StudentsCoursesHistory", b =>
                {
                    b.HasOne("Entity_Layer.GradeLetter", "GradeLetter")
                        .WithMany()
                        .HasForeignKey("Grade");

                    b.HasOne("Entity_Layer.Student", "Student")
                        .WithMany("StudentsCoursesHistories")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity_Layer.UnassignCoursesCount", "UnassignCoursesCount")
                        .WithMany()
                        .HasForeignKey("UnassignCoursesCountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity_Layer.Course", "Course")
                        .WithMany("StudentsCoursesHistories")
                        .HasForeignKey("CourseCode", "DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("GradeLetter");

                    b.Navigation("Student");

                    b.Navigation("UnassignCoursesCount");
                });

            modelBuilder.Entity("Entity_Layer.Teacher", b =>
                {
                    b.HasOne("Entity_Layer.Department", "Department")
                        .WithMany("Teachers")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity_Layer.Designation", "Designation")
                        .WithMany("Teachers")
                        .HasForeignKey("DesignationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Designation");
                });

            modelBuilder.Entity("Entity_Layer.Course", b =>
                {
                    b.Navigation("AllocateClassrooms");

                    b.Navigation("StudentsCourses");

                    b.Navigation("StudentsCoursesHistories");
                });

            modelBuilder.Entity("Entity_Layer.Day", b =>
                {
                    b.Navigation("AllocateClassrooms");
                });

            modelBuilder.Entity("Entity_Layer.Department", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("Entity_Layer.Designation", b =>
                {
                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("Entity_Layer.Room", b =>
                {
                    b.Navigation("AllocateClassrooms");
                });

            modelBuilder.Entity("Entity_Layer.Student", b =>
                {
                    b.Navigation("StudentsCourses");

                    b.Navigation("StudentsCoursesHistories");
                });

            modelBuilder.Entity("Entity_Layer.Teacher", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}