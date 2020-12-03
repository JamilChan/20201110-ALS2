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
        new Educator { EducatorId = 1, Name = "John Johnson" },
        new Educator { EducatorId = 3, Name = "Flemming" },
        new Educator { EducatorId = 2, Name = "Hans Hansen" }
      );
    }

    public static void SeedEducations(this ModelBuilder modelBuilder) {
      modelBuilder.Entity<Education>().HasData(
        new Education { EducationId = 1, Name = "Datamatiker" },
        new Education { EducationId = 2, Name = "Finansøkonom" }
      );
    }

    public static void SeedAdmin(this ModelBuilder modelBuilder, AlsDbContext context) {
      PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();

      string adminUser = "admin";
      string adminPassword = "Secret123$";
      ApplicationUser user = new ApplicationUser { UserName = adminUser, NormalizedUserName = "ADMIN",
        PasswordHash = hasher.HashPassword(null, adminPassword), EducatorId = 1
      };
      IdentityRole adminRole = new IdentityRole {Id = "1", Name = "Admin", NormalizedName = "ADMIN" };
      IdentityRole userRole = new IdentityRole { Name = "User", NormalizedName = "USER" };

      modelBuilder.Entity<ApplicationUser>().HasData(user);
      modelBuilder.Entity<IdentityRole>().HasData(adminRole, userRole);
      modelBuilder.Entity<IdentityUserRole<string>>().HasData(
        new IdentityUserRole<string> { RoleId = adminRole.Id, UserId = user.Id }
      );
      modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(
        new IdentityRoleClaim<string> { ClaimType = "Håndter Studerende", ClaimValue = "Håndter Studerende", RoleId = "1", Id = 1 },
        new IdentityRoleClaim<string> { ClaimType = "Se Studerende", ClaimValue = "Se Studerende", RoleId = "1", Id = 2 },
        new IdentityRoleClaim<string> { ClaimType = "Slet Studerende", ClaimValue = "Slet Studerende", RoleId = "1", Id = 3 },
        new IdentityRoleClaim<string> { ClaimType = "Håndter Fag", ClaimValue = "Håndter Fag", RoleId = "1", Id = 4 },
        new IdentityRoleClaim<string> { ClaimType = "Se Fag", ClaimValue = "Se Fag", RoleId = "1", Id = 5},
        new IdentityRoleClaim<string> { ClaimType = "Slet Fag", ClaimValue = "Slet Fag", RoleId = "1", Id = 6 },
        new IdentityRoleClaim<string> { ClaimType = "Giv Fravær", ClaimValue = "Giv Fravær", RoleId = "1", Id = 7 }

      );
    }
    //public static void EtEllerAndet(this ModelBuilder modelBuilder) {
    //  modelBuilder.Entity<StudentCourse>().HasKey(k => new { k.CourseId, k.StudentId });
    //}

    //public static void SeedStudents(this ModelBuilder modelBuilder) {
    //  modelBuilder.Entity<Student>().HasData(
    //    new Student { StudentId = 1, Name = "Mathias", Education = "Computer Science", Semester = 3 },
    //    new Student { StudentId = 2, Name = "Hans", Education = "Computer Science", Semester = 3 },
    //    new Student { StudentId = 3, Name = "Claus", Education = "Computer Science", Semester = 3 }
    //  );
    //}
  }
}
