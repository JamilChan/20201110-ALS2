using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Controllers {
  public class DragenDropenController : Controller {
    public IActionResult Index() {
      return View();
    }
  }
}
