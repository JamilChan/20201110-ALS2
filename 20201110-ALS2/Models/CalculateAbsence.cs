using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class CalculateAbsence {
    public string StudentName { get; set; }
    public double AbsenceInPercent { get; set; }

    public List<CalculateAbsence> CalcAllAbsences(Course course, List<Absence> absenceByCourseList,
      List<Student> studentList) {
      List<CalculateAbsence> studentAbsenceList = new List<CalculateAbsence>();

      foreach (Student student in studentList) {
        studentAbsenceList.Add(AbsenceForStudent(course, absenceByCourseList, student));
      }

      return studentAbsenceList;
    }

    private CalculateAbsence AbsenceForStudent(Course course, List<Absence> absenceByCourseList, Student student) {
      List<Absence> studentAbsenceList = absenceByCourseList.FindAll(a => a.Student.StudentId == student.StudentId);

      double daysOfAbsence = studentAbsenceList.Count;
      double totalCourseDays = TotalDays(course);

      double result = (daysOfAbsence / totalCourseDays) * 100;

      CalculateAbsence studentAbsence = new CalculateAbsence { StudentName = student.Name, AbsenceInPercent = result };

      return studentAbsence;
    }

    private double TotalDays(Course course) {
      double allSchoolDays = 0;
      DateTime currentDate = course.StartDate;

      double startEndDiff = (course.EndDate - course.StartDate).TotalDays;

      for (double i = 0; i < startEndDiff; i++) {
        switch (currentDate.DayOfWeek.ToString()) {
          case "Monday":
            if (course.Week.Monday) {
              allSchoolDays++;
            }
            break;
          case "Tuesday":
            if (course.Week.Tuesday) {
              allSchoolDays++;
            }
            break;
          case "Wednesday":
            if (course.Week.Wednesday) {
              allSchoolDays++;
            }
            break;
          case "Thursday":
            if (course.Week.Thursday) {
              allSchoolDays++;
            }
            break;
          case "Friday":
            if (course.Week.Friday) {
              allSchoolDays++;
            }
            break;
          default:
            break;
        }

        currentDate = currentDate.AddDays(1);
      }

      return allSchoolDays;
    }
  }
}
