using System;
using System.Collections.Generic;
using System.Text;
using _20201110_ALS2.Models;
using Xunit;

namespace CalcAbsenceTest {
  public class StudentAbsence {
    [Fact]
    public void TestDaysInACourse() {
      //Arrange
      DateTime startDateTest = new DateTime(2020, 11, 30);
      DateTime endDateTest = new DateTime(2021, 1, 02);
      Week weekTest1 = new Week { Monday = true, Tuesday = true, Wednesday = false, Thursday = true, Friday = true };
      Week weekTest2 = new Week { Monday = false, Tuesday = true, Wednesday = false, Thursday = false, Friday = true };
      Course courseTest1 = new Course { Name = "ProTek3", StartDate = startDateTest, EndDate = endDateTest, Week = weekTest1 };
      Course courseTest2 = new Course { StartDate = startDateTest, EndDate = endDateTest, Week = weekTest2 };

      List<Course> oneCourse1 = new List<Course> { courseTest1 };
      List<Course> oneCourse2 = new List<Course> { courseTest2 };
      List<Course> courseByEducationList = new List<Course> { courseTest1, courseTest2 };

      //Act
      double course1Days = FindSchoolDays(oneCourse1);
      double course2Days = FindSchoolDays(oneCourse2);
      double allDays = FindSchoolDays(courseByEducationList);


      //Assert
      Assert.Equal(20, course1Days);
      Assert.Equal(10, course2Days);
      Assert.Equal(30, allDays);

    }

    [Fact]
    public void StudentAbsenceByEducation() {
      //Method parameters: (Education education, int semesterNo)
      //Find fag 
      //Find studerende
      //Find skoledage
      //find fraværsdage i fag
      //Udregn fravær

      //Arrange
      Education educationTest = new Education { Name = "Datamatiker" };
      int semesterNo = 3;

      List<Course> courseByEducationList = FindCourseByEducation(educationTest);

      List<Student> studentsBySemesterList = FindStudentsBySemesterNo(semesterNo);

      //Absence absenceTest1 = new Absence { Student = student, Course = courseTest1, Date = startDateTest.AddDays(14) };
      //Absence absenceTest2 = new Absence { Student = student, Course = courseTest1, Date = startDateTest.AddDays(15) };
      //Absence absenceTest3 = new Absence { Student = student, Course = courseTest2, Date = startDateTest.AddDays(1) };
      //Absence absenceTest4 = new Absence { Student = student, Course = courseTest2, Date = startDateTest.AddDays(3) };
      //Absence absenceTest5 = new Absence { Student = student, Course = courseTest2, Date = startDateTest.AddDays(8) };
      //Absence absenceTest6 = new Absence { Student = student, Course = courseTest2, Date = startDateTest.AddDays(10) };

      //List<Absence> absenceList = new List<Absence> { absenceTest1, absenceTest2, absenceTest3, absenceTest4, absenceTest5, absenceTest6 }; ;
      ////Act
      //double absDays = absenceList.Count;
      //double totalDays = FindSchoolDays(courseByEducationList);

      //double absenceInPercent = absDays / totalDays;

      ////Assert
      //Assert.Equal(0.2, absenceInPercent);
    }

    private void IndicationForWeeklyAbsence(List<Student> studentsByEducation, List<Absence> absenceByCourseList) {

    }

    private List<Student> FindStudentsBySemesterNo(int semesterNo)
    {
      Student student1 = new Student { Semester = 3 };
      Student student2 = new Student { Semester = 3 };
      Student student3 = new Student { Semester = 3 };
      Student student4 = new Student { Semester = 5 };
      Student student5 = new Student { Semester = 1 };

      List<Student> studentList = new List<Student>{student1, student2, student3, student4, student5};

      return studentList.FindAll(s => s.Semester == semesterNo);
    }

    private List<Course> FindCourseByEducation(Education education) {
      Education educationTest1 = new Education { Name = "Datamatiker" };
      Education educationTest2 = new Education { Name = "Multimedie-deisgn" };

      DateTime startDateTest = new DateTime(2020, 11, 30);
      DateTime endDateTest = new DateTime(2021, 1, 02);
      Week weekTest1 = new Week { Monday = true, Tuesday = true, Wednesday = false, Thursday = true, Friday = true };
      Week weekTest2 = new Week { Monday = false, Tuesday = true, Wednesday = false, Thursday = false, Friday = true };
      Week weekTest3 = new Week { Monday = true, Tuesday = true, Wednesday = false, Thursday = true, Friday = true };
      Week weekTest4 = new Week { Monday = false, Tuesday = true, Wednesday = false, Thursday = false, Friday = true };
      Course courseTest1 = new Course { Education = educationTest1, Name = "ProTek3", StartDate = startDateTest, EndDate = endDateTest, Week = weekTest1 };
      Course courseTest2 = new Course { Education = educationTest1, Name = "SystemUdvikling", StartDate = startDateTest, EndDate = endDateTest, Week = weekTest2 };
      Course courseTest3 = new Course { Education = educationTest2, Name = "ProTek1", StartDate = startDateTest, EndDate = endDateTest, Week = weekTest3 };
      Course courseTest4 = new Course { Education = educationTest2, Name = "Tegn Tegninger", StartDate = startDateTest, EndDate = endDateTest, Week = weekTest4 };
      List<Course> courseByEducationList = new List<Course> { courseTest1, courseTest2, courseTest3, courseTest4 };

      return courseByEducationList.FindAll(c => c.Education == education);
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

    //private double TotalDays(List<Course> courseList) {
    //  double allSchoolDays = 0;

    //  foreach (Course course in courseList) {
    //    foreach (bool schoolDay in FindSchoolDays(course)) {
    //      if (schoolDay) {
    //        allSchoolDays++;
    //      }
    //    }
    //  }

    //  return allSchoolDays;
    //}

    //private List<bool> FindSchoolDays(Course course) {
    //  List<bool> schoolDaysList = new List<bool>();

    //  for (DateTime i = course.StartDate; i < course.EndDate; i = i.AddDays(1)) {
    //    schoolDaysList.Add(course.Week.Monday);
    //    schoolDaysList.Add(course.Week.Tuesday);
    //    schoolDaysList.Add(course.Week.Wednesday);
    //    schoolDaysList.Add(course.Week.Thursday);
    //    schoolDaysList.Add(course.Week.Friday);
    //    schoolDaysList.Add(false);
    //    schoolDaysList.Add(false);
    //  }


    //  return schoolDaysList;
    //}
  }
}
