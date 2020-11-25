using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20201110_ALS2.Models;

namespace _20201110_ALS2.Controllers {
  public class ChartsController : Controller {

    public IActionResult Index() {
      return View();
    }

    [HttpGet]
    public JsonResult PopulationChart() {
      List<StudentAbsence> absenceList = new List<StudentAbsence> {
        new StudentAbsence {
          StudentName = "Mathias",
          AbsencePercent = 10
        },
        new StudentAbsence {
          StudentName = "Dean",
          AbsencePercent = 40
        },
        new StudentAbsence {
          StudentName = "Simon",
          AbsencePercent = 1
        },
        new StudentAbsence {
          StudentName = "Emil",
          AbsencePercent = 99
        }
      };

      return Json(absenceList);
    }
  }
} 
