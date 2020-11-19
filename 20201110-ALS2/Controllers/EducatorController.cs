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
      return View("AbsenceList", new StudentListViewModel {
        StatusList = new string[studentRepository.Students.ToList().Count],
        StudentsList = studentRepository.Students.ToList(),
        Course = course
      });
    }

    [HttpPost]
    public IActionResult Toggle(StudentListViewModel studentList) {
      if (studentList.IsChecked == "on") {
        studentList.StudentsList = studentRepository.Students.ToList();
        ViewBag.Check = true;
        return View("AbsenceList", studentList);
      } else {
        studentList.StudentsList = studentRepository.Students.ToList();
        ViewBag.Check = false;
        return View("AbsenceList", studentList);
      }
    }
  }
}
