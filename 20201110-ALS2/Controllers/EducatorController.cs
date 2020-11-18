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

    public EducatorController(IStudentRepository studentRepo , ICourseRepository courseRepo) {
      this.studentRepo = studentRepo;
      this.courseRepo = courseRepo;
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
      CreateCourseViewModel CCVM = new CreateCourseViewModel { 
        EducatorList = new List<Educator> { new Educator {EducatorId = 1, Name = "Flemming" }, new Educator { EducatorId = 2, Name = "Hans" } } };
      CCVM.GetEducatorsName();
      return View("CreateCourse", CCVM);
    }

    [HttpPost]
    public IActionResult CreateCourse(CreateCourseViewModel CCVM) {
      List<Educator> educatorList = new List<Educator> { new Educator { EducatorId = 1, Name = "Flemming" }, new Educator { EducatorId = 2, Name = "Hans" } };
      if (ModelState.IsValid) {
        foreach (Educator e in educatorList) {
          if (e.Name == CCVM.SelectedEducator) {
            CCVM.Crs.Educator = e;
            break;
          }
        }
        courseRepo.SaveCourse(CCVM.Crs);
        TempData["message"] = $"{CCVM.Crs.Name} has been saved";
        return RedirectToAction("Index", "Home");
      } else {
        CCVM.EducatorList = educatorList;
        CCVM.GetEducatorsName();
        return View("CreateCourse", CCVM); 
      }
    }
  }
}
