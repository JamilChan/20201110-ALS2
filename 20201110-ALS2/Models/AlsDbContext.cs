using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _20201110_ALS2.Models {
  public class AlsDbContext : IdentityDbContext {
    public AlsDbContext(DbContextOptions<AlsDbContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }
    public DbSet<Absence> Absences { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Educator> Educators { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<Week> Weeks { get; set; }
    public DbSet<Education> Educations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Education>().HasMany(e => e.Students).WithOne(s => s.Education)
          .HasForeignKey(s => s.EducationId)
          .OnDelete(DeleteBehavior.NoAction);

      modelBuilder.Entity<StudentCourse>()
          .HasKey(sc => new { sc.StudentId, sc.CourseId });
      modelBuilder.Entity<StudentCourse>()
          .HasOne(sc => sc.Student)
          .WithMany(s => s.StudentCourses)
          .HasForeignKey(sc => sc.StudentId);
      modelBuilder.Entity<StudentCourse>()
          .HasOne(sc => sc.Course)
          .WithMany(c => c.StudentCourses)
          .HasForeignKey(sc => sc.CourseId);

      //foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) {
      //  foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
      //}

      modelBuilder.Entity<Absence>().HasOne(a => a.Course).WithMany().OnDelete(DeleteBehavior.SetNull);
      modelBuilder.Entity<Absence>().HasOne(a => a.Student).WithMany().OnDelete(DeleteBehavior.Cascade);

      modelBuilder.SeedEducators();
      modelBuilder.SeedEducations();
      modelBuilder.SeedAdmin(this);
    }
  }
}
