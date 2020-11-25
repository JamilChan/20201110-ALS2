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

    public EducatorController(IStudentRepository studentRepo , ICourseRepository courseRepo, IEducatorRepository educatorRepo) {
      this.studentRepo = studentRepo;
      this.courseRepo = courseRepo;
      this.educatorRepo = educatorRepo;
    }

    [HttpGet]
    public ViewResult AbsenceList() {
      return View(studentRepo.Students);
    }

    [HttpPost]
    public IActionResult AbsenceList(int[] attended, int[] absence) {
      if (ModelState.IsValid) {
        
      }
      List<int[]> list = new List<int[]>();
      list.Add(attended);
      list.Add(absence);

      return View("TestView", list);
    }

    [HttpGet]
    public ViewResult CreateCourse() {
      CreateCourseViewModel model = CreateCCVM();

      return View("CreateCourse", model);
    }

    [HttpPost]
    public IActionResult CreateCourse(CreateCourseViewModel model, IFormCollection form) {
      if (ModelState.IsValid) {
        foreach (Educator educator in educatorRepo.GetAll()) {
          if (educator.Name == model.SelectedEducator) {
            model.Course.Educator = educator;
            break;
          }
        }

        string selected = Request.Form["SelectedStudents"].ToString();
        string[] selectedList = selected.Split(',');
        List<StudentCourse> studentCourseList = new List<StudentCourse>();

        if (selectedList[0] != "") {
          foreach (string studentId in selectedList) {
            StudentCourse studentCourse = new StudentCourse { Course = model.Course, Student = studentRepo.Students.FirstOrDefault(s => s.StudentId == Int64.Parse(studentId)) };
            studentCourseList.Add(studentCourse);
          }

          model.Course.StudentCourses = studentCourseList;
        }
        
        courseRepo.SaveCourse(model.Course);
        TempData["message"] = $"{model.Course.Name} has been saved";
        return RedirectToAction("ViewCourses");
      } else {
        model = CreateCCVM();

        return View("CreateCourse", model); 
      }
    }

    [HttpGet]
    public ViewResult ViewCourses() {
      return View("ViewCourses", courseRepo.Courses);
    }

    [HttpGet]
    public ViewResult EditCourse(int courseId) {
      CreateCourseViewModel model = CreateCCVM();
      model.Course = courseRepo.Courses.FirstOrDefault(c => c.CourseId == courseId);
      model.SelectedEducator = model.Course.Educator.Name;
      model.CheckedStudentList = courseRepo.SelectedStudents(courseId);
      model.Edit = true;

      return View("CreateCourse", model);
    }

    [HttpPost]
    public IActionResult DeleteCourse(int courseId) {
      courseRepo.Delete(courseId);

      return View("ViewCourses", courseRepo.Courses);
    }

    [HttpGet]
    public ViewResult ViewThisCourse(int courseId) {
      ViewCourseViewModel model = new ViewCourseViewModel();
      model.Course = courseRepo.Courses.FirstOrDefault(c => c.CourseId == courseId);
      model.StudentList = studentRepo.GetAllStudentsFromCourses(model.Course);

      return View("ViewThisCourse", model);
    }

    public CreateCourseViewModel CreateCCVM() {
      CreateCourseViewModel model = new CreateCourseViewModel();
      model.Educators = educatorRepo.GetAll();
      model.GetEducatorsName();
      model.Students = studentRepo.Students;
      model.Course.Week = new Week();
      model.Course.Week.WeekId = 0;

      return model;
    }
  }
}
