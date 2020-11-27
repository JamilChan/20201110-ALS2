using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _20201110_ALS2.Models {
  public static class ModelBuilderExtension {
    public static void SeedEducators(this ModelBuilder modelBuilder) {
      modelBuilder.Entity<Educator>().HasData(
      new Educator { EducatorId = 1, Name = "God Flemse" },
      new Educator { EducatorId = 2, Name = "Big Daddy D" }
      );
    }

    public static void SeedEducations(this ModelBuilder modelBuilder) {
      modelBuilder.Entity<Education>().HasData(
      new Education { EducationId = 1, Name = "Datamatiker" },
      new Education { EducationId = 2, Name = "Finansøkonom" }
      );
    }

    public static void SeedAdmin(this ModelBuilder modelBuilder, AlsDbContext context) {
      PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();

      string adminUser = "admin";
      string adminPassword = "Secret123$";
      IdentityUser user = new IdentityUser {
        Id = "1", UserName = adminUser, NormalizedUserName = "ADMIN",
        PasswordHash = hasher.HashPassword(null, adminPassword)
      };
      IdentityRole role = new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" };

      modelBuilder.Entity<IdentityUser>().HasData(user);
      modelBuilder.Entity<IdentityRole>().HasData(role);
      modelBuilder.Entity<IdentityUserRole<string>>().HasData(
        new IdentityUserRole<string> { RoleId = role.Id, UserId = user.Id }
      );
    }
    public static void EtEllerAndet(this ModelBuilder modelBuilder) {
      modelBuilder.Entity<StudentCourse>().HasKey(k => new { k.CourseId, k.StudentId });
    }

    //public static void SeedStudents(this ModelBuilder modelBuilder) {
    //  modelBuilder.Entity<Student>().HasData(
    //    new Student { StudentId = 1, Name = "Mathias", Education = "Computer Science", Semester = 3 },
    //    new Student { StudentId = 2, Name = "Hans", Education = "Computer Science", Semester = 3 },
    //    new Student { StudentId = 3, Name = "Claus", Education = "Computer Science", Semester = 3 }
    //  );
    //}
  }
}
