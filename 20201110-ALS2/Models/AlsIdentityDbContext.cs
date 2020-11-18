using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _20201110_ALS2.Models {
  public class AlsIdentityDbContext : IdentityDbContext<IdentityUser> {
    public AlsIdentityDbContext(DbContextOptions<AlsIdentityDbContext> options) : base(options) {

    }
  }
}
