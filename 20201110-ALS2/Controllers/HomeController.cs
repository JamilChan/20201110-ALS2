using _20201110_ALS2.Models;
using _20201110_ALS2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _20201110_ALS2.Controllers {
  public class HomeController : Controller {
    private readonly ICourseRepository courseRepo;
    private readonly IAbsenceRepository absenceRepo;

    public HomeController(ICourseRepository courseRepo, IAbsenceRepository absenceRepo) {
      this.courseRepo = courseRepo;
      this.absenceRepo = absenceRepo;
    }

    [HttpGet]
    public IActionResult Index() {
      HomeIndexViewModel hi = new HomeIndexViewModel {
        Date = DateTime.Now,
        Courses = new List<Course>()
      };

      List<Course> identityCourses = courseRepo.Courses.ToList();
      dayOfWeekCheck(identityCourses, hi);

      hi.CheckedCourse = absenceRepo.IsChecked(hi.Courses, hi.Date);

      return View("Index", hi);
    }

    [HttpPost]
    public IActionResult Index(HomeIndexViewModel hi) {
      hi.Courses = new List<Course>();
      DateTime dateTime = hi.Date;

      if (hi.Direction == "Backward") {
        dateTime = hi.Date.AddDays(-1);
      } else if (hi.Direction == "Forward") {
        dateTime = hi.Date.AddDays(1);
      }

      hi.Date = dateTime;

      List<Course> identityCourses = courseRepo.Courses.ToList();
      dayOfWeekCheck(identityCourses, hi);

      hi.CheckedCourse = absenceRepo.IsChecked(hi.Courses, hi.Date);

      return View("Index", hi);
    }

    private void dayOfWeekCheck(List<Course> identityCourses, HomeIndexViewModel hi) {

      string[] date = hi.Date.ToString("O").Split("T", 2);
      hi.DateAsString = date[0];

      DayOfWeek dow = hi.Date.DayOfWeek;

      foreach (Course c in identityCourses) {
        if (c.StartDate < hi.Date && hi.Date < c.EndDate) {
          if (dow == DayOfWeek.Monday && c.Week.Monday) {
            hi.Courses.Add(c);
          } else if (dow == DayOfWeek.Tuesday && c.Week.Tuesday) {
            hi.Courses.Add(c);
          } else if (dow == DayOfWeek.Wednesday && c.Week.Wednesday) {
            hi.Courses.Add(c);
          } else if (dow == DayOfWeek.Thursday && c.Week.Thursday) {
            hi.Courses.Add(c);
          } else if (dow == DayOfWeek.Friday && c.Week.Friday) {
            hi.Courses.Add(c);
          }
        }
      }
    }
  }
}

