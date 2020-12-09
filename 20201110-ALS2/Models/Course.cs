using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class Course {
    public long CourseId { get; set; }
    [Required(ErrorMessage = "Vælg et navn kurset.")]
    public string Name { get; set; }
    [ForeignKey("Educator")]
    public long EducatorId { get; set; }
    public Educator Educator { get; set; }
    [ForeignKey("Education")]
    public long EducationId { get; set; }
    public Education Education { get; set; }
    [ForeignKey("Week")]
    public long WeekId { get; set; }
    [Required]
    public Week Week { get; set; }

    [Required(ErrorMessage = "Hvorfor ødelægger du systemet??!!??")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; } = DateTime.Today;
    [Required(ErrorMessage = "Hvorfor ødelægger du systemet??!!??")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);
    public ICollection<StudentCourse> StudentCourses { get; set; }
  }
}
