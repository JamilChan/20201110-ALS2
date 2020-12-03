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

      //Dependancy Injected Repositories
      services.AddScoped<IAbsenceRepository, EfAbsenceRepository>();
      services.AddScoped<IStudentRepository, EfStudentRepository>();
      services.AddScoped<ICourseRepository, EfCourseRepository>();
      services.AddScoped<IEducationRepository, EfEducationRepository>();
      services.AddScoped<IEducatorRepository, EfEducatorRepository>();

      services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AlsDbContext>();
      //services.AddControllersWithViews(options => {
      //  AuthorizationPolicy policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
      //  options.Filters.Add(new AuthorizeFilter(policy));
      //});

      services.AddAuthorization(
        options =>
        {
          options.AddPolicy("SeFagPolicy", policy => policy.RequireClaim("Se Fag"));
          options.AddPolicy("HåndterFagPolicy", policy => policy.RequireClaim("Håndter Fag"));
          options.AddPolicy("SletFagPolicy", policy => policy.RequireClaim("Slet Fag"));
          options.AddPolicy("FraværPolicy", policy => policy.RequireClaim("Giv Fravær"));
          options.AddPolicy("HåndterStuderendePolicy", policy => policy.RequireClaim("Håndter Studerende"));
          options.AddPolicy("SletStuderendePolicy", policy => policy.RequireClaim("Slet Studerende"));
          options.AddPolicy("SeStuderendePolicy", policy => policy.RequireClaim("Se Studerende"));
        });

      //LIVE UPDATE STUFF STARTS HERE
      services.AddLiveReload(config => { });
      services.AddRazorPages().AddRazorRuntimeCompilation();
      services.AddMvc().AddRazorRuntimeCompilation();
      services.AddMemoryCache();
      services.AddSession();
      //ENDS HERE
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      //LIVE UPDATE STUFF STARTS HERE
      app.UseLiveReload();
      app.UseStaticFiles();
      //ENDS HERE
      app.UseSession();


      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      } else {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseAuthentication();

      app.UseRouting();
      app.UseAuthorization();


      app.UseEndpoints(endpoints => {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
