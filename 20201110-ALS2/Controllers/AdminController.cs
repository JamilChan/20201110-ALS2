using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using _20201110_ALS2.Models;
using _20201110_ALS2.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace _20201110_ALS2.Controllers {
  public class AdminController : Controller {
    private IEducatorRepository repository;
    private readonly UserManager<IdentityUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AdminController(IEducatorRepository repository, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) {
      this.repository = repository;
      this.userManager = userManager;
      this.roleManager = roleManager;
    }

    public IActionResult Index() {
      roleManager.Roles.FirstOrDefault(r => r.Id == "1");
      return View("Index", repository.GetAll());
    }

    [HttpGet]
    public ViewResult Create() {
      return View("CreateEducator", new RegisterEducatorViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(RegisterEducatorViewModel registerEducatorViewModel) {
      if (ModelState.IsValid) {
        repository.Add(registerEducatorViewModel.Educator);
        TempData["Message"] = registerEducatorViewModel.Educator.Name + " er blevet oprettet";

        CreateUser(registerEducatorViewModel);

        foreach (IdentityRole role in roleManager.Roles) {
          if (role.Name == registerEducatorViewModel.RoleName) {
            AddNewUserToRole(registerEducatorViewModel);
          } else {
            CreateRole(registerEducatorViewModel);
          }
        }

        return RedirectToAction("Index");
      } else {
        return View("CreateEducator");
      }
    }

    [HttpGet]
    public ViewResult Edit(int educatorId) {
      return View("Edit", repository.Get(educatorId));
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

    private async void CreateRole(RegisterEducatorViewModel registerEducatorViewModel) {
      IdentityRole newRole = new IdentityRole { Name = registerEducatorViewModel.RoleName };
      await roleManager.CreateAsync(newRole);
    }

    private async void CreateUser(RegisterEducatorViewModel registerEducatorViewModel) {
      IdentityUser newUser = new IdentityUser { UserName = registerEducatorViewModel.LoginModel.Name };
      await userManager.CreateAsync(newUser, registerEducatorViewModel.LoginModel.Password);
    }
    private async void AddNewUserToRole(RegisterEducatorViewModel registerEducatorViewModel) {
      IdentityUser newUser = new IdentityUser { UserName = registerEducatorViewModel.LoginModel.Name };
      await userManager.AddToRoleAsync(newUser, registerEducatorViewModel.RoleName);
    }
  }
}
