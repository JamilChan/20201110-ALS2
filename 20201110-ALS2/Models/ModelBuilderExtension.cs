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
    public static void SeedAdmin(this ModelBuilder modelBuilder, AlsDbContext context) {
      PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();

      string adminUser = "admin";
      string adminPassword = "Secret123$";
      IdentityUser user = new IdentityUser {
        Id = "1", UserName = adminUser, NormalizedUserName = "ADMIN",
        PasswordHash = hasher.HashPassword(null, adminPassword)
      };
      IdentityRole role = new IdentityRole {Id = "1", Name = "Admin", NormalizedName = "ADMIN"};

      modelBuilder.Entity<IdentityUser>().HasData(user);
      modelBuilder.Entity<IdentityRole>().HasData(role);
      context.UserRoles.Add(new IdentityUserRole<string> {RoleId = role.Id, UserId = user.Id});

    }
  }
}
