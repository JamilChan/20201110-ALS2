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
    private IStudentRepository repoistory;

    public EducatorController(IStudentRepository repoistory) {
      this.repoistory = repoistory;
    }

    public ViewResult AbsenceList() {
      ViewBag.Check = false;
      return View(new StudentListViewModel {
        StatusList = new string[repoistory.Students.ToList().Count],
        StudentsList = repoistory.Students.ToList()
      });
    }

    [HttpPost]
    public IActionResult AbsenceList(StudentListViewModel studentList) {
      if (ModelState.IsValid) {

      }
      ViewBag.Check = false;
      List<string> temp = studentList.StatusList.ToList();


      return View("TestView", temp);
    }

    public IActionResult GetFromMainFrame() {
      return PartialView("AbsenceList");
    }

    [HttpPost]
    public IActionResult NewMethod(StudentListViewModel StudentList) {
      if (StudentList.IsChecked == "on") {
        StudentList.StudentsList = repoistory.Students.ToList();
        ViewBag.Check = true;
        return View("AbsenceList", StudentList);
      } else {
        StudentList.StudentsList = repoistory.Students.ToList();
        ViewBag.Check = false;
        return View("AbsenceList", StudentList);
      }
    }
  }
}
