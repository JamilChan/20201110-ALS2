using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20201110_ALS2.Models;
using _20201110_ALS2.Models.ViewModels;
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
      CreateCourseViewModel CCVM = new CreateCourseViewModel();
      CCVM.EducatorList = educatorRepo.GetAll();
      CCVM.GetEducatorsName();
      return View("CreateCourse", CCVM);
    }

    [HttpPost]
    public IActionResult CreateCourse(CreateCourseViewModel CCVM) {
      if (ModelState.IsValid) {
        foreach (Educator e in educatorRepo.GetAll()) {
          if (e.Name == CCVM.SelectedEducator) {
            CCVM.Crs.Educator = e;
            break;
          }
        }
        courseRepo.SaveCourse(CCVM.Crs);
        TempData["message"] = $"{CCVM.Crs.Name} has been saved";
        return RedirectToAction("Index", "Home");
      } else {
        CCVM.EducatorList = educatorRepo.GetAll();
        CCVM.GetEducatorsName();
        return View("CreateCourse", CCVM); 
      }
    }

    public ViewResult ViewCourse() {
      
      return View("ViewCourse", courseRepo.Courses);

    }


  }
}
