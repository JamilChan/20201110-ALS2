using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models.ViewModels {
  public class HomeIndexViewModel {

    public DateTime Date { get; set; }
    public string DateAsString { get; set; }
    public string Direction { get; set; }
    public List<Course> Courses { get; set; }
    public Dictionary<Course, bool> CheckedCourse { get; set; } = new Dictionary<Course, bool>();
  }
}