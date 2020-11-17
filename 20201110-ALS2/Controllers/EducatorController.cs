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
      return View(new StudentListViewModel {
        StatusList = new string[repoistory.Students.ToList().Count],
        StudentsList = repoistory.Students.ToList()
      }) ;
    }

    [HttpPost]
    public IActionResult AbsenceList(StudentListViewModel studenList) {
      if (ModelState.IsValid) {
        
      }

      List<string> temp = studenList.StatusList.ToList();


      return View("TestView", temp);
    }
  }
}
