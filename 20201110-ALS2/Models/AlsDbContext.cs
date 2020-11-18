using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _20201110_ALS2.Models {
  public class AlsDbContext : IdentityDbContext {
    public AlsDbContext(DbContextOptions<AlsDbContext> options) : base(options) {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Absence> Absences { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Educator> Educators { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);
      modelBuilder.SeedEducators();
      modelBuilder.Seed();
      //modelBuilder.Entity<Course>().HasOne(c => c.Educator).WithOne(e => e.Name).Map(m => { m.MapLeftKey("EducatorId"); m.MapRightKey("EducatorId"); m.ToTable("Educators") });
    }
  }
}
