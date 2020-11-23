using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
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
    private IEducatorRepository educatorRepo;
    private readonly UserManager<IdentityUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AdminController(IEducatorRepository educatorRepo, UserManager<IdentityUser> userManager,
      RoleManager<IdentityRole> roleManager) {
      this.educatorRepo = educatorRepo;
      this.userManager = userManager;
      this.roleManager = roleManager;
    }

    [HttpGet]
    public IActionResult Index()
    {
      IQueryable<Educator> educators = educatorRepo.Educators;

      return View("Index", educators);
    }

    [HttpGet]
    public ViewResult CreateEducator() {
      return View("CreateEducator", new RegisterEducatorViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> CreateEducator(RegisterEducatorViewModel model) {
      if (ModelState.IsValid) {
        educatorRepo.SaveEducator(model.Educator);
        TempData["Message"] = model.Educator.Name + " er blevet oprettet";

        IdentityUser newUser = new IdentityUser { UserName = model.UserName };
        await userManager.CreateAsync(newUser, model.Password);

        return RedirectToAction("Index");
      } else {
        return View("CreateEducator", model);
      }
    }

    [HttpGet]
    public ViewResult EditEducator(long educatorId) {
      Educator educator = educatorRepo.Get(educatorId);

      return View("Edit", educator);
    }

    [HttpPost]
    public IActionResult EditEducator(Educator educator) {
      if (ModelState.IsValid) {
        educatorRepo.SaveEducator(educator);
        TempData["Message"] = educator.Name + " er blevet gemt";

        return RedirectToAction("Index");
      } else {
        return View("Edit", educator);
      }
    }

    [HttpPost]
    public IActionResult DeleteEducator(long educatorId) {
      Educator educatorDeleted = educatorRepo.Delete(educatorId);

      if (educatorDeleted != null) {
        TempData["Message"] = educatorDeleted.Name + " er blevet slettet";
      }

      return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ListRoles() {
      IEnumerable<IdentityRole> roles = roleManager.Roles;

      return View("ListRoles", roles);
    }

    [HttpGet]
    public ViewResult CreateRole() {
      return View(new CreateRoleViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(CreateRoleViewModel model) {
      if (ModelState.IsValid) {
        IdentityRole newRole = new IdentityRole { Name = model.RoleName };
        IdentityResult result = await roleManager.CreateAsync(newRole);

        if (result.Succeeded) {
          return RedirectToAction("ListRoles");
        }

        foreach (IdentityError error in result.Errors) {
          ModelState.AddModelError("", error.Description);
        }
      }

      return View("CreateRole", model);
    }

    [HttpGet]
    public async Task<IActionResult> EditRole(string roleId) {
      IdentityRole role = await roleManager.FindByIdAsync(roleId);

      if (role != null) {
        EditRoleViewModel model = new EditRoleViewModel { RoleId = roleId, RoleName = role.Name };

        foreach (IdentityUser user in userManager.Users) {
          if (await userManager.IsInRoleAsync(user, role.Name)) {
            model.AllUsers.Add(user.UserName);
          }
        }

        return View("EditRole", model);
      }
      ViewBag.ErrorMessage = "Rolle med id = " + roleId + " kunne ikke findes";

      return View("NotFound");
    }

    [HttpPost]
    public async Task<IActionResult> EditRole(EditRoleViewModel model) {
      IdentityRole role = await roleManager.FindByIdAsync(model.RoleId);

      if (role != null) {
        role.Name = model.RoleName;
        IdentityResult result = await roleManager.UpdateAsync(role);

        if (result.Succeeded) {
          return RedirectToAction("ListRoles");
        }

        foreach (IdentityError error in result.Errors) {
          ModelState.AddModelError("", error.Description);
        }

        return View("EditRole", model);
      }
      ViewBag.ErrorMessage = "Rolle med id = " + model.RoleId + " kunne ikke findes";

      return View("NotFound");
    }

    [HttpGet]
    public async Task<IActionResult> EditUsersInRole(string roleId) {
      ViewBag.roleId = roleId;
      IdentityRole role = await roleManager.FindByIdAsync(roleId);

      if (role != null) {
        List<UserRoleViewModel> modelList = new List<UserRoleViewModel>();

        foreach (IdentityUser user in userManager.Users) {
          UserRoleViewModel model = new UserRoleViewModel {
            UserId = user.Id,
            UserName = user.UserName
          };

          if (await userManager.IsInRoleAsync(user, role.Name)) {
            model.IsSelected = true;
          } else {
            model.IsSelected = false;
          }
          modelList.Add(model);
        }

        return View("EditUsersInRole", modelList);
      }
      ViewBag.ErrorMessage = "Rolle med id = " + roleId + " kunne ikke findes";

      return View("NotFound");
    }

    [HttpPost]
    public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> usrList, string roleId) {
      IdentityRole role = await roleManager.FindByIdAsync(roleId);

      if (role != null) {
        for (int i = 0; i < usrList.Count; i++) {
          IdentityUser user = await userManager.FindByIdAsync(usrList[i].UserId);

          IdentityResult result = null;

          if (usrList[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name))) {
            result = await userManager.AddToRoleAsync(user, role.Name);
          } else if (!usrList[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name)) {
            result = await userManager.RemoveFromRoleAsync(user, role.Name);
          } else {
            continue;
          }

          if (result.Succeeded) {
            if (i < (usrList.Count - 1)) {
              continue;
            } else {
              return RedirectToAction("EditRole", new { roleId = roleId });
            }
          }
        }

        return RedirectToAction("EditRole", new { roleId = roleId });
      }
      ViewBag.ErrorMessage = "Rolle med id = " + roleId + " kunne ikke findes";

      return View("NotFound");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteRole(string roleId) {
      IdentityRole role = await roleManager.FindByIdAsync(roleId);

      if (role != null) {
        IdentityResult result = await roleManager.DeleteAsync(role);

        if (result.Succeeded) {
          return RedirectToAction("ListRoles", "Admin");
        }

        foreach (IdentityError error in result.Errors) {
          ModelState.AddModelError("", error.Description);
        }

        return View("ListRoles");
      } else {
        ViewBag.ErrorMessage = "Rolle med id = " + roleId + " kunne ikke findes";

        return View("NotFound");
      }
    }
  }
}
