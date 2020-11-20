using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20201110_ALS2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using _20201110_ALS2.Models.ViewModels;

namespace _20201110_ALS2.Controllers {
  public class EducatorController : Controller {
    private IStudentRepository studentRepository;
    private IAbsenceRepository absenceRepository;

    public EducatorController(IStudentRepository studentRepository, IAbsenceRepository absenceRepository) {
      this.studentRepository = studentRepository;
      this.absenceRepository = absenceRepository;
    }

    //[HttpGet]
    //public ViewResult AbsenceList(Course course) {
    //  ViewBag.Check = false;

    //  return View(new StudentListViewModel {
    //    StatusList = new string[studentRepository.Students.ToList().Count],
    //    StudentsList = studentRepository.Students.ToList(),
    //    Course = course
    //  });
    //}

    [HttpPost]
    public IActionResult AbsenceList(StudentListViewModel slvm) {
      List<string> temp = slvm.StatusList.ToList();

      if (ModelState.IsValid) {
        List<Absence> absenceList = new List<Absence>();

        foreach (string s in temp) {
          string[] split = s.Split(":", 2);

          foreach (Student student in studentRepository.Students) {
            if (student.StudentId.ToString() == split[0]) {
              Absence absence = new Absence {
                Student = student,
                Date = DateTime.Now,
                Course = slvm.Course,
                Status = split[1]
              };
              absenceList.Add(absence);
              break;
            }
          }
        }
        absenceRepository.CreateAbsence(absenceList);
      }

      ViewBag.Check = false;

      return View("TestView", slvm.StatusList);
      //return View("TestView", slvm.Course.Name);
    }

    [HttpPost]
    public IActionResult Test(Course course) {
      ViewBag.Check = true;

      ApplyCourseWithId(course);

      return View("AbsenceList", new StudentListViewModel {
        StatusList = new string[studentRepository.Students.ToList().Count],
        StudentsList = studentRepository.Students.ToList(),
        Course = course
      });
    }

    [HttpPost]
    public IActionResult Toggle(StudentListViewModel studentList) {
      studentList.StudentsList = studentRepository.Students.ToList();

      ApplyCourseWithId(studentList.Course);

      if (studentList.IsChecked == "on") {
        ViewBag.Check = true;
        return View("AbsenceList", studentList);
      } else {
        ViewBag.Check = false;
        return View("AbsenceList", studentList);
      }
    }

    private void ApplyCourseWithId(Course course) {
      List<Course> cl = testCourses();

      foreach (Course c in cl) {
        if (c.CourseId == course.CourseId) {
          course = c;
          break;
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
