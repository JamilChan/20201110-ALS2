using _20201110_ALS2.Models;
using _20201110_ALS2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace _20201110_ALS2.Controllers {
  public class EducationController : Controller {
    private IEducationRepository educationRepo;

    public EducationController(IEducationRepository educationRepo) {
      this.educationRepo = educationRepo;
    }

    [HttpGet]
    public IActionResult Overview() {
      List<Education> educations = educationRepo.Educations.ToList();

      return View("Overview", educations);
    }

    [HttpGet]
    public IActionResult Create() {
      ViewBag.action = "create";

      return View("Crud", new EducationCRUDViewModel {
        Education = new Education()
      });
    }

    [HttpPost]
    public IActionResult Create(EducationCRUDViewModel model) {
      if (!ModelState.IsValid) {
        ViewBag.action = "create";

        return View("Crud", model);
      }
      educationRepo.Create(model.Education);
      
      return RedirectToAction("Overview");
    }

    [HttpGet]
    public IActionResult Edit(int educationId) {
      ViewBag.action = "edit";
      Education education = educationRepo.Educations.FirstOrDefault(e => e.EducationId == educationId);

      return View("Crud", new EducationCRUDViewModel {
        Education = education
      });
    }

    [HttpPost]
    public IActionResult Edit(EducationCRUDViewModel model) {
      if (!ModelState.IsValid) {
        ViewBag.action = "edit";

        return View("Crud", model);
      }
      educationRepo.Update(model.Education);

      return RedirectToAction("Overview");
    }

    [HttpPost]
    public IActionResult Delete(int educationId) {
      educationRepo.Delete(educationId);

      return RedirectToAction("Overview");
    }
  }
}
