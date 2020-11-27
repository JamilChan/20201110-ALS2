using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class CalculateAbsence {
    public string StudentName { get; set; }
    public double AbsenceInPercent { get; set; }
    public List<DateTime> DaysOfAbsence { get; set; } = new List<DateTime>();

    public List<CalculateAbsence> AbsenceForStudentsInCourse(List<Absence> absenceByCourseList) {
      List<Student> studentList = new List<Student>();
      List<Course> courseList = new List<Course> { absenceByCourseList[0].Course };

      if (absenceByCourseList.TrueForAll(a => a.Course.CourseId == courseList[0].CourseId)) {
        foreach (Absence absence in absenceByCourseList) {
          if (!studentList.Contains(absence.Student)) {
            studentList.Add(absence.Student);
          }
        }

        List<CalculateAbsence> studentsAbsenceList = new List<CalculateAbsence>();
        foreach (Student student in studentList) {
          studentsAbsenceList.Add(AbsenceBuilder(courseList, absenceByCourseList, student));
        }

        return studentsAbsenceList;
      }

      return null;
    }

    public List<CalculateAbsence> AbsenceByEducationAndSemester(List<Absence> absenceList, Education education, int semesterNo) {
      List<Student> studentList = new List<Student>();
      List<Course> courseByEducationList = new List<Course>();

      List<Absence> absenceByEducationList =
        absenceList.FindAll(a => (a.Course.Education.EducationId == education.EducationId) && (a.Student.Semester == semesterNo));

      foreach (Absence absence in absenceByEducationList) {
        if (!studentList.Contains(absence.Student)) {
          studentList.Add(absence.Student);
        }
        if (!courseByEducationList.Contains(absence.Course)) {
          courseByEducationList.Add(absence.Course);
        }
      }

      List<CalculateAbsence> studentsAbsenceList = new List<CalculateAbsence>();
      if (studentList.TrueForAll(s => s.Education.EducationId == education.EducationId)) {
        foreach (Student student in studentList) {
          studentsAbsenceList.Add(AbsenceBuilder(courseByEducationList, absenceByEducationList, student));
        }
      }

      return studentsAbsenceList;
    }

    public List<CalculateAbsence> NotoriousStudents(List<Absence> absenceByCourseList, TimeSpan timeSpan) {
      DateTime todaysDate = DateTime.Today;
      DateTime checkDate = FindTimeSpan(timeSpan, todaysDate);

      List<Student> studentList = new List<Student>();
      List<Absence> absenceInTimeSpanList = new List<Absence>();
      foreach (Absence absence in absenceByCourseList) {
        if (checkDate <= absence.Date && absence.Date <= todaysDate) {
          absenceInTimeSpanList.Add(absence);

          if (!studentList.Contains(absence.Student)) {
            studentList.Add(absence.Student);
          }
        }
      }

      List<CalculateAbsence> notoriousStudentList = new List<CalculateAbsence>();
      foreach (Student student in studentList) {
        CalculateAbsence entry = new CalculateAbsence();
        entry.StudentName = student.Name;

        foreach (Absence absence in absenceInTimeSpanList) {
          if (absence.Student.StudentId == student.StudentId) {
            entry.DaysOfAbsence.Add(absence.Date);
          }
        }

        notoriousStudentList.Add(entry);
      }

      return notoriousStudentList;
    }

    private CalculateAbsence AbsenceBuilder(List<Course> courseList, List<Absence> absenceList, Student student) {
      List<Absence> absenceForStudentList = absenceList.FindAll(a => a.Student.StudentId == student.StudentId);

      double daysOfAbsence = absenceForStudentList.Count;
      double totalCourseDays = FindSchoolDays(courseList);
      double result = (daysOfAbsence / totalCourseDays) * 100;

      CalculateAbsence studentAbsence = new CalculateAbsence { StudentName = student.Name, AbsenceInPercent = result };

      return studentAbsence;
    }

    private DateTime FindTimeSpan(TimeSpan timeSpan, DateTime todaysDate) {
      DateTime checkDate = new DateTime();

      switch (timeSpan) {
        case TimeSpan.Week:
          checkDate = todaysDate.AddDays(-7);
          break;
        case TimeSpan.TwoWeeks:
          checkDate = todaysDate.AddDays(-14);
          break;
        case TimeSpan.ThreeWeeks:
          checkDate = todaysDate.AddDays(-21);
          break;
        case TimeSpan.Month:
          checkDate = todaysDate.AddMonths(-1);
          break;
        default:
          break;
      }

      return checkDate;
    }

    private double FindSchoolDays(List<Course> courseList) {
      double allSchoolDays = 0;

      foreach (Course course in courseList) {
        for (DateTime i = course.StartDate; i < course.EndDate; i = i.AddDays(1)) {
          switch (i.DayOfWeek.ToString()) {
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
        }
      }

      return allSchoolDays;
    }
  }
}
