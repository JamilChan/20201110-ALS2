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

    public EducatorController(IStudentRepository studentRepo , ICourseRepository courseRepo, IEducatorRepository educatorRepo, IStudentCourseRepository scRepo) {
      this.studentRepo = studentRepo;
      this.courseRepo = courseRepo;
      this.educatorRepo = educatorRepo;
      this.scRepo = scRepo;
    }

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
      CreateCourseViewModel CCVM = CreateCCVM();
      return View("CreateCourse", CCVM);
    }

    [HttpPost]
    public IActionResult CreateCourse(CreateCourseViewModel CCVM, IFormCollection form) {
      if (ModelState.IsValid) {
        foreach (Educator e in educatorRepo.GetAll()) {
          if (e.Name == CCVM.SelectedEducator) {
            CCVM.Crs.Educator = e;
            break;
          }
        }
        string selected = Request.Form["SelectedStudents"].ToString();
        string[] selectedList = selected.Split(',');
        foreach (var sId in selectedList) {
          StudentCourse sc = new StudentCourse { Course = CCVM.Crs, Student = studentRepo.Students.FirstOrDefault(s => s.StudentId == Int32.Parse(sId)) };
          scRepo.CreateStudentCourse(sc);
        }
        courseRepo.SaveCourse(CCVM.Crs);
        TempData["message"] = $"{CCVM.Crs.Name} has been saved";
        return RedirectToAction("ViewCourses");
      } else {
        CCVM.EducatorList = educatorRepo.GetAll();
        CCVM.GetEducatorsName();
        return View("CreateCourse", CCVM); 
      }
    }

    public ViewResult ViewCourses() {
      return View("ViewCourses", courseRepo.Courses);
    }

    [HttpGet]
    public ViewResult EditCourse(int CourseId) {
      CreateCourseViewModel CCVM = CreateCCVM();
      CCVM.Crs = courseRepo.Courses.FirstOrDefault(c => c.CourseId == CourseId);
      CCVM.SelectedEducator = CCVM.Crs.Educator.Name;
      return View("CreateCourse", CCVM);
    }

    [HttpPost]
    public IActionResult DeleteCourse(int CourseId) {
      Course deletedCourse = courseRepo.Delete(CourseId);
      return View("ViewCourses", courseRepo.Courses);
    }

    //[HttpGet]
    //public ViewResult ViewThisCourse(int CourseId) {
    //  CreateCourseViewModel CCVM = CreateCCVM();
    //  CCVM.Crs = courseRepo.Courses.FirstOrDefault(c => c.CourseId == CourseId);
    //  CCVM.SelectedEducator = CCVM.Crs.Educator.Name;
    //  return View("ViewThisCourse", CCVM);
    //}

    //Service metode
    public CreateCourseViewModel CreateCCVM() {
      CreateCourseViewModel CCVM = new CreateCourseViewModel();
      CCVM.EducatorList = educatorRepo.GetAll();
      CCVM.GetEducatorsName();
      CCVM.StudentList = studentRepo.Students;
      return CCVM;
    }

  }
}
