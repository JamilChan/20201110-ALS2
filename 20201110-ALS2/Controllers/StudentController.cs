using System.Linq;
using _20201110_ALS2.Models;
using _20201110_ALS2.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace _20201110_ALS2.Controllers {
  public class StudentController : Controller {
    public IStudentRepository studentRepo;
    public IEducationRepository educationRepo;

    public StudentController(IStudentRepository studentRepo, IEducationRepository educationRepo) {
      this.studentRepo = studentRepo;
      this.educationRepo = educationRepo;
    }

    [HttpGet]
    [Authorize(Policy = "SeStuderendePolicy")]
    public IActionResult Overview() {
      SaveSession("", 1);

      return View("Overview", studentRepo.Students);
    }

    [HttpGet]
    [Authorize(Policy = "HåndterStuderendePolicy")]
    public IActionResult Crud(string crud, long studentId) {
      ViewBag.crud = crud;
      StudentCRUDViewModel model = new StudentCRUDViewModel();
      model.GenerateModel(educationRepo.Educations.ToList());
      model.Student.Education.Name = HttpContext.Session.GetString("Education");
      model.Student.Semester = (int) HttpContext.Session.GetInt32("Semester");

      if (crud == "edit") {
        foreach (Student s in studentRepo.Students) {
          if (studentId == s.StudentId) {
            model.Student = s;
          }
        }
      }

      return View("Crud", model);
    }

    [HttpPost]
    [Authorize(Policy = "HåndterStuderendePolicy")]
    public IActionResult Create(StudentCRUDViewModel model) {
      if (!ModelState.IsValid) {
        model.GenerateModel(educationRepo.Educations.ToList());
        ViewBag.crud = "create";

        return View("Crud", model);
      }

      model.Student.Education = educationRepo.Educations.FirstOrDefault(e => e.Name == model.Student.Education.Name);
      studentRepo.Create(model.Student);
      SaveSession(model?.Student?.Education?.Name, model.Student.Semester);

      return View("Overview", studentRepo.Students);
    }

    [HttpPost]
    [Authorize(Policy = "SletStuderendePolicy")]
    public IActionResult DeleteStudent(long studentId) {

      studentRepo.Delete(studentId);

      return View("Overview", studentRepo.Students);
    }

    [HttpPost]
    [Authorize(Policy = "HåndterStuderendePolicy")]
    public IActionResult Update(StudentCRUDViewModel model) {
      if (!ModelState.IsValid) {
        return View("Crud", model);
      }

      model.Student.Education = educationRepo.Educations.FirstOrDefault(e => e.Name == model.Student.Education.Name);
      studentRepo.Update(model.Student);

      return View("Overview", studentRepo.Students);
    }

    private void SaveSession(string educationName, int semester) {
      HttpContext.Session.SetString("Education", educationName);
      HttpContext.Session.SetInt32("Semester", semester);
    }
  }
}