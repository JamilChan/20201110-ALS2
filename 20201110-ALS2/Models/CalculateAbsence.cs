﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class CalculateAbsence {
    public string StudentName { get; set; }
    public double AbsenceInPercent { get; set; }
    public List<Student> StudentList { get; set; } = new List<Student>();

    public List<CalculateAbsence> AbsenceForStudentsInCourse(List<Absence> absenceByCourseList) {
      List<CalculateAbsence> studentsAbsenceList = new List<CalculateAbsence>();

      foreach (Absence absence in absenceByCourseList) {
        if (!StudentList.Contains(absence.Student)) {
          StudentList.Add(absence.Student);
          studentsAbsenceList.Add(AbsenceBuilder(absenceByCourseList, absence.Student));
        }
      }

      return studentsAbsenceList;
    }

    private CalculateAbsence AbsenceBuilder(List<Absence> absenceList, Student student) {
      List<Absence> absenceForStudentList = absenceList.FindAll(a => a.Student.StudentId == student.StudentId);

      double daysOfAbsence = absenceForStudentList.FindAll(a => a.Status == "absent").Count;
      double totalCourseDays = absenceForStudentList.Count;
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
  }
}
