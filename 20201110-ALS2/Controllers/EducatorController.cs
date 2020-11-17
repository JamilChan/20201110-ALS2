﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20201110_ALS2.Models;
using Microsoft.AspNetCore.Mvc;

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
      ViewBag.EducatorList = new List<Educator> { new Educator { Name = "Flemming" }, new Educator { Name = "Hans" } };
      return View("CreateCourse");
    }

    [HttpPost]
    public IActionResult CreateCourse(Course course) {
      if (ModelState.IsValid) {
        return RedirectToAction("Index", "Home");
      } else { 
        return View("CreateCourse", course); 
      }
    }

  }
}
