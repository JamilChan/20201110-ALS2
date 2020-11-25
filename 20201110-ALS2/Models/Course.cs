using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class Course {
    public long CourseId { get; set; }
    [Required]
    public string Name { get; set; }
    [ForeignKey("Educator")]
    public long EducatorId { get; set; }
    public Educator Educator { get; set; }
    [ForeignKey("Week")]
    public long WeekId { get; set; }
    public Week Week { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    public ICollection<StudentCourse> StudentCourses { get; set; }
  }
}
