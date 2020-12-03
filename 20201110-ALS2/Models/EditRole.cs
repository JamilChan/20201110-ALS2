using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _20201110_ALS2.Models {
  public class EditRole {
    public string RoleId { get; set; }

    [Required(ErrorMessage = "Rolle navn er påkrævet")]
    public string RoleName { get; set; }

    public List<string> AllUsers { get; set; } = new List<string>();
    public List<string> RoleClaims { get; set; } = new List<string>();
  }
}
