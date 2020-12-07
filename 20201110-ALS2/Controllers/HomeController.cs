using _20201110_ALS2.Models;
using _20201110_ALS2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using _20201110_ALS2.Models.DAL.Absence;
using _20201110_ALS2.Models.DAL.Course;

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
      HomeIndexViewModel model = new HomeIndexViewModel {
        Date = DateTime.Now,
        CourseList = new List<Course>()
      };

      List<Course> identityCourses = courseRepo.Courses.ToList();
      DayOfWeekCheck(identityCourses, model);

      model.CheckedCourse = absenceRepo.CourseHasAbsence(model.CourseList, model.Date);

      return View("Index", model);
    }

    [HttpPost]
    public IActionResult Index(HomeIndexViewModel model) {
      model.CourseList = new List<Course>();
      DateTime dateTime = model.Date;

      if (model.Direction == "Backward") {
        dateTime = model.Date.AddDays(-1);
      } else if (model.Direction == "Forward") {
        dateTime = model.Date.AddDays(1);
      }

      model.Date = dateTime;

      List<Course> identityCourses = courseRepo.Courses.ToList();
      DayOfWeekCheck(identityCourses, model);

      model.CheckedCourse = absenceRepo.CourseHasAbsence(model.CourseList, model.Date);

      return View("Index", model);
    }

    private void DayOfWeekCheck(List<Course> identityCourses, HomeIndexViewModel model) {

      string[] date = model.Date.ToString("O").Split("T", 2);
      model.DateAsString = date[0];

      DayOfWeek dayOfWeek = model.Date.DayOfWeek;

      foreach (Course course in identityCourses) {
        if (course.StartDate < model.Date && model.Date < course.EndDate) {
          if (dayOfWeek == DayOfWeek.Monday && course.Week.Monday) {
            model.CourseList.Add(course);
          } else if (dayOfWeek == DayOfWeek.Tuesday && course.Week.Tuesday) {
            model.CourseList.Add(course);
          } else if (dayOfWeek == DayOfWeek.Wednesday && course.Week.Wednesday) {
            model.CourseList.Add(course);
          } else if (dayOfWeek == DayOfWeek.Thursday && course.Week.Thursday) {
            model.CourseList.Add(course);
          } else if (dayOfWeek == DayOfWeek.Friday && course.Week.Friday) {
            model.CourseList.Add(course);
          }
        }
      }
    }
  }
}

