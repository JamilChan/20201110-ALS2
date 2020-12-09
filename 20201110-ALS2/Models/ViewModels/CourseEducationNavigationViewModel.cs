using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models.ViewModels {
  public class CourseEducationNavigationViewModel {
    public IQueryable<Course> Courses { get; set; }
    public IQueryable<Education> Educations { get; set; }
    public string ChartView { get; set; }
  }
}
