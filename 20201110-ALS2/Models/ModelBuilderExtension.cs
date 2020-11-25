using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace _20201110_ALS2.Models {
  public static class ModelBuilderExtension {
    public static void SeedEducators(this ModelBuilder modelBuilder) {
      modelBuilder.Entity<Educator>().HasData(
      new Educator { EducatorId = 1, Name = "God Flemse" },
      new Educator { EducatorId = 2, Name = "Big Daddy D" }
      );
    }
    public static void EtEllerAndet(this ModelBuilder modelBuilder) {
      modelBuilder.Entity<StudentCourse>().HasKey(k => new { k.CourseId, k.StudentId });
    }

    public static void SeedStudents(this ModelBuilder modelBuilder) {
      modelBuilder.Entity<Student>().HasData(
        new Student { StudentId = 1, Name = "Mathias", Education = "Computer Science", Semester = 3 },
        new Student { StudentId = 2, Name = "Hans", Education = "Computer Science", Semester = 3 },
        new Student { StudentId = 3, Name = "Claus", Education = "Computer Science", Semester = 3 }
      );
    }
  }
}
