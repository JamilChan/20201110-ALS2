using _20201110_ALS2.Models;
using _20201110_ALS2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace _20201110_ALS2.Controllers {
  public class HomeController : Controller {
    private readonly ICourseRepository courseRepository;

    public HomeController(ICourseRepository courseRepository) {
      this.courseRepository = courseRepository;
    }

    [HttpGet]
    public IActionResult Index() {
      HomeIndexViewModel hi = new HomeIndexViewModel {
        Date = DateTime.Now,
        Courses = new List<Course>()
      };

      List<Course> identityCourses = testCourses();
      dayOfWeekCheck(identityCourses, hi);

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

      List<Course> identityCourses = testCourses();
      dayOfWeekCheck(identityCourses, hi);

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

    private List<Course> testCourses() {
      //Test Course Delete Later! Something get on IdentityUser
      Course c1 = new Course {
        CourseId = 1,
        Name = "ProtekTest",
        Educator = null,
        Week = new Week {
          WeekId = 1,
          Monday = false,
          Tuesday = true,
          Wednesday = false,
          Thursday = true,
          Friday = false
        },
        StartDate = DateTime.Today.AddMonths(-1),
        EndDate = DateTime.Today.AddMonths(1)
      };

      Course c2 = new Course {
        CourseId = 2,
        Name = "SysTest",
        Educator = null,
        Week = new Week {
          WeekId = 2,
          Monday = true,
          Tuesday = false,
          Wednesday = false,
          Thursday = true,
          Friday = true
        },
        StartDate = DateTime.Today.AddMonths(-1),
        EndDate = DateTime.Today.AddMonths(1)
      };

      List<Course> identityCourses = new List<Course>();
      identityCourses.Add(c1);
      identityCourses.Add(c2);
      //Test Course Delete Later!

      return identityCourses;
    }
  }
}

