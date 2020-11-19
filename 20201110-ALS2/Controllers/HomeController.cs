using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _20201110_ALS2.Models;
using _20201110_ALS2.Models.ViewModels;

namespace _20201110_ALS2.Controllers {
  public class HomeController : Controller {

    [HttpGet]
    public IActionResult Index() {
      HomeIndexViewModel hi = new HomeIndexViewModel();

      hi.Date = DateTime.Now;
      string[] date = DateTime.Now.ToString("O").Split("T", 2);
      hi.DateAsString = date[0];

      return View("Index", hi);
    }

    [HttpPost]
    public IActionResult Index(HomeIndexViewModel hi) {

      DateTime dateTime = hi.Date;

      if (hi.Direction == "Backward") {
        dateTime = hi.Date.AddDays(-1);
      } else if (hi.Direction == "Forward") {
        dateTime = hi.Date.AddDays(1);
      }

      hi.Date = dateTime;

      string[] date = hi.Date.ToString("O").Split("T", 2);
      hi.DateAsString = date[0];

      return View("Index", hi);
    }
  }
}
