using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20201110_ALS2.Models;
using _20201110_ALS2.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _20201110_ALS2.Controllers {
  public class EducatorController : Controller {
    private IStudentRepository studentRepo;
    private ICourseRepository courseRepo;
    private IEducatorRepository educatorRepo;
    private IStudentCourseRepository scRepo;
    private readonly IAbsenceRepository absenceRepo;

    public EducatorController(IStudentRepository studentRepo, ICourseRepository courseRepo, IEducatorRepository educatorRepo, IStudentCourseRepository scRepo, IAbsenceRepository absenceRepo) {
      this.studentRepo = studentRepo;
      this.courseRepo = courseRepo;
      this.educatorRepo = educatorRepo;
      this.scRepo = scRepo;
      this.absenceRepo = absenceRepo;
    }

    [HttpGet]
    public ViewResult AbsenceList(long courseId, string dateString, bool edit) {
      ViewBag.Check = true;

      string[] dateSplit = dateString.Split("-", 3);

      DateTime date = new DateTime(Int32.Parse(dateSplit[2].Split(" ", 2)[0]), Int32.Parse(dateSplit[1]), int.Parse(dateSplit[0]));

      Course course = ApplyCourseWithId(courseId);

      return View("AbsenceList", new StudentListViewModel {
        StatusList = new string[studentRepo.Students.ToList().Count],
        StudentsList = studentRepo.GetAllStudentsFromCourses(course),
        AbsencesList = AbsenceForStudentList(courseId, date),
        Course = course,
        Date = date,
        Edit = edit
      });
    }

    [HttpPost]
    public IActionResult AbsenceList(StudentListViewModel slvm) {
      List<string> temp = slvm.StatusList.ToList();

      List<Absence> absenceList = new List<Absence>();

      if (!slvm.Edit) {
        foreach (string s in temp) {
          string[] split = s.Split(":", 2);

          foreach (Student student in studentRepo.Students) {
            if (student.StudentId.ToString() == split[0]) {

              Course selectedCourse = new Course();
              foreach (Course course in courseRepo.Courses) {
                if (course.CourseId == slvm.Course.CourseId) {
                  selectedCourse = course;
                }
              }

              Absence absence = new Absence {
                Student = student,
                Date = slvm.Date,
                Course = selectedCourse,
                Status = split[1]
              };
              absenceList.Add(absence);
              break;
            }
          }
        }

        absenceRepo.CreateAbsence(absenceList);
      } else {
        List<Absence> a = absenceRepo.AbsencesForDateCourse(slvm.Course, slvm.Date).ToList();

        absenceRepo.UpdateAbsence(a, temp);
      }

      ViewBag.Check = false;

      return View("TestView", slvm.StatusList);
    }

    [HttpPost]
    public IActionResult Toggle(StudentListViewModel studentList) {
      Course course = ApplyCourseWithId(studentList.Course.CourseId);
      studentList.Course = course;

      studentList.StudentsList = studentRepo.GetAllStudentsFromCourses(course);

      if (studentList.IsChecked == "on") {
        ViewBag.Check = true;
        return View("AbsenceList", studentList);
      } else {
        ViewBag.Check = false;
        return View("AbsenceList", studentList);
      }
    }

    [HttpGet]
    public ViewResult CreateCourse() {
      CreateCourseViewModel ccvm = CreateCCVM();

      return View("CreateCourse", ccvm);
    }

    [HttpPost]
    public IActionResult CreateCourse(CreateCourseViewModel ccvm, IFormCollection form) {
      if (ModelState.IsValid) {
        foreach (Educator e in educatorRepo.Educators()) {
          if (e.Name == ccvm.SelectedEducator) {
            ccvm.Crs.Educator = e;
            break;
          }
        }
        string selected = Request.Form["SelectedStudents"].ToString();
        string[] selectedList = selected.Split(',');
        foreach (var sId in selectedList) {
          StudentCourse sc = new StudentCourse { Course = ccvm.Crs, Student = studentRepo.Students.FirstOrDefault(s => s.StudentId == int.Parse(sId)) };
          scRepo.CreateStudentCourse(sc);
        }
        courseRepo.SaveCourse(ccvm.Crs);
        TempData["message"] = $"{ccvm.Crs.Name} has been saved";
        return RedirectToAction("ViewCourses");
      } else {
        ccvm.EducatorList = educatorRepo.Educators();
        ccvm.GetEducatorsName();
        return View("CreateCourse", ccvm);
      }
    }

    [HttpGet]
    public ViewResult ViewCourses() {
      return View("ViewCourses", courseRepo.Courses);
    }

    [HttpGet]
    public ViewResult EditCourse(int courseId) {
      CreateCourseViewModel ccvm = CreateCCVM();
      ccvm.Crs = courseRepo.Courses.FirstOrDefault(c => c.CourseId == courseId);
      ccvm.SelectedEducator = ccvm.Crs.Educator.Name;

      return View("CreateCourse", ccvm);
    }

    [HttpPost]
    public IActionResult DeleteCourse(int courseId) {
      courseRepo.Delete(courseId);

      return View("ViewCourses", courseRepo.Courses);
    }

    private CreateCourseViewModel CreateCCVM() {
      CreateCourseViewModel ccvm = new CreateCourseViewModel();
      ccvm.EducatorList = educatorRepo.Educators();
      ccvm.GetEducatorsName();
      ccvm.StudentList = studentRepo.Students;

      return ccvm;
    }

    private Course ApplyCourseWithId(long courseId) {
      List<Course> cl = courseRepo.Courses.ToList();

      foreach (Course c in cl) {
        if (c.CourseId == courseId) {
          return c;
        }
      }

      return null;
    }

    private List<Absence> AbsenceForStudentList(long courseId, DateTime date) {
      List<Absence> absences = new List<Absence>();

      foreach (Absence absence in absenceRepo.Absences) {
        if (absence.Course.CourseId == courseId && absence.Date.Date == date.Date) {
          absences.Add(absence);
        }
      }

      return absences;
    }
  }
}
