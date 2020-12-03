using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _20201110_ALS2.Models {
  public class ApplicationUser : IdentityUser {

    public long EducatorId { get; set; }
    public Educator Educator { get; set; }
  }
}
