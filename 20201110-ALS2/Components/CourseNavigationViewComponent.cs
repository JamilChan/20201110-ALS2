using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20201110_ALS2.Models;
using _20201110_ALS2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace _20201110_ALS2.Components {
  public class CourseNavigationViewComponent : ViewComponent {
    public ICourseRepository courseRepo { get; }
    public IEducationRepository educationRepo { get; }

    public CourseNavigationViewComponent(ICourseRepository courseRepo, IEducationRepository educationRepo) {
      this.courseRepo = courseRepo;
      this.educationRepo = educationRepo;
    }

    public IViewComponentResult Invoke(string chartView) {
      return View("Default", new CourseEducationNavigationViewModel{
        Courses = courseRepo.Courses,
        Educations = educationRepo.Educations,
        ChartView = chartView
      });
    }
  }
}
