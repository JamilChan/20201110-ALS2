using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace _20201110_ALS2.Models {
  public static class IdentitySeedData {
    private const string adminUser = "Admin";
    private const string adminPassword = "godflemse";

    public static async void EnsurePopulated(IApplicationBuilder applicationBuilder) {
      UserManager<IdentityUser> userManager =
        applicationBuilder.ApplicationServices.GetRequiredService<UserManager<IdentityUser>>();

      IdentityUser user = await userManager.FindByIdAsync(adminUser);
      if (user == null) {
        user = new IdentityUser("Admin");
        await userManager.CreateAsync(user, adminPassword);
      }
    }
  }
}
