using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using _20201110_ALS2.Models;
using Microsoft.AspNetCore.Authorization;

namespace _20201110_ALS2.Controllers {
  //[Authorize]
  public class AdminController : Controller {
    private IEducatorRepository repository;

    public AdminController(IEducatorRepository repository) {
      this.repository = repository;
    }

    public IActionResult Index() {
      return View("Index", repository.GetAll());
    }

    [HttpGet]
    public ViewResult Create() {
      return View("CreateEducator", new Educator());
    }

    [HttpPost]
    public IActionResult Create(Educator newEducator) {
      if (ModelState.IsValid) {
        repository.Add(newEducator);
        TempData["Message"] = newEducator.Name + " er blevet oprettet";

        return RedirectToAction("Index");
      } else {
        return View("CreateEducator");
      }
    }

    [HttpGet]
    public ViewResult Edit(int educatorId) {
      return View("Edit", repository.GetAll().FirstOrDefault(ed => ed.EducatorId == educatorId));
    }

    [HttpPost]
    public IActionResult Edit(Educator educatorChanges) {
      if (ModelState.IsValid) {
        Educator educatorUpdated = repository.Update(educatorChanges);
        TempData["Message"] = educatorChanges.Name + " er blevet gemt";

        return RedirectToAction("Index");
      } else {
        return View("Edit", educatorChanges);
      }
    }

    [HttpPost]
    public IActionResult Delete(int educatorId) {
      Educator educatorDeleted = repository.Delete(educatorId);
      if (educatorDeleted != null) {
        TempData["Message"] = educatorDeleted.Name + " er blevet slettet";
      }

      return RedirectToAction("Index");
    }
  }
}
