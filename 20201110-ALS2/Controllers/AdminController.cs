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
    private IEducatorRepository repository;
    private readonly UserManager<IdentityUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AdminController(IEducatorRepository repository, UserManager<IdentityUser> userManager,
      RoleManager<IdentityRole> roleManager) {
      this.repository = repository;
      this.userManager = userManager;
      this.roleManager = roleManager;
    }

    [HttpGet]
    public IActionResult Index() {
      EducatorsViewModel edcModel = new EducatorsViewModel { AllEducators = repository.GetAll(), AllRoles = roleManager.Roles }; //er deer en smartere måde at sætte dette på?
      IQueryable<Educator> educators = repository.GetAll();

      //List<string> userIdList = new List<string>(); // det at slette , skal det ske igennem et andet view?

      //foreach (IdentityUser user in userManager.Users) {
      //  userIdList.Add(user.Id);
      //}

      return View("Index", edcModel);
    }

    [HttpGet]
    public ViewResult CreateEducator() {
      RegisterEducatorViewModel model = new RegisterEducatorViewModel { AllRoles = roleManager.Roles }; //er deer en smartere måde at sætte dette på?

      return View("CreateEducator", model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEducator(RegisterEducatorViewModel registerEducatorViewModel, string roleId) {
      if (ModelState.IsValid) {
        repository.Add(registerEducatorViewModel.Educator);
        TempData["Message"] = registerEducatorViewModel.Educator.Name + " er blevet oprettet";

        IdentityUser newUser = new IdentityUser { UserName = registerEducatorViewModel.LoginModel.Name };
        await userManager.CreateAsync(newUser, registerEducatorViewModel.LoginModel.Password);

        //Mangler funktionalitet for at kunne tilføje en underviser til en rolle ved oprettelse - er det nødvendigt? Det er vel flemming??

        return RedirectToAction("Index");
      } else {
        return View("CreateEducator", registerEducatorViewModel);
      }
    }

    [HttpGet]
    public ViewResult EditEducator(int educatorId) {

      return View("Edit", repository.Get(educatorId));
    }

    [HttpPost]
    public IActionResult EditEducator(Educator educatorChanges) {
      if (ModelState.IsValid) {
        Educator educatorUpdated = repository.Update(educatorChanges);
        TempData["Message"] = educatorChanges.Name + " er blevet gemt";

        return RedirectToAction("Index");
      } else {
        return View("Edit", educatorChanges);
      }
    }

    [HttpPost]
    public IActionResult DeleteEducator(int educatorId) {
      Educator educatorDeleted = repository.Delete(educatorId);
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
    public async Task<IActionResult> CreateRole(CreateRoleViewModel createRoleViewModel) {
      if (ModelState.IsValid) {
        IdentityRole newRole = new IdentityRole { Name = createRoleViewModel.RoleName };

        IdentityResult result = await roleManager.CreateAsync(newRole);

        if (result.Succeeded) {
          return RedirectToAction("ListRoles", "Admin"); //Bør vi også skrive controlleren? I dette tilfælde er det den samme
        }

        foreach (IdentityError error in result.Errors) {
          ModelState.AddModelError("", error.Description);
        }
      }

      return View("CreateRole", createRoleViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> EditRole(string roleId) {
      IdentityRole role = await roleManager.FindByIdAsync(roleId);

      if (role == null) {
        ViewBag.ErrorMessage = "Role with Id = " + roleId + " cannot be found";
        return View("NotFound");
      }

      EditRoleViewModel editRoleViewModel = new EditRoleViewModel { RoleId = roleId, RoleName = role.Name };

      foreach (IdentityUser user in userManager.Users) {
        if (await userManager.IsInRoleAsync(user, role.Name)) {
          editRoleViewModel.AllUsers.Add(user.UserName);
        }
      }
      return View("EditRole", editRoleViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditRole(EditRoleViewModel editRoleViewModel) {
      IdentityRole role = await roleManager.FindByIdAsync(editRoleViewModel.RoleId);

      if (role == null) {
        ViewBag.ErrorMessage = "Rolle med id = " + editRoleViewModel.RoleId + " kunne ikke findes";

        return View("NotFound");
      } else {
        role.Name = editRoleViewModel.RoleName;

        IdentityResult result = await roleManager.UpdateAsync(role);

        if (result.Succeeded) {
          return RedirectToAction("ListRoles");
        }

        foreach (IdentityError error in result.Errors) {
          ModelState.AddModelError("", error.Description);
        }

        return View("EditRole", editRoleViewModel);
      }
    }

    [HttpGet]
    public async Task<IActionResult> EditUsersInRole(string roleId) {
      ViewBag.roleId = roleId;

      IdentityRole role = await roleManager.FindByIdAsync(roleId);

      if (role == null) {
        ViewBag.ErrorMessage = "Role with Id = " + roleId + " cannot be found";
        return View("NotFound");
      }

      List<UserRoleViewModel> usrList = new List<UserRoleViewModel>();

      foreach (IdentityUser user in userManager.Users) {
        UserRoleViewModel userRoleViewModel = new UserRoleViewModel {
          UserId = user.Id,
          UserName = user.UserName
        };

        if (await userManager.IsInRoleAsync(user, role.Name)) {
          userRoleViewModel.IsSelected = true;
        } else {
          userRoleViewModel.IsSelected = true;
        }
        usrList.Add(userRoleViewModel);
      }

      return View("EditUsersInRole", usrList);
    }

    [HttpPost]
    public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> usrList, string roleId) {
      IdentityRole role = await roleManager.FindByIdAsync(roleId);

      if (role == null) {
        ViewBag.ErrorMessage = "Role with Id = " + roleId + " cannot be found";
        return View("NotFound");
      }

      if (usrList.Count == 0) {
        TempData["Message"] = "Ingen brugere blev tilføjet til rollen " + role.Name;
      }

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
            continue; ;

          } else {
            return RedirectToAction("EditRole", new { roleId = roleId });
          }
        }
      }

      return RedirectToAction("EditRole", new { roleId = roleId });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteRole(string roleId) {
      IdentityRole role = await roleManager.FindByIdAsync(roleId);

      if (role == null) {
        ViewBag.ErrorMessage = "Role with Id = " + roleId + " cannot be found";
        return View("NotFound");

      } else {
        IdentityResult result = await roleManager.DeleteAsync(role);

        if (result.Succeeded) {
          return RedirectToAction("ListRoles", "Admin");
        }

        foreach (IdentityError error in result.Errors) {
          ModelState.AddModelError("", error.Description);
        }
        return View("ListRoles");
      }
    }
  }
}
