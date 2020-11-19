using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20201110_ALS2.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace _20201110_ALS2.Controllers {
  public class AccountController : Controller {
    private UserManager<IdentityUser> userManager;
    private SignInManager<IdentityUser> signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) {
      this.userManager = userManager;
      this.signInManager = signInManager;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string returnUrl) {
      return View(new LoginModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginModel loginModel, string returnUrl) {
      if (ModelState.IsValid) {
        IdentityUser user = await userManager.FindByNameAsync(loginModel.Name);
        if (user != null)
        {
          SignInResult result = await signInManager.PasswordSignInAsync(user, loginModel.Password, loginModel.RememberMe, false);
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
      return View("Login", loginModel);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<RedirectResult> Logout(string returnUrl = "/") {
      await signInManager.SignOutAsync();
      return Redirect(returnUrl);
    }
  }
}
