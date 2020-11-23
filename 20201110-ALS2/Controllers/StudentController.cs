using System.Linq;
using _20201110_ALS2.Models;
using Microsoft.AspNetCore.Mvc;

namespace _20201110_ALS2.Controllers {
  public class StudentController : Controller {
    public IStudentRepository studentRepo;

    public StudentController(IStudentRepository studentRepo) {
      this.studentRepo = studentRepo;
    }

    [HttpGet]
    public IActionResult Overview() {


      return View("Overview", studentRepo.Students);
    }

    [HttpPost]
    public IActionResult DeleteStudent(long studentId) {

      studentRepo.Delete(studentId);

      return View("Overview", studentRepo.Students);
    }


    [HttpGet]
    public IActionResult Crud(string crud) {


      return View("Crud");
    }
  }
}