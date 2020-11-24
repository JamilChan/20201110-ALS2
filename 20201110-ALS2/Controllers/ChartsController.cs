using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20201110_ALS2.Models;

namespace _20201110_ALS2.Controllers {
  public class ChartsController : Controller {

    //public IActionResult Index() {
    //  return View("Index");
    //}

    // GET: /<controller>/  
    public IActionResult Index() {
      return View();
    }

    [HttpGet]
    public JsonResult PopulationChart() {
      var populationList = PopulationDataAccessaLayer.GetUsStatePopulationList();
      return Json(populationList);
    }
  }
} 
