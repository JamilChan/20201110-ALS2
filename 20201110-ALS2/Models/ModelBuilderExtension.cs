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
    public static void SeedAdmin(this ModelBuilder modelBuilder)
    {
      string adminUser = "admin";
      string adminPassword = "Secret123$";
      
      PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();
      modelBuilder.Entity<IdentityUser>().HasData(
      new IdentityUser { Id = "1", UserName = adminUser, NormalizedUserName = "ADMIN", PasswordHash = hasher.HashPassword(null, adminPassword) }
        );
    }
  }
}
