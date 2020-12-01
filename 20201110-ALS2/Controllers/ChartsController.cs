using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20201110_ALS2.Models;
using _20201110_ALS2.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using TimeSpan = _20201110_ALS2.Models.TimeSpan;

namespace _20201110_ALS2.Controllers {
  public class ChartsController : Controller {
    public IStudentRepository studentRepo { get; }
    public IAbsenceRepository absenceRepo { get; set; }
    public ICourseRepository courseRepo { get; }
    public IEducationRepository educationRepo { get; }

    public ChartsController(IStudentRepository studentRepo, IAbsenceRepository absenceRepo, ICourseRepository courseRepo, IEducationRepository educationRepo) {
      this.studentRepo = studentRepo;
      this.absenceRepo = absenceRepo;
      this.courseRepo = courseRepo;
      this.educationRepo = educationRepo;
    }

    [HttpGet]
    public ViewResult Index() {
      return View();
    }

    [HttpGet]
    public IActionResult CourseStudents(int courseId) {
      if (courseId == 0) {
        courseId = 1;
      }

      Course course = courseRepo.Courses.FirstOrDefault(c => c.CourseId == courseId);

      List<Absence> tempAbsences = absenceRepo.AbsenceByCourse(course);

      List<CalculateAbsence> absenceList = new CalculateAbsence().AbsenceForStudentsInCourse(tempAbsences);

      return View("CourseStudents", new StudentsViewModel {
        Absences = absenceList,
        CourseId = courseId
      });
    }

    [HttpGet]
    public IActionResult EducationStudents(int educationId, int semesterNo) {
      Education education = educationRepo.Educations.FirstOrDefault(e => e.EducationId == educationId);

      List<Absence> tempAbsences = absenceRepo.AbsenceBy(education, semesterNo);

      List<CalculateAbsence> absenceList = new CalculateAbsence().AbsenceForStudentsInCourse(tempAbsences);

      return View("CourseStudents", new StudentsViewModel {
        Absences = absenceList,
        EducationId = educationId
      });
    }

    [HttpGet]
    public ViewResult CourseStudentsDays(int courseId, TimeSpan span) {
      if (courseId == 0) {
        courseId = 1;
      }

      Course course = courseRepo.Courses.FirstOrDefault(c => c.CourseId == courseId);
      List<Student> studentList = studentRepo.GetAllStudentsFromCourse(course);
      List<string> students = new List<string>();
      List<string> dates = GetDateSpan(span);

      Dictionary<string, Dictionary<string, string>> dic = new Dictionary<string, Dictionary<string, string>>();

      foreach (Student student in studentList) {
        Dictionary<string, string> dico = new Dictionary<string, string>();

        foreach (string date in dates) {
          Absence a = absenceRepo.AbsenceForDateCourseStudent(course, DateTime.Parse(date), student);
          dico.Add(date, a != null ? a.Status : "");
        }

        string s = ShortenName(student.Name);
        students.Add(s);
        dic.Add(s, dico);
      }

      return View(new StudentsDaysViewModel {
        Dates = dates,
        StudentList = students,
        StudentStatuses = dic,
        CourseId = courseId
      });
    }

    [HttpGet]
    public ViewResult EducationStudentsDays(int educationId, int semesterNo, TimeSpan span) {

      Education education = educationRepo.Educations.FirstOrDefault(e => e.EducationId == educationId);
      List<Student> studentList = studentRepo.GetAllStudentsFromEducationSemester(education, semesterNo);
      List<string> students = new List<string>();
      List<string> dates = GetDateSpan(span);

      Dictionary<string, Dictionary<string, string>> dic = new Dictionary<string, Dictionary<string, string>>();

      foreach (Student student in studentList) {
        Dictionary<string, string> dico = new Dictionary<string, string>();

        foreach (string date in dates) {
          Absence a = absenceRepo.AbsenceForDateEducationStudent(education, DateTime.Parse(date), student);
          dico.Add(date, a != null ? a.Status : "");
        }

        string s = ShortenName(student.Name);
        students.Add(s);
        dic.Add(s, dico);
      }

      return View("CourseStudentsDays", new StudentsDaysViewModel {
        Dates = dates,
        StudentList = students,
        StudentStatuses = dic,
        EducationId = educationId,
        SemesterNo = semesterNo
      });
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

    private List<string> GetDateSpan(TimeSpan span) {
      List<string> dates = new List<string>();

      switch (span) {
        case TimeSpan.Week:
          for (DateTime i = DateTime.Today.AddDays(-6); i <= DateTime.Today; i = i.AddDays(1)) {
            dates.Add(i.ToShortDateString());
          }
          break;
        case TimeSpan.TwoWeeks:
          for (DateTime i = DateTime.Today.AddDays(-13); i <= DateTime.Today; i = i.AddDays(1)) {
            dates.Add(i.ToShortDateString());
          }
          break;
        case TimeSpan.ThreeWeeks:
          for (DateTime i = DateTime.Today.AddDays(-20); i <= DateTime.Today; i = i.AddDays(1)) {
            dates.Add(i.ToShortDateString());
          }
          break;
        case TimeSpan.Month:
          for (DateTime i = DateTime.Today.AddMonths(-1).AddDays(1); i <= DateTime.Today; i = i.AddDays(1)) {
            dates.Add(i.ToShortDateString());
          }
          break;
      }

      return dates;
    }
  }
}

