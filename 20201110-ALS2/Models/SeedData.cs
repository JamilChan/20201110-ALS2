using System.Collections.Generic;
using System.Linq;
using System.Xml;
using _20201110_ALS2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace _20201110_ALS2.Models {
  public static class SeedData {

    public static void Seed(this ModelBuilder modelBuilder) {
      modelBuilder.Entity<Student>().HasData(
            new Student { StudentId = 1, Name = "Mathias", Education = "Computer Science", Semester = 3 },
            new Student { StudentId = 2, Name = "Hans", Education = "Computer Science", Semester = 3 },
            new Student { StudentId = 3, Name = "Claus", Education = "Computer Science", Semester = 3 }
        );
    }
  }
}

