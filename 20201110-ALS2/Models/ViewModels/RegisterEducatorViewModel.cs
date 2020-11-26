using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _20201110_ALS2.Models.ViewModels {
  public class RegisterEducatorViewModel {
    public Educator Educator { get; set; }

    [Required(ErrorMessage = "Angiv et brugernavn")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Indtast et kodeord")]
    [DataType(DataType.Password)]
    [Display(Name = "Kodeord")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Bekræft kodeord")]
    [Compare("Password", ErrorMessage = "Kodeordet matcher ikke bekræftelsen deraf")]
    public string ConfirmPassword { get; set; }
  }
}
