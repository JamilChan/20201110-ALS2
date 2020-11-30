using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20201110_ALS2.Models;
using _20201110_ALS2.Models.ViewModels;

namespace _20201110_ALS2.Controllers {
  public class ChartsController : Controller {
    public IStudentRepository studentRepo { get; }
    public IAbsenceRepository absenceRepo { get; set; }

    public ChartsController(IStudentRepository studentRepo, IAbsenceRepository absenceRepo) {
      this.studentRepo = studentRepo;
      this.absenceRepo = absenceRepo;
    }

    [HttpGet]
    public ViewResult Index() {
      return View();
    }

    [HttpGet]
    public IActionResult CourseStudents() {
      List<Absence> tempAbsences = absenceRepo.Absences.ToList();

      CalculateAbsence calculateAbsence = new CalculateAbsence();

      List<CalculateAbsence> absenceList = calculateAbsence.AbsenceForStudentsInCourse(tempAbsences);

      return View("CourseStudents", absenceList);
    }

    [HttpGet]
    public ViewResult CourseStudentsDays() {

      List<Student> studentList = studentRepo.Students.ToList();

      Student s1 = new Student { Name = "Simon Markussen" };
      Student s2 = new Student { Name = "Dean Marco Dalager Birch Nielsen" };
      Student s3 = new Student { Name = "Emil Overgaard Jensen" };

      studentList.Add(s1);
      studentList.Add(s2);
      studentList.Add(s3);
      
      List<string> students = new List<string>();

      foreach (Student student in studentList) {
        students.Add(ShortenName(student.Name));
      }

      List<string> dates = new List<string>{
        DateTime.Today.AddDays(-18).ToShortDateString(),
        DateTime.Today.AddDays(-17).ToShortDateString(),
        DateTime.Today.AddDays(-16).ToShortDateString(),
        DateTime.Today.AddDays(-15).ToShortDateString(),
        DateTime.Today.AddDays(-14).ToShortDateString(),
        DateTime.Today.AddDays(-13).ToShortDateString(),
        DateTime.Today.AddDays(-12).ToShortDateString(),
        DateTime.Today.AddDays(-11).ToShortDateString(),
        DateTime.Today.AddDays(-10).ToShortDateString(),
        DateTime.Today.AddDays(-9).ToShortDateString(),
        DateTime.Today.AddDays(-8).ToShortDateString(),
        DateTime.Today.AddDays(-7).ToShortDateString(),
        DateTime.Today.AddDays(-6).ToShortDateString(),
        DateTime.Today.AddDays(-5).ToShortDateString(),
        DateTime.Today.AddDays(-4).ToShortDateString(),
        DateTime.Today.AddDays(-3).ToShortDateString(),
        DateTime.Today.AddDays(-2).ToShortDateString(),
        DateTime.Today.AddDays(-1).ToShortDateString()
      };

      Dictionary<string, Dictionary<string, string>> dic = new Dictionary<string, Dictionary<string, string>>();

      foreach (string student in students) {
        Dictionary<string, string> dico = new Dictionary<string, string>();

        foreach (string date in dates) {
          string[] statuses = { "absent", "attended", "virtual" };
          int rng = new Random().Next(statuses.Length);
          string s = statuses[rng];

          dico.Add(date, s);
        }

        dic.Add(student, dico);
      }

      StudentsDaysViewModel model = new StudentsDaysViewModel {
        Dates = dates,
        StudentList = students,
        StudentStatuses = dic
      };

      return View(model);
    }

    private string ShortenName(string name) {
      string[] split = name.Split(" ");

      if (split.Length > 2) {
        name = split[0] + " ";

        for (int i = 1; i < split.Length; i++) {
          if (i + 1 == split.Length) {
            name += split[i];
          } else {
            name += split[i].ToCharArray()[0] + ". ";
          }
        }
      }

      return name;
    }
  }
}

