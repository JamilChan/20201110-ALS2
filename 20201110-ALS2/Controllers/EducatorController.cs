using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20201110_ALS2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace _20201110_ALS2.Controllers {
  public class EducatorController : Controller {
    private IStudentRepository repoistory;

    public EducatorController(IStudentRepository repoistory) {
      this.repoistory = repoistory;
    }

    public ViewResult AbsenceList() {
      return View(repoistory.Students);
    }

    [HttpPost]
    public IActionResult AbsenceList(IFormCollection status) {
      if (ModelState.IsValid) {
        
      }

      //List<string> temp = new List<string>();
      //string s = status["1"];

      //return View("TestView", s);

      List<string> temp = new List<string>();

      for (int i = 0; i < status.Count; i++) {
        temp.Concat(status[i.ToString()]).ToList();
      }

      return View("TestView", temp);
    }
  }
}
