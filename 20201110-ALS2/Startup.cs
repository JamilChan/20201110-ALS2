using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20201110_ALS2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Westwind.AspNetCore.LiveReload;

namespace _20201110_ALS2 {
  public class Startup {
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services) {
      //Db Contexts
      services.AddDbContext<AlsDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("AlsDbConnection")));

      services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AlsDbContext>();

      //Dependancy Injected Repositories
      services.AddScoped<IEducatorRepository, SqlEducatorRepository>();

      services.AddControllersWithViews(options =>
      {
        AuthorizationPolicy policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        options.Filters.Add(new AuthorizeFilter(policy));
      });

      //LIVE UPDATE STUFF STARTS HERE
      services.AddLiveReload(config => { });
      services.AddRazorPages().AddRazorRuntimeCompilation();
      services.AddMvc().AddRazorRuntimeCompilation();
      //ENDS HERE
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

      //LIVE UPDATE STUFF STARTS HERE
      app.UseLiveReload();
      app.UseStaticFiles();
      //ENDS HERE

      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      } else {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseRouting();

      app.UseAuthorization();
      app.UseAuthentication();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
