using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class StudentCourse {
    [ForeignKey("Course")]
    public long CourseId { get; set; }
    public Course Course { get; set; }
    [ForeignKey("Student")]
    public long StudentId { get; set; }
    public Student Student { get; set; }
    
  }
}
