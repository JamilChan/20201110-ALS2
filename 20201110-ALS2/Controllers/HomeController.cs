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
    private readonly IEducatorRepository educatorRepo;
    private readonly IEducationRepository educationRepo;

    public HomeController(ICourseRepository courseRepo, IAbsenceRepository absenceRepo, IEducatorRepository educatorRepo, IEducationRepository educationRepo) {
      this.courseRepo = courseRepo;
      this.absenceRepo = absenceRepo;
      this.educatorRepo = educatorRepo;
      this.educationRepo = educationRepo;
    }

    [HttpGet]
    public IActionResult Index() {
      Educator educator = educatorRepo.Get(2);
      List<Education> identityEducation = educationRepo.EducationsByEducator(educator);

      HomeIndexViewModel model = new HomeIndexViewModel {
        Date = DateTime.Now,
        IsChecked = false,
        CourseList = new List<Course>(),
        EducationList = new List<Education>()
      };

      foreach (Education education in identityEducation) {
        List<Course> courseList = new List<Course>();
        foreach (Student student in education.Students) {
          foreach (StudentCourse studentCourse in student.StudentCourses) {
            courseList.Add(studentCourse.Course);
          }
        }

        courseList = courseList.Distinct().ToList();
        DayOfWeekCheckEducation(courseList, education, model);
      }

      model.EducationList = model.EducationList.Distinct().ToList();
      model.CheckedEducation = absenceRepo.EducationHasAbsence(model.EducationList, model.Date);

      ViewBag.TypeOfView = "education";

      return View("Index", model);
    }

    [HttpPost]
    public IActionResult Index(HomeIndexViewModel model) {
      Educator educator = educatorRepo.Get(2);
      DateTime dateTime = model.Date;
      model.CourseList = new List<Course>();

      if (model.Direction == "Backward") {
        dateTime = model.Date.AddDays(-1);
      } else if (model.Direction == "Forward") {
        dateTime = model.Date.AddDays(1);
      }

      model.Date = dateTime;

      if (model.IsChecked) {
        List<Course> identityCourses = courseRepo.CoursesByEducator(educator);
        DayOfWeekCheckCourse(identityCourses, model);
        model.CheckedCourse = absenceRepo.CourseHasAbsence(model.CourseList, model.Date);

        ViewBag.TypeOfView = "course";
      } else {
        model.EducationList = new List<Education>();
        List<Education> identityEducation = educationRepo.EducationsByEducator(educator);

        foreach (Education education in identityEducation) {
          List<Course> courseList = new List<Course>();
          foreach (Student student in education.Students) {
            foreach (StudentCourse studentCourse in student.StudentCourses) {
              courseList.Add(studentCourse.Course);
            }
          }

          courseList = courseList.Distinct().ToList();
          DayOfWeekCheckEducation(courseList, education, model);
        }

        model.EducationList = model.EducationList.Distinct().ToList();
        model.CheckedEducation = absenceRepo.EducationHasAbsence(model.EducationList, model.Date);

        ViewBag.TypeOfView = "education";
      }

      return View("Index", model);
    }

    private void DayOfWeekCheckCourse(List<Course> identityCourses, HomeIndexViewModel model) {
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

    private void DayOfWeekCheckEducation(List<Course> identityCourses, Education education, HomeIndexViewModel model) {
      string[] date = model.Date.ToString("O").Split("T", 2);
      model.DateAsString = date[0];

      DayOfWeek dayOfWeek = model.Date.DayOfWeek;

      foreach (Course course in identityCourses) {
        if (course.StartDate < model.Date && model.Date < course.EndDate) {
          if (dayOfWeek == DayOfWeek.Monday && course.Week.Monday) {
            model.EducationList.Add(education);
          } else if (dayOfWeek == DayOfWeek.Tuesday && course.Week.Tuesday) {
            model.EducationList.Add(education);
          } else if (dayOfWeek == DayOfWeek.Wednesday && course.Week.Wednesday) {
            model.EducationList.Add(education);
          } else if (dayOfWeek == DayOfWeek.Thursday && course.Week.Thursday) {
            model.EducationList.Add(education);
          } else if (dayOfWeek == DayOfWeek.Friday && course.Week.Friday) {
            model.EducationList.Add(education);
          }
        }
      }
    }
  }
}

