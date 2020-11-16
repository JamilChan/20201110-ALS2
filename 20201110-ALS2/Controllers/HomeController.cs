using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _20201110_ALS2.Models;

namespace _20201110_ALS2.Controllers {
  public class HomeController : Controller {
    public IActionResult Index() {
        return View("Index"); 
    }
  }
}
