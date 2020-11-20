using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class StudentCourse {
    public long CourseId { get; set; }
    public Course Course { get; set; }
    public long StudentId { get; set; }
    public Student Student { get; set; }
    
  }
}
