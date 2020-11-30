using System;
using System.Collections.Generic;
using System.Text;
using _20201110_ALS2.Models;
using Xunit;
using TimeSpan = _20201110_ALS2.Models.TimeSpan;

namespace CalcAbsenceTest {
  public class DynamicCalcAbsence {
    [Fact]
    public void AbsenceByCourseTest() {
      //Arrange
      DateTime startDateTest = new DateTime(2020, 11, 30);
      DateTime endDateTest = new DateTime(2021, 1, 02);
      Week weekTest1 = new Week { Monday = true, Tuesday = true, Wednesday = false, Thursday = true, Friday = true };
      Course courseTest1 = new Course { CourseId = 1, Name = "ProTek3", StartDate = startDateTest, EndDate = endDateTest, Week = weekTest1 };

      Student studentTest1 = new Student { StudentId = 1, Name = "Carlos" };
      Student studentTest2 = new Student { StudentId = 2, Name = "Hector" };

      List<Absence> absenceByCourseList = new List<Absence>();

      for (int i = 0; i < 16; i++) { // 10 absences
        if (!(i == 2 || i == 5 || i == 6 || i == 9 || i == 12 || i == 13)) {
          absenceByCourseList.Add(new Absence { Course = courseTest1, Date = startDateTest.AddDays(i), Student = studentTest1 });
        }
      }
      absenceByCourseList.Add(new Absence { Course = courseTest1, Date = startDateTest.AddDays(1), Student = studentTest2 });

      //Act
      List<CalculateAbsence> absenceList = AbsenceForStudentsInCourse(absenceByCourseList);

      //Assert
      Assert.Equal(2, absenceList.Count);
      Assert.Equal("Carlos", absenceList[0].StudentName);
      Assert.Equal("Hector", absenceList[1].StudentName);
      Assert.Equal(50, absenceList[0].AbsenceInPercent);
      Assert.Equal(5, absenceList[1].AbsenceInPercent);
    }

