using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class Student {
    public long StudentId { get; set; }
    [Required(ErrorMessage = "Udfyld navn")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Udfyld Uddannelse")]
    public Education Education { get; set; }
    [Required(ErrorMessage = "Hvorfor ødelægger du systemet??!!??")]
    public int Semester { get; set; }
    public ICollection<StudentCourse> StudentCourses { get; set; }
  }
}
