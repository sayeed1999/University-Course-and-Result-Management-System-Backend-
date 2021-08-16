﻿// <auto-generated />
using System;
using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data_Access_Layer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.HasCheckConstraint("CHK_LengthOfDeptName", "len(name) >= 3");

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

                    b.Property<int>("DesignationId")
                        .HasColumnType("int");

                    b.Property<byte?>("DesignationId1")
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

                    b.HasIndex("DesignationId1");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Teachers");

                    b.HasCheckConstraint("CHK_TeacherEmailInCorrectFormat", "Email like '%_@_%._%'");

                    b.HasCheckConstraint("CHK_TeacherContactInCorrectFormat", "LEN(CAST(Contact as varchar(max))) between 6 and 15");

                    b.HasCheckConstraint("CHK_CreditToBeTakenByTeacher", "CreditToBeTaken !< 0");

                    b.HasCheckConstraint("CHK_RemainingCreditOfTeacher", "RemainingCredit BETWEEN 0 AND CreditToBeTaken");
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

            modelBuilder.Entity("Entity_Layer.Teacher", b =>
                {
                    b.HasOne("Entity_Layer.Department", "Department")
                        .WithMany("Teachers")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity_Layer.Designation", "Designation")
                        .WithMany()
                        .HasForeignKey("DesignationId1");

                    b.Navigation("Department");

                    b.Navigation("Designation");
                });

            modelBuilder.Entity("Entity_Layer.Department", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("Entity_Layer.Teacher", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