    [Fact]
    public void AbsenceByEducationAndSemesterNoTest() {
      //Arrange
      //Fag og Uddannelse
      DateTime startDateTest = new DateTime(2020, 11, 30);
      DateTime endDateTest = new DateTime(2021, 1, 02);
      Education educationTest1 = new Education { EducationId = 1, Name = "Datamatiker" };
      Education educationTest2 = new Education { EducationId = 2, Name = "Multimedie-deisgn" };

      Week weekTest1 = new Week { Monday = true, Tuesday = true, Wednesday = false, Thursday = true, Friday = true };
      Week weekTest2 = new Week { Monday = false, Tuesday = true, Wednesday = false, Thursday = false, Friday = true };
      Week weekTest3 = new Week { Monday = true, Tuesday = true, Wednesday = false, Thursday = true, Friday = true };
      Week weekTest4 = new Week { Monday = false, Tuesday = true, Wednesday = false, Thursday = true, Friday = false };
      Course courseTest1 = new Course { Education = educationTest1, Name = "ProTek3", StartDate = startDateTest, EndDate = endDateTest, Week = weekTest1 };
      Course courseTest2 = new Course { Education = educationTest1, Name = "SystemUdvikling", StartDate = startDateTest, EndDate = endDateTest, Week = weekTest2 };
      Course courseTest3 = new Course { Education = educationTest1, Name = "ProTek1", StartDate = startDateTest, EndDate = endDateTest, Week = weekTest3 };
      Course courseTest4 = new Course { Education = educationTest2, Name = "Tegn Tegninger1", StartDate = startDateTest, EndDate = endDateTest, Week = weekTest4 };
      Course courseTest5 = new Course { Education = educationTest2, Name = "Tegn Tegninger2", StartDate = startDateTest, EndDate = endDateTest, Week = weekTest4 };

      //Studerende
      Student studentTest1 = new Student { StudentId = 1, Name = "Carlos", Education = educationTest1, Semester = 1 }; //5 absences
      Student studentTest2 = new Student { StudentId = 2, Name = "Hector", Education = educationTest1, Semester = 3 }; //15 absences
      Student studentTest3 = new Student { StudentId = 3, Name = "Gustavo", Education = educationTest2, Semester = 1 }; //4 absences
      Student studentTest4 = new Student { StudentId = 4, Name = "Juan", Education = educationTest2, Semester = 3 }; //2 absences

      List<Absence> absenceList = new List<Absence>();

      //5 absences for Datamatiker på 1. semester
      absenceList.Add(new Absence { Course = courseTest3, Date = startDateTest.AddDays(1), Student = studentTest1 });
      absenceList.Add(new Absence { Course = courseTest3, Date = startDateTest.AddDays(3), Student = studentTest1 });
      absenceList.Add(new Absence { Course = courseTest3, Date = startDateTest.AddDays(4), Student = studentTest1 });
      absenceList.Add(new Absence { Course = courseTest3, Date = startDateTest.AddDays(8), Student = studentTest1 });
      absenceList.Add(new Absence { Course = courseTest3, Date = startDateTest.AddDays(10), Student = studentTest1 });

      for (int i = 0; i < 16; i++) { // 10 absences for Datamatiker på 3. semester
        if (!(i == 2 || i == 5 || i == 6 || i == 9 || i == 12 || i == 13)) {
          absenceList.Add(new Absence { Course = courseTest1, Date = startDateTest.AddDays(i), Student = studentTest2 });
        }
      }
      // 5 absences more for Datamatiker på 3. semester
      absenceList.Add(new Absence { Course = courseTest2, Date = startDateTest.AddDays(1), Student = studentTest2 });
      absenceList.Add(new Absence { Course = courseTest2, Date = startDateTest.AddDays(4), Student = studentTest2 });
      absenceList.Add(new Absence { Course = courseTest2, Date = startDateTest.AddDays(8), Student = studentTest2 });
      absenceList.Add(new Absence { Course = courseTest2, Date = startDateTest.AddDays(11), Student = studentTest2 });
      absenceList.Add(new Absence { Course = courseTest2, Date = startDateTest.AddDays(15), Student = studentTest2 });

      //4 absences for Multimedie designere på 1. semester
      absenceList.Add(new Absence { Course = courseTest4, Date = startDateTest.AddDays(1), Student = studentTest3 });
      absenceList.Add(new Absence { Course = courseTest4, Date = startDateTest.AddDays(3), Student = studentTest3 });
      absenceList.Add(new Absence { Course = courseTest4, Date = startDateTest.AddDays(8), Student = studentTest3 });
      absenceList.Add(new Absence { Course = courseTest4, Date = startDateTest.AddDays(10), Student = studentTest3 });

      //2 absences for Multimedie designere på 3. semester
      absenceList.Add(new Absence { Course = courseTest5, Date = startDateTest.AddDays(1), Student = studentTest4 });
      absenceList.Add(new Absence { Course = courseTest5, Date = startDateTest.AddDays(3), Student = studentTest4 });

      //Act
      List<CalculateAbsence> calcAbsList = AbsenceByEducationAndSemester(absenceList, educationTest1, 3);
      List<CalculateAbsence> calcAbsList1 = AbsenceByEducationAndSemester(absenceList, educationTest1, 1);
      List<CalculateAbsence> calcAbsList2 = AbsenceByEducationAndSemester(absenceList, educationTest2, 1);
      List<CalculateAbsence> calcAbsList3 = AbsenceByEducationAndSemester(absenceList, educationTest2, 3);

      //Assert
      Assert.Single(calcAbsList);
      Assert.Equal("Hector", calcAbsList[0].StudentName);
      Assert.Equal(50, calcAbsList[0].AbsenceInPercent);

      Assert.Single(calcAbsList1);
      Assert.Equal("Carlos", calcAbsList1[0].StudentName);
      Assert.Equal(25, calcAbsList1[0].AbsenceInPercent);

      Assert.Single(calcAbsList2);
      Assert.Equal("Gustavo", calcAbsList2[0].StudentName);
      Assert.Equal(40, calcAbsList2[0].AbsenceInPercent);

      Assert.Single(calcAbsList3);
      Assert.Equal("Juan", calcAbsList3[0].StudentName);
      Assert.Equal(20, calcAbsList3[0].AbsenceInPercent);
    }

