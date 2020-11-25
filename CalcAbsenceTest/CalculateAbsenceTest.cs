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

      Student studentTest = new Student { StudentId = 1, Name = "Carlos", Education = "Big Dick Power Energy User", Semester = 3 };
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
      DateTime startDateTest = new DateTime(2020, 11, 30);
      DateTime endDateTest = new DateTime(2021, 1, 02);
      Course courseTest = new Course { CourseId = 1, Name = "Test Fag", StartDate = startDateTest, EndDate = endDateTest };

      Student studentTest1 = new Student { StudentId = 1, Name = "Carlos", Education = "Big Dick Power Energy User", Semester = 3 };
      Student studentTest2 = new Student { StudentId = 2, Name = "Hector", Education = "Big Dick Power Energy User", Semester = 3 };
      Student studentTest3 = new Student { StudentId = 3, Name = "Gustavo", Education = "Big Dick Power Energy User", Semester = 3 };
      Student studentTest4 = new Student { StudentId = 4, Name = "Juan", Education = "Big Dick Power Energy User", Semester = 3 };

      List<Student> studentList = new List<Student> { studentTest1, studentTest2, studentTest3, studentTest4 };
      List<Absence> absenceTestList = new List<Absence> { new Absence { AbsenceId = 1, Student = studentTest3, Course = courseTest, Date = startDateTest.AddDays(14) } };

      //Assert
      for (int i = 0; i < 4; i++) {
        absenceTestList.Add(new Absence { AbsenceId = i, Student = studentTest1, Course = courseTest, Date = startDateTest.AddDays(7) });
        absenceTestList.Add(new Absence { AbsenceId = i, Student = studentTest2, Course = courseTest, Date = startDateTest.AddDays(7) });
        absenceTestList.Add(new Absence { AbsenceId = i, Student = studentTest4, Course = courseTest, Date = startDateTest.AddDays(7) });
      }

      int numberOfAbsences = FindAbsenceForStudent(absenceTestList, studentTest3).Count;

      //Arrange
      Assert.Equal(1, numberOfAbsences);
    }

    [Fact]
    public void CalculateAbsenceForStudentTest() {
      //Act
      Week weekTest = new Week { WeekId = 1, Monday = true, Tuesday = true, Wednesday = false, Thursday = true, Friday = true };
      DateTime startDateTest = new DateTime(2020, 11, 30);
      DateTime endDateTest = new DateTime(2021, 1, 02);
      Course courseTest = new Course { CourseId = 1, Name = "Test Fag", Week = weekTest, StartDate = startDateTest, EndDate = endDateTest };

      Student studentTest1 = new Student { StudentId = 1, Name = "Carlos", Education = "Big Dick Power Energy User", Semester = 3 };
      Student studentTest2 = new Student { StudentId = 2, Name = "Hector", Education = "Big Dick Power Energy User", Semester = 3 };
      Student studentTest3 = new Student { StudentId = 3, Name = "Gustavo", Education = "Big Dick Power Energy User", Semester = 3 };
      Student studentTest4 = new Student { StudentId = 4, Name = "Juan", Education = "Big Dick Power Energy User", Semester = 3 };

      List<Student> studentList = new List<Student> { studentTest1, studentTest2, studentTest3, studentTest4 };
      List<Absence> absenceTestList = new List<Absence> { new Absence { Student = studentTest3, Course = courseTest, Date = startDateTest.AddDays(14) } };

      //Assert
      for (int i = 0; i < 4; i++) {
        absenceTestList.Add(new Absence { Student = studentTest1, Course = courseTest, Date = startDateTest.AddDays(7) });
        absenceTestList.Add(new Absence { Student = studentTest2, Course = courseTest, Date = startDateTest.AddDays(7) });
        absenceTestList.Add(new Absence { Student = studentTest4, Course = courseTest, Date = startDateTest.AddDays(7) });
      }

      CalculateAbsence calcAbs = AbsenceForStudent(courseTest, absenceTestList, studentTest4);

      //Arrange
      Assert.Equal("Juan", calcAbs.StudentName);
      Assert.Equal(20, calcAbs.AbsenceInPercent);
    }

    [Fact]
    public void CalculateAbsenceForAllStudentsTest() {
      //Act
      Week weekTest = new Week { WeekId = 1, Monday = true, Tuesday = true, Wednesday = false, Thursday = true, Friday = true };
      DateTime startDateTest = new DateTime(2020, 11, 30);
      DateTime endDateTest = new DateTime(2021, 1, 02);
      Course courseTest = new Course { CourseId = 1, Name = "Test Fag", Week = weekTest, StartDate = startDateTest, EndDate = endDateTest };

      Student studentTest1 = new Student { StudentId = 1, Name = "Carlos", Education = "Big Dick Power Energy User", Semester = 3 };
      Student studentTest2 = new Student { StudentId = 2, Name = "Hector", Education = "Big Dick Power Energy User", Semester = 3 };
      Student studentTest3 = new Student { StudentId = 3, Name = "Gustavo", Education = "Big Dick Power Energy User", Semester = 3 };
      Student studentTest4 = new Student { StudentId = 4, Name = "Juan", Education = "Big Dick Power Energy User", Semester = 3 };

      List<Student> studentList = new List<Student> { studentTest1, studentTest2, studentTest3, studentTest4 };
      List<Absence> absenceTestList = new List<Absence> { new Absence { Student = studentTest3, Course = courseTest, Date = startDateTest.AddDays(14) }, new Absence { Student = studentTest2, Course = courseTest, Date = startDateTest.AddDays(15) } };

      CalculateAbsence studentAbsence1 = new CalculateAbsence();
      CalculateAbsence studentAbsence2 = new CalculateAbsence();
      CalculateAbsence studentAbsence3 = new CalculateAbsence();
      CalculateAbsence studentAbsence4 = new CalculateAbsence();

      //Assert
      for (int i = 0; i < 4; i++) {
        DateTime date = startDateTest.AddDays(1);
        absenceTestList.Add(new Absence { Student = studentTest1, Course = courseTest, Date = startDateTest.AddDays(7) });
        absenceTestList.Add(new Absence { Student = studentTest2, Course = courseTest, Date = startDateTest.AddDays(7) });
        absenceTestList.Add(new Absence { Student = studentTest4, Course = courseTest, Date = startDateTest.AddDays(7) });
        if (i == 3) {
          date = startDateTest.AddDays(-1);
          absenceTestList.Add(new Absence { Student = studentTest4, Course = courseTest, Date = date.AddDays(7) });
        } else {
          absenceTestList.Add(new Absence { Student = studentTest4, Course = courseTest, Date = date.AddDays(7) });
        }
      }

      List<CalculateAbsence> studentAbsenceList = new List<CalculateAbsence>();

      foreach (Student student in studentList) {
        studentAbsenceList.Add(AbsenceForStudent(courseTest, absenceTestList, student));
      }



      foreach (CalculateAbsence calcAbs in studentAbsenceList) {
        if (calcAbs.StudentName == studentTest1.Name) {
          studentAbsence1 = calcAbs;
        } else if (calcAbs.StudentName == studentTest2.Name) {
          studentAbsence2 = calcAbs;
        } else if (calcAbs.StudentName == studentTest3.Name) {
          studentAbsence3 = calcAbs;
        } else if (calcAbs.StudentName == studentTest4.Name) {
          studentAbsence4 = calcAbs;
        }
      }

      //Arrange
      //Student1 Carlos
      Assert.Equal("Carlos", studentAbsence1.StudentName);
      Assert.Equal(20, studentAbsence1.AbsenceInPercent);
      //Student2 Hector
      Assert.Equal("Hector", studentAbsence2.StudentName);
      Assert.Equal(25, studentAbsence2.AbsenceInPercent);
      //Student3 Gustavo
      Assert.Equal("Gustavo", studentAbsence3.StudentName);
      Assert.Equal(5, studentAbsence3.AbsenceInPercent);
      //Student4 Juan
      Assert.Equal("Juan", studentAbsence4.StudentName);
      Assert.Equal(40, studentAbsence4.AbsenceInPercent);
    }

    private CalculateAbsence AbsenceForStudent(Course course, List<Absence> absenceByCourseList, Student student) {
      List<Absence> studentAbsenceList = absenceByCourseList.FindAll(a => a.Student.StudentId == student.StudentId);

      double daysOfAbsence = studentAbsenceList.Count;
      double totalCourseDays = AllDays(course);

      double result = (daysOfAbsence / totalCourseDays) * 100;

      CalculateAbsence studentAbsence = new CalculateAbsence { StudentName = student.Name, AbsenceInPercent = result };

      return studentAbsence;
    }


    private List<Absence> FindAbsenceForStudent(List<Absence> absenceList, Student student) {
      List<Absence> studentAbsenceList = absenceList.FindAll(a => a.Student.StudentId == student.StudentId);

      return studentAbsenceList;
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
