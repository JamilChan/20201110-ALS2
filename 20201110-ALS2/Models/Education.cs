using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class Education {

    public long? EducationId { get; set; }
    [Required(ErrorMessage = "Indtast et navn for uddannelsen.")]
    public string Name { get; set; }
    public ICollection<Student> Students { get; set; }
  }
}
