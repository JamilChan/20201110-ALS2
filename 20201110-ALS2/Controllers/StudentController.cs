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

    [HttpGet]
    public IActionResult Crud(string crud, long studentId) {
      ViewBag.crud = crud;
      Student student = new Student();

      if (crud == "edit") {
        foreach (Student s in studentRepo.Students) {
          if (studentId == s.StudentId) {
            student = s;
          }
        }
      }

      return View("Crud", student);
    }

    [HttpPost]
    public IActionResult Create(Student student) {
      if (!ModelState.IsValid) {
        return View("Crud", student);
      }

      studentRepo.Create(student);

      return View("Overview", studentRepo.Students);
    }

    [HttpPost]
    public IActionResult DeleteStudent(long studentId) {

      studentRepo.Delete(studentId);

      return View("Overview", studentRepo.Students);
    }

    [HttpPost]
    public IActionResult Update(Student student) {
      if (!ModelState.IsValid) {
        return View("Crud", student);
      }

      studentRepo.Update(student);

      return View("Overview", studentRepo.Students);
    }
  }
}