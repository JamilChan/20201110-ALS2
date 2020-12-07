using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class CreateRole {
    [Required(ErrorMessage = "Indtast et rolle navn")]
    public string RoleName { get; set; }
  }
}