    [Fact]
    public void NotoriousStudentsTest() {
      //Arrange
      //Fag og Uddannelse
      DateTime startDateTest = new DateTime(2020, 11, 30);
      DateTime endDateTest = new DateTime(2021, 1, 02);
      Education educationTest1 = new Education { EducationId = 1, Name = "Datamatiker" };

      Week weekTest1 = new Week { Monday = true, Tuesday = true, Wednesday = false, Thursday = true, Friday = true };
      Week weekTest2 = new Week { Monday = false, Tuesday = true, Wednesday = false, Thursday = false, Friday = true };
      Course courseTest1 = new Course { Education = educationTest1, Name = "ProTek3", StartDate = startDateTest, EndDate = endDateTest, Week = weekTest1 };
      Course courseTest2 = new Course { Education = educationTest1, Name = "SystemUdvikling", StartDate = startDateTest, EndDate = endDateTest, Week = weekTest2 };

      //Studerende
      Student studentTest1 = new Student { StudentId = 1, Name = "Carlos", Education = educationTest1, Semester = 3 }; //5 absences
      Student studentTest2 = new Student { StudentId = 2, Name = "Hector", Education = educationTest1, Semester = 3 }; //20 absences
      Student studentTest3 = new Student { StudentId = 3, Name = "Gustavo", Education = educationTest1, Semester = 3 }; //4 absences
      Student studentTest4 = new Student { StudentId = 4, Name = "Juan", Education = educationTest1, Semester = 3 }; //2 absences

      List<Absence> absenceCourse1List = new List<Absence>();
      List<Absence> absenceCourse2List = new List<Absence>();


      for (int i = 0; i < 16; i++) { // 10 absences for Carlos
        if (!(i == 2 || i == 5 || i == 6 || i == 9 || i == 12 || i == 13)) {
          absenceCourse1List.Add(new Absence { Course = courseTest1, Date = startDateTest.AddDays(i), Student = studentTest1 });
        }
      }

      for (int i = 0; i < 16; i++) { // 20 absences for Hector
        if (!(i == 2 || i == 5 || i == 6 || i == 9 || i == 12 || i == 13)) {
          absenceCourse1List.Add(new Absence { Course = courseTest1, Date = startDateTest.AddDays(i), Student = studentTest2 });
          absenceCourse2List.Add(new Absence { Course = courseTest2, Date = startDateTest.AddDays(i), Student = studentTest2 });
        }
      }

      //2 absences for Juan
      absenceCourse1List.Add(new Absence { Course = courseTest1, Date = startDateTest.AddDays(1), Student = studentTest4 });
      absenceCourse2List.Add(new Absence { Course = courseTest2, Date = startDateTest.AddDays(3), Student = studentTest4 });

      //Act
      List<CalculateAbsence> calcAbsList1 = NotoriousStudents(absenceCourse1List, TimeSpan.Week);
      List<CalculateAbsence> calcAbsList2 = NotoriousStudents(absenceCourse1List, TimeSpan.TwoWeeks);
      List<CalculateAbsence> calcAbsList3 = NotoriousStudents(absenceCourse1List, TimeSpan.ThreeWeeks);
      List<CalculateAbsence> calcAbsList4 = NotoriousStudents(absenceCourse1List, TimeSpan.Month);

      List<CalculateAbsence> calcAbsList11 = NotoriousStudents(absenceCourse2List, TimeSpan.Week);
      List<CalculateAbsence> calcAbsList22 = NotoriousStudents(absenceCourse2List, TimeSpan.TwoWeeks);
      List<CalculateAbsence> calcAbsList33 = NotoriousStudents(absenceCourse2List, TimeSpan.ThreeWeeks);
      List<CalculateAbsence> calcAbsList44 = NotoriousStudents(absenceCourse2List, TimeSpan.Month);

      //Assert
      Assert.Empty(calcAbsList1);
      Assert.Empty(calcAbsList2);

      Assert.Equal(2, calcAbsList3.Count);
      Assert.Equal("Carlos", calcAbsList3[0].StudentName);
      Assert.Equal(4, calcAbsList3[0].DaysOfAbsence.Count);
      Assert.Equal("Hector", calcAbsList3[1].StudentName);
      Assert.Equal(4, calcAbsList3[1].DaysOfAbsence.Count);

      Assert.Equal(3, calcAbsList4.Count);
      Assert.Equal("Carlos", calcAbsList4[0].StudentName);
      Assert.Equal(10, calcAbsList4[0].DaysOfAbsence.Count);
      Assert.Equal("Hector", calcAbsList4[1].StudentName);
      Assert.Equal(10, calcAbsList4[1].DaysOfAbsence.Count);
      Assert.Equal("Juan", calcAbsList4[2].StudentName);
      Assert.Single(calcAbsList4[2].DaysOfAbsence);

      Assert.Empty(calcAbsList11);
      Assert.Empty(calcAbsList22);

      Assert.Single(calcAbsList33);
      Assert.Equal("Hector", calcAbsList33[0].StudentName);
      Assert.Equal(4, calcAbsList33[0].DaysOfAbsence.Count);

      Assert.Equal(2, calcAbsList44.Count);
      Assert.Equal("Hector", calcAbsList44[0].StudentName);
      Assert.Equal(10, calcAbsList44[0].DaysOfAbsence.Count);
      Assert.Equal("Juan", calcAbsList44[1].StudentName);
      Assert.Single(calcAbsList44[1].DaysOfAbsence);
    }

