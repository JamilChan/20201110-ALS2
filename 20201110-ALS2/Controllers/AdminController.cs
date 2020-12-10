using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Threading.Tasks;
using _20201110_ALS2.Models;
using _20201110_ALS2.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace _20201110_ALS2.Controllers {
  [Authorize(Roles = "Admin")]
  public class AdminController : Controller {
    private IEducatorRepository educatorRepo;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AdminController(IEducatorRepository educatorRepo, UserManager<ApplicationUser> userManager,
      RoleManager<IdentityRole> roleManager) {
      this.educatorRepo = educatorRepo;
      this.userManager = userManager;
      this.roleManager = roleManager;
    }

    [HttpGet]
    public IActionResult Index() {
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

        ApplicationUser newUser = new ApplicationUser { UserName = model.UserName, Educator = model.Educator };

        IdentityResult result = await userManager.CreateAsync(newUser, model.Password);
        if (result.Succeeded) {
          TempData["Message"] = model.Educator.Name + " er blevet oprettet";
        }

        return RedirectToAction("Index");
      } else {
        return View("CreateEducator", model);
      }
    }

    [HttpGet]
    public ViewResult EditEducator(long educatorId) {
      Educator educator = educatorRepo.Educators.FirstOrDefault(e => e.EducatorId == educatorId);

      return View("Edit", educator);
    }

    [HttpPost]
    public IActionResult EditEducator(Educator educator) {
      if (ModelState.IsValid) {
        educatorRepo.EditEducator(educator);
        TempData["Message"] = educator.Name + " er blevet gemt";

        return RedirectToAction("Index");
      } else {
        return View("Edit", educator);
      }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteEducator(long educatorId) {
      ApplicationUser user = userManager.Users.FirstOrDefault(u => u.Educator.EducatorId == educatorId);


      if (user != null) {
        educatorRepo.Delete(educatorId);
        IdentityResult result = await userManager.DeleteAsync(user);


        foreach (IdentityError error in result.Errors) {
          ModelState.AddModelError("", error.Description);
        }
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
      return View(new CreateRole());
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(CreateRole model) {
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
        EditRole model = new EditRole { RoleId = roleId, RoleName = role.Name };

        foreach (ApplicationUser user in userManager.Users) {
          if (await userManager.IsInRoleAsync(user, role.Name)) {
            model.AllUsers.Add(user.UserName);
          }
        }

        Task<IList<Claim>> roleClaims = roleManager.GetClaimsAsync(role);

        foreach (Claim claim in roleClaims.Result) {
          model.RoleClaims.Add(claim.Value);
        }

        return View("EditRole", model);
      }
      ViewBag.ErrorMessage = "Rolle med id = " + roleId + " kunne ikke findes";

      return View("NotFound");
    }

    [HttpPost]
    public async Task<IActionResult> EditRole(EditRole model) {
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
        List<UserRole> modelList = new List<UserRole>();

        foreach (ApplicationUser user in userManager.Users) {
          UserRole model = new UserRole {
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
    public async Task<IActionResult> EditUsersInRole(List<UserRole> usrList, string roleId) {
      IdentityRole role = await roleManager.FindByIdAsync(roleId);

      if (role != null) {
        for (int i = 0; i < usrList.Count; i++) {
          ApplicationUser user = await userManager.FindByIdAsync(usrList[i].UserId);

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

    [HttpGet]
    public async Task<IActionResult> EditPermissionsForRole(string roleId) {
      ViewBag.roleId = roleId;
      IdentityRole role = await roleManager.FindByIdAsync(roleId);

      if (role != null) {
        Dictionary<string, bool> roleClaimsStore = RoleClaimsStore(role);

        return View("EditPermissionsInRole", roleClaimsStore);
      }

      return View("NotFound");
    }

    [HttpPost]
    public async Task<IActionResult> EditPermissionsForRole(Dictionary<string, bool> claimsConnectedToRole, string roleId) {
      IdentityRole role = await roleManager.FindByIdAsync(roleId);

      if (role != null) {
        Dictionary<string, bool> roleClaimsStore = RoleClaimsStore(role);

        foreach (string claim in claimsConnectedToRole.Keys) {
          if (claim != "__RequestVerificationToken") {
            Claim newClaim = new Claim(claim, claim);
            if (claimsConnectedToRole[claim] && claimsConnectedToRole[claim] != roleClaimsStore[claim]) {
              await roleManager.AddClaimAsync(role, newClaim);
            } else if (!claimsConnectedToRole[claim] && claimsConnectedToRole[claim] != roleClaimsStore[claim]) {
              await roleManager.RemoveClaimAsync(role, newClaim);
            }
          }
        }

        return RedirectToAction("EditRole", new { roleId = roleId });
      }

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
      }

      return View("NotFound");
    }

    private Dictionary<string, bool> RoleClaimsStore(IdentityRole role) {
      Dictionary<string, bool> claimsConnectedToRole = new Dictionary<string, bool>();

      IdentityRole adminRole = roleManager.Roles.FirstOrDefault(r => r.Name == "Admin");
      Task<IList<Claim>> adminClaims = roleManager.GetClaimsAsync(adminRole);
      List<Claim> adminClaimList = (List<Claim>)adminClaims.Result;

      Task<IList<Claim>> roleClaims = roleManager.GetClaimsAsync(role);
      List<Claim> claimsList = (List<Claim>)roleClaims.Result;

      foreach (Claim claimAdmin in adminClaimList) {
        bool claimIsInRole = false;
        foreach (Claim claim in claimsList) {
          if (claim.Type == claimAdmin.Type) {
            claimIsInRole = true;
            break;
          }
        }
        claimsConnectedToRole.Add(claimAdmin.Type, claimIsInRole);
      }

      return claimsConnectedToRole;
    }
  }
}
