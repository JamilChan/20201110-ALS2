using System;
using System.Collections.Generic;
using _20201110_ALS2.Models;
using Xunit;

namespace CalcAbsenceTest {
  public class CalculateAbsenceTest {
    [Fact]
    public void SingleStudentAbsenceTest() {
      //Act
      Week weekTest = new Week { WeekId = 1, Monday = true, Tuesday = true, Wednesday = false, Thursday = true, Friday = true };
      Educator educatorTest = new Educator { EducatorId = 1, Name = "Moose" };

      DateTime startDateTest = new DateTime(2020, 11, 30);
      DateTime endDateTest = new DateTime(2021, 1, 02);

      Course courseTest = new Course { CourseId = 1, Name = "Test Fag", Educator = educatorTest, Week = weekTest, StartDate = startDateTest, EndDate = endDateTest };

      Student studentTest = new Student { StudentId = 1, Name = "Carlos", Semester = 3 };
      List<Absence> absenceTestList = new List<Absence>();

      //Assert
      for (int i = 0; i < 4; i++) {
        absenceTestList.Add(new Absence { AbsenceId = i, Student = studentTest, Course = courseTest, Date = startDateTest.AddDays(7) });
      }

      double absDays = DaysOfAbsence(absenceTestList);
      double totalDays = AllDays(courseTest);

      double absenceInPercent = absDays / totalDays;



      //Arrange
      Assert.Equal(0.20, absenceInPercent);
    }
    [Fact]
    public void FindAbsenceList() {
      //Act

      //Assert

      //Arrange
    
    }

    private double AllDays(Course course) {
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

    private double DaysOfAbsence(List<Absence> absenceList) {
      return absenceList.Count;
    }
  }
}
