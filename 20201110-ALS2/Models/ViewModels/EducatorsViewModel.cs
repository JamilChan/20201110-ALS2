using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models.ViewModels {
  public class EducatorsViewModel {
    public IQueryable<Educator> AllEducators { get; set; }

    public IEnumerable<IdentityRole> AllRoles { get; set; }

    public List<string> AllUserIds { get; set; }
  }
}
