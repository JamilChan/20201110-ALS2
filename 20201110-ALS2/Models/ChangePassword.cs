using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class ChangePassword {
    [Required(ErrorMessage = "Dit nuværende kodeord var ikke korrekt")]
    [DataType(DataType.Password)]
    [Display(Name = "Nuværende kodeord")]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = "Et nyt kodeord er påkrævet")]
    [DataType(DataType.Password)]
    [Display(Name = "Nyt kodeord")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Bekræft nyt kodeord")]
    [Compare("NewPassword", ErrorMessage = "Det nye kodeord, og bekræftelsen deraf matcher ikke")]
    public string ConfirmPassword { get; set; }
  }
}
