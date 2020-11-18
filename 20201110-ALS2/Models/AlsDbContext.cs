using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _20201110_ALS2.Models {
  public class AlsDbContext : DbContext {
    public AlsDbContext(DbContextOptions<AlsDbContext> options) : base(options) {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Educator> Educators { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.SeedEducators();
    }
  }
}
