using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20201110_ALS2.Models;
using _20201110_ALS2.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _20201110_ALS2.Controllers {
  public class EducatorController : Controller {
    private IStudentRepository studentRepo;
    private ICourseRepository courseRepo;
    private IEducatorRepository educatorRepo;
    private IEducationRepository educationRepo;
    private readonly IAbsenceRepository absenceRepo;

    public EducatorController(IStudentRepository studentRepo, ICourseRepository courseRepo, IEducatorRepository educatorRepo, IAbsenceRepository absenceRepo, IEducationRepository educationRepo) {
      this.studentRepo = studentRepo;
      this.courseRepo = courseRepo;
      this.educatorRepo = educatorRepo;
      this.educationRepo = educationRepo;
      this.absenceRepo = absenceRepo;
    }

    [HttpGet]
    public ViewResult AbsenceList(long courseId, string dateString, bool edit) {
      ViewBag.Check = true;

      string[] dateSplit = dateString.Split("-", 3);

      DateTime date = new DateTime(Int32.Parse(dateSplit[2].Split(" ", 2)[0]), Int32.Parse(dateSplit[1]), int.Parse(dateSplit[0]));

      Course course = ApplyCourseWithId(courseId);

      StudentListViewModel model = new StudentListViewModel {
        StatusList = new string[studentRepo.Students.ToList().Count],
        StudentsList = studentRepo.GetAllStudentsFromCourses(course),
        AbsencesList = AbsenceForStudentList(courseId, date),
        Course = course,
        Date = date,
        Edit = edit
      };

      return View("AbsenceList", model);
    }

    [HttpPost]
    public IActionResult AbsenceList(StudentListViewModel model) {
      List<string> statusList = model.StatusList.ToList();

      List<Absence> absenceList = new List<Absence>();

      if (!model.Edit) {
        foreach (string s in statusList) {
          string[] split = s.Split(":", 2);

          foreach (Student student in studentRepo.Students) {
            if (student.StudentId.ToString() == split[0]) {

              Course selectedCourse = new Course();
              foreach (Course course in courseRepo.Courses) {
                if (course.CourseId == model.Course.CourseId) {
                  selectedCourse = course;
                }
              }

              Absence absence = new Absence {
                Student = student,
                Date = model.Date,
                Course = selectedCourse,
                Status = split[1]
              };
              absenceList.Add(absence);
              break;
            }
          }
        }

        absenceRepo.CreateAbsence(absenceList);
      } else {
        absenceList = absenceRepo.AbsencesForDateCourse(model.Course, model.Date).ToList();

        absenceRepo.UpdateAbsence(absenceList, statusList);
      }

      ViewBag.Check = false;

      return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult Toggle(StudentListViewModel studentList) {
      Course course = ApplyCourseWithId(studentList.Course.CourseId);
      studentList.Course = course;

      studentList.StudentsList = studentRepo.GetAllStudentsFromCourses(course);

      if (studentList.IsChecked == "on") {
        ViewBag.Check = true;
        return View("AbsenceList", studentList);
      } else {
        ViewBag.Check = false;
        return View("AbsenceList", studentList);
      }
    }

    [HttpGet]
    public ViewResult CreateCourse() {
      CreateCourseViewModel model = CreateCCVM(1);
      ViewBag.search = false;

      return View("CreateCourse", model);
    }

    [HttpPost]
    public IActionResult CreateCourse(CreateCourseViewModel model, IFormCollection form, long educationId, int semesterNo, bool search) {
      if (ModelState.IsValid && !search) {
        model.Course.Educator = educatorRepo.Educators.FirstOrDefault(e => e.Name == model.Course.Educator.Name);

        List<StudentCourse> studentCourseList = new List<StudentCourse>();
        string selected = Request.Form["SelectedStudents"].ToString();

        string[] selectedList = selected.Split(',');

				if (selectedList[0] != "") {
					foreach (string studentId in selectedList) {
						StudentCourse studentCourse = new StudentCourse { Course = model.Course, Student = studentRepo.Students.FirstOrDefault(s => s.StudentId == Int64.Parse(studentId)) };
						studentCourseList.Add(studentCourse);
					}
				}

				model.Course.Education = educationRepo.Educations.FirstOrDefault(e => e.EducationId == educationId);
				model.Course.StudentCourses = studentCourseList;

        courseRepo.SaveCourse(model.Course);
        TempData["message"] = $"{model.Course.Name} has been saved";

        return RedirectToAction("ViewCourses");
      } else {
        model = CreateCCVM(educationId);

        if (semesterNo != 0) {
          model.StudentList = model.StudentList.FindAll(s => s.Semester == semesterNo);
          model.CheckedStudentList = model.StudentList;

          ViewBag.selectedSemesterNo = semesterNo;
        }

        ViewBag.search = search;

        return View("CreateCourse", model);
      }
    }

    [HttpGet]
    public ViewResult ViewCourses() {
      return View("ViewCourses", courseRepo.Courses);
    }

    [HttpGet]
    public ViewResult EditCourse(int courseId) {
      Course course = courseRepo.Courses.FirstOrDefault(c => c.CourseId == courseId);

      CreateCourseViewModel model = CreateCCVM((long) course.EducationId);
      model.Course = course;
      model.CheckedStudentList = courseRepo.SelectedStudents(courseId);
      model.Edit = true;
      ViewBag.search = false;

      return View("CreateCourse", model);
    }

    [HttpPost]
    public IActionResult DeleteCourse(int courseId) {
      courseRepo.Delete(courseId);

      return View("ViewCourses", courseRepo.Courses);
    }

    [HttpGet]
    public ViewResult ViewThisCourse(int courseId) {
      ViewCourseViewModel model = new ViewCourseViewModel();
      model.Course = courseRepo.Courses.FirstOrDefault(c => c.CourseId == courseId);
      model.StudentList = studentRepo.GetAllStudentsFromCourses(model.Course);

      return View("ViewThisCourse", model);
    }

    private CreateCourseViewModel CreateCCVM(long educationId) {
      CreateCourseViewModel model = new CreateCourseViewModel();
      Education education = educationRepo.Educations.FirstOrDefault(e => e.EducationId == educationId);

      model.Educators = educatorRepo.Educators;
      model.Educations = educationRepo.Educations;
      model.GetCourseInfoName();
      model.StudentList = studentRepo.Students.Where(s => s.Education == education).OrderBy(s => s.Name).ToList();
      model.CheckedStudentList = model.StudentList;
      model.Course.Week = new Week();
      model.Course.Week.WeekId = 0;

      ViewBag.selectedEducationId = educationId;

      return model;
    }

		private Course ApplyCourseWithId(long courseId) {
      List<Course> courseList = courseRepo.Courses.ToList();

      foreach (Course course in courseList) {
        if (course.CourseId == courseId) {
          return course;
        }
      }

      return null;
    }

    private List<Absence> AbsenceForStudentList(long courseId, DateTime date) {
      List<Absence> absenceList = new List<Absence>();

      foreach (Absence absence in absenceRepo.Absences) {
        if (absence.Course.CourseId == courseId && absence.Date.Date == date.Date) {
          absenceList.Add(absence);
        }
      }

      return absenceList;
    }
  }
}
