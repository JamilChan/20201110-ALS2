using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20201110_ALS2.Models;
using _20201110_ALS2.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace _20201110_ALS2.Controllers {
  [Authorize]
  public class AccountController : Controller {
    private UserManager<ApplicationUser> userManager;
    private SignInManager<ApplicationUser> signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) {
      this.userManager = userManager;
      this.signInManager = signInManager;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string returnUrl) {
      return View("Login", new LoginModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginModel model, string returnUrl) {
      if (ModelState.IsValid) {
        ApplicationUser user = await userManager.FindByNameAsync(model.Name);

        if (user != null) {
          SignInResult result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

          if (result.Succeeded) {
            if (!string.IsNullOrEmpty(returnUrl)) {
              return Redirect(returnUrl);
            } else {
              return RedirectToAction("Index", "Home");
            }
          }
        }
      }
      ModelState.AddModelError("", "Invalid name or password");
      return View("Login", model);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<RedirectResult> Logout(string returnUrl = "/") {
      await signInManager.SignOutAsync();

      return Redirect(returnUrl);
    }

    [HttpGet]
    public IActionResult EditPassword() {
      return View("ChangePassword", new ChangePassword());
    }

    [HttpPost]
    public async Task<IActionResult> EditPassword(ChangePassword model) {
      if (ModelState.IsValid) {
        ApplicationUser user = await userManager.GetUserAsync(User);

        if (user == null) {
          return RedirectToAction("Login");
        }

        IdentityResult result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        if (!result.Succeeded) {
          foreach (IdentityError error in result.Errors) {
            ModelState.AddModelError("", error.Description);
          }

          return View("ChangePassword");
        }

        await signInManager.RefreshSignInAsync(user);
        return View("ChangedPasswordConfirmation");
      }

      return View("ChangePassword", model);
    }
  }
}
