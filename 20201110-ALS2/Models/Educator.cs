using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _20201110_ALS2.Models {
  public class Educator{
    public long EducatorId { get; set; }

    [Required(ErrorMessage = "Indtast venligst navn på underviser")]
    public string Name { get; set; }

    public override string ToString() {
      return Name;
    }
  }
}
