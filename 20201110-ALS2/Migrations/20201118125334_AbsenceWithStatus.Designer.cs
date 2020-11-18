﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _20201110_ALS2.Models;

namespace _20201110_ALS2.Migrations
{
    [DbContext(typeof(AlsDbContext))]
    [Migration("20201118125334_AbsenceWithStatus")]
    partial class AbsenceWithStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("_20201110_ALS2.Models.Absence", b =>
                {
                    b.Property<int>("AbsenceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("StudentId")
                        .HasColumnType("bigint");

                    b.HasKey("AbsenceId");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Absences");
                });

            modelBuilder.Entity("_20201110_ALS2.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("EducatorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CourseId");

                    b.HasIndex("EducatorId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("_20201110_ALS2.Models.Educator", b =>
                {
                    b.Property<int>("EducatorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EducatorId");

                    b.ToTable("Educator");
                });

            modelBuilder.Entity("_20201110_ALS2.Models.Student", b =>
                {
                    b.Property<long>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Education")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Semester")
                        .HasColumnType("int");

                    b.HasKey("StudentId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            StudentId = 1L,
                            Education = "Computer Science",
                            Name = "Mathias",
                            Semester = 3
                        },
                        new
                        {
                            StudentId = 2L,
                            Education = "Computer Science",
                            Name = "Hans",
                            Semester = 3
                        },
                        new
                        {
                            StudentId = 3L,
                            Education = "Computer Science",
                            Name = "Claus",
                            Semester = 3
                        });
                });

            modelBuilder.Entity("_20201110_ALS2.Models.Absence", b =>
                {
                    b.HasOne("_20201110_ALS2.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId");

                    b.HasOne("_20201110_ALS2.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("_20201110_ALS2.Models.Course", b =>
                {
                    b.HasOne("_20201110_ALS2.Models.Educator", "Educator")
                        .WithMany()
                        .HasForeignKey("EducatorId");

                    b.Navigation("Educator");
                });
#pragma warning restore 612, 618
        }
    }
}