    public List<CalculateAbsence> NotoriousStudents(List<Absence> absenceByCourseList, _20201110_ALS2.Models.TimeSpan timeSpan) {
      DateTime todaysDate = new DateTime(2020, 12,30);
      DateTime checkDate = FindTimeSpan(timeSpan, todaysDate);

      List<Absence> absenceInTimeSpanList = new List<Absence>();
      //Find fravær de seneste 14 dage
      foreach (Absence absence in absenceByCourseList) {
        if (checkDate <= absence.Date && absence.Date <= todaysDate) {
          absenceInTimeSpanList.Add(absence);
        }
      }

      //Find studerende
      List<Student> studentList = new List<Student>();
      foreach (Absence absence in absenceInTimeSpanList) {
        if (!studentList.Contains(absence.Student)) {
          studentList.Add(absence.Student);
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

        //Tilføj fraværsprocent i perioden? eller på fag? eller uddannelse?
        notoriousStudentList.Add(entry);
      }

      return notoriousStudentList;
    }

    private List<CalculateAbsence> AbsenceForStudentsInCourse(List<Absence> absenceByCourseList) {
      List<Student> studentsList = new List<Student>();
      List<Course> courseList = new List<Course> { absenceByCourseList[0].Course };

      if (absenceByCourseList.TrueForAll(a => a.Course.CourseId == courseList[0].CourseId)) {
        foreach (Absence absence in absenceByCourseList) {
          if (!studentsList.Contains(absence.Student)) {
            studentsList.Add(absence.Student);
          }
        }

        List<CalculateAbsence> studentsAbsenceList = new List<CalculateAbsence>();
        foreach (Student student in studentsList) {
          studentsAbsenceList.Add(AbsenceBuilder(courseList, absenceByCourseList, student));
        }

        return studentsAbsenceList;
      }

      return null;
    }

    private List<CalculateAbsence> AbsenceByEducationAndSemester(List<Absence> absenceList, Education education, int semesterNo) {
      List<Student> studentList = new List<Student>();
      List<Course> courseByEducationList = new List<Course>();

      List<Absence> absenceByEducationList =
        absenceList.FindAll(a => (a.Course.Education.EducationId == education.EducationId) && (a.Student.Semester == semesterNo));

      foreach (Absence absence in absenceByEducationList) {
        if (absence.Student.Semester == semesterNo) {
          if (!studentList.Contains(absence.Student)) {
            studentList.Add(absence.Student);
          }
          if (!courseByEducationList.Contains(absence.Course)) {
            courseByEducationList.Add(absence.Course);
          }
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


    //Service methods to the methods above that are being tested
    private CalculateAbsence AbsenceBuilder(List<Course> courseList, List<Absence> absenceByCourseOrEducationList,
      Student student) {
      if (courseList != null)
      {
        List<Absence> absenceForStudentList =
          absenceByCourseOrEducationList.FindAll(a => a.Student.StudentId == student.StudentId);

        double daysOfAbsence = absenceForStudentList.Count;
        double totalCourseDays = FindSchoolDays(courseList);

        double result = (daysOfAbsence / totalCourseDays) * 100;

        CalculateAbsence studentAbsence = new CalculateAbsence { StudentName = student.Name, AbsenceInPercent = result };

        return studentAbsence;
      }

      return new CalculateAbsence();
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


