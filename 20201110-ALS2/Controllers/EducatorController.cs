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
    private readonly IAbsenceRepository absenceRepo;
    private readonly IEducationRepository educationRepo;

    public EducatorController(IStudentRepository studentRepo, ICourseRepository courseRepo, IEducatorRepository educatorRepo, IAbsenceRepository absenceRepo, IEducationRepository educationRepo) {
      this.studentRepo = studentRepo;
      this.courseRepo = courseRepo;
      this.educatorRepo = educatorRepo;
      this.absenceRepo = absenceRepo;
      this.educationRepo = educationRepo;
    }

    [HttpGet]
    public ViewResult AbsenceList(long courseId, long educationId, string dateString, bool edit) {
      StudentListViewModel model = new StudentListViewModel();
      string[] dateSplit = dateString.Split("-", 3);
      DateTime date = new DateTime(int.Parse(dateSplit[2].Split(" ", 2)[0]), Int32.Parse(dateSplit[1]), int.Parse(dateSplit[0]));

      if (educationId != 0) {
        Education education = educationRepo.Educations.FirstOrDefault(e => e.EducationId == educationId);
        List<Student> students = studentRepo.GetAllStudentsFromEducation(education);

        model = new StudentListViewModel {
          Date = date,
          AbsencesList = AbsenceForStudentListByEducation(education, date),
          Edit = edit,
          IndicationList = new CalculateAbsence().IndicationForStudents(students, absenceRepo.AbsenceByEducation(education)),
          IsChecked = "on",
          StatusList = new string[students.Count],
          StudentsList = students,
          Education = education
        };
      } else if (courseId != 0) {
        Course course = ApplyCourseWithId(courseId);
        List<Student> students = studentRepo.GetAllStudentsFromCourse(course);

        model = new StudentListViewModel {
          Date = date,
          AbsencesList = AbsenceForStudentList(courseId, date),
          Edit = edit,
          IndicationList = new CalculateAbsence().IndicationForStudents(students, absenceRepo.AbsenceByCourse(course)),
          IsChecked = "on",
          StatusList = new string[students.Count],
          StudentsList = students,
          Course = course,
        };
      }

      ViewBag.Check = true;

      return View("AbsenceList", model);
    }

    [HttpPost]
    public IActionResult AbsenceList(StudentListViewModel model) {
      List<string> statusList = model.StatusList.ToList();
      List<Absence> absenceList = new List<Absence>();

      // If education is selected
      if (model.Education.EducationId != 0) {
        Education education = educationRepo.Educations.FirstOrDefault(e => e.EducationId == model.Education.EducationId);
        List<Course> courses = courseRepo.CoursesByEducationAndDate(education, model.Date);

        if (!model.Edit) {
          foreach (string s in statusList) {
            string[] split = s.Split(":", 2);

            foreach (Student student in studentRepo.Students) {
              if (student.StudentId.ToString() == split[0]) {

                foreach (Course course in courses) {

                  foreach (StudentCourse studentCourse in course.StudentCourses) {
                    if (student.StudentId == studentCourse.StudentId) {
                      Absence absence = new Absence {
                        Student = student,
                        Date = model.Date,
                        Course = course,
                        Status = split[1]
                      };
                      absenceList.Add(absence);
                    }
                  }
                }
                break;
              }
            }
          }

          absenceRepo.CreateAbsence(absenceList);
        } else {
          absenceList = absenceRepo.AbsencesForDateEducation(model.Education, model.Date).ToList();

          absenceRepo.UpdateAbsence(absenceList, statusList);
        }

        // If course is selected
      } else if (model.Course.CourseId != 0) {
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
      }

      ViewBag.Check = false;

      return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult Toggle(StudentListViewModel model) {

      if (model.Education.EducationId != 0) {
        Education education = educationRepo.Educations.FirstOrDefault(e => e.EducationId == model.Education.EducationId);
        model.Education = education;

        model.StudentsList = studentRepo.GetAllStudentsFromEducation(education);
        model.IndicationList =
          new CalculateAbsence().IndicationForStudents(model.StudentsList, absenceRepo.AbsenceByEducation(education));
        model.Course = null;
      } else if (model.Course.CourseId != 0) {
        Course course = ApplyCourseWithId(model.Course.CourseId);
        model.Course = course;

        model.StudentsList = studentRepo.GetAllStudentsFromCourse(course);
        model.IndicationList = new CalculateAbsence().IndicationForStudents(model.StudentsList, absenceRepo.AbsenceByCourse(course));
      }

      if (model.IsChecked != "off") {
        model.IsChecked = "off";
        ViewBag.Check = false;
        return View("AbsenceList", model);
      } else {
        model.IsChecked = "on";
        ViewBag.Check = true;
        return View("AbsenceList", model);
      }
    }

    [HttpGet]
    public ViewResult CreateCourse() {
      CreateCourseViewModel model = CreateCCVM();

      return View("CreateCourse", model);
    }

    [HttpPost]
    public IActionResult CreateCourse(CreateCourseViewModel model, IFormCollection form) {
      if (ModelState.IsValid) {
        foreach (Educator educator in educatorRepo.Educators) {
          if (educator.Name == model.SelectedEducator) {
            model.Course.Educator = educator;
            break;
          }
        }

        string selected = Request.Form["SelectedStudents"].ToString();
        string[] selectedList = selected.Split(',');
        List<StudentCourse> studentCourseList = new List<StudentCourse>();

        if (selectedList[0] != "") {
          foreach (string studentId in selectedList) {
            StudentCourse studentCourse = new StudentCourse { Course = model.Course, Student = studentRepo.Students.FirstOrDefault(s => s.StudentId == Int64.Parse(studentId)) };
            studentCourseList.Add(studentCourse);
          }

          model.Course.StudentCourses = studentCourseList;
        }

        model.Course.Education = educationRepo.Educations.FirstOrDefault(e => e.EducationId == 3); // DELETE ME! SIMONS TEST DA JEG INGEN EDUCATION HAR OG S:TTE
        courseRepo.SaveCourse(model.Course);
        TempData["message"] = $"{model.Course.Name} has been saved";
        return RedirectToAction("ViewCourses");
      } else {
        model = CreateCCVM();

        return View("CreateCourse", model);
      }
    }

    [HttpGet]
    public ViewResult ViewCourses() {
      return View("ViewCourses", courseRepo.Courses);
    }

    [HttpGet]
    public ViewResult EditCourse(int courseId) {
      CreateCourseViewModel model = CreateCCVM();
      model.Course = courseRepo.Courses.FirstOrDefault(c => c.CourseId == courseId);
      model.SelectedEducator = model.Course.Educator.Name;
      model.CheckedStudentList = courseRepo.SelectedStudents(courseId);
      model.Edit = true;

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
      model.StudentList = studentRepo.GetAllStudentsFromCourse(model.Course);

      return View("ViewThisCourse", model);
    }

    public CreateCourseViewModel CreateCCVM() {
      CreateCourseViewModel model = new CreateCourseViewModel();
      model.Educators = educatorRepo.Educators;
      model.GetEducatorsName();
      model.Students = studentRepo.Students;
      model.Course.Week = new Week();
      model.Course.Week.WeekId = 0;

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

    private List<Absence> AbsenceForStudentListByEducation(Education education, DateTime date) {
      List<Absence> absenceList = new List<Absence>();

      foreach (Absence absence in absenceRepo.Absences) {
        if (absence.Student.Education == education && absence.Date.Date == date.Date && !absenceList.Exists(a => a.Student.StudentId == absence.Student.StudentId)) {
          absenceList.Add(absence);
        }
      }

      return absenceList;
    }
  }
}