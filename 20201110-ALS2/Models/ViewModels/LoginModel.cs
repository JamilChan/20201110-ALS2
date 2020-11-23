using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models.ViewModels {
  public class LoginModel {
    [Required]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Kodeord")]
    public string Password { get; set; }

    //[DataType(DataType.Password)]
    //[Display(Name = "Bekræft kodeord")]
    //[Compare("Password", ErrorMessage = "Kodeordet matcher ikke bekræftelsen deraf")]
    //public string ConfirmPassword { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }

    public string ReturnUrl { get; set; }
  }
}
