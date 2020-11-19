using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class Course {
    public int CourseId { get; set; }
    public string Name { get; set; }
    public Educator Educator { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Week Week { get; set; }
  }
}
