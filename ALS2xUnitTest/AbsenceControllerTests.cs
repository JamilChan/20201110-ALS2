//using System;
//using System.Collections.Generic;
//using System.Linq;
//using _20201110_ALS2.Models;
//using _20201110_ALS2.Controllers;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using Xunit;

//namespace ALS2xUnitTest {
//  public class AbsenceControllerTests {

//    [Fact]
//    public void StoreAbsenceInRepository() {
//      //Arrange
//      Educator[] educators = {
//        new Educator {
//          EducatorId = 1, 
//          Name = "Jan"
//        },
//        new Educator {
//          EducatorId = 2, 
//          Name = "John"
//        }
//      };

//      Student[] students = {
//        new Student {
//          StudentId = 1, 
//          Name = "Mathias", 
//          Education = "Computer Science", 
//          Semester = 3
//        },
//        new Student {
//          StudentId = 2, 
//          Name = "Simon", 
//          Education = "Computer Science", 
//          Semester = 3
//        },
//        new Student {
//          StudentId = 3, 
//          Name = "Dean", 
//          Education = "Computer Science", 
//          Semester = 3
//        },
//        new Student {
//          StudentId = 4, 
//          Name = "Emil", 
//          Education = "Computer Science", 
//          Semester = 3
//        }
//      };

//      Course[] courses = {
//        new Course {
//          CourseId = 1,
//          Name = "ProTek",
//          Educator = educators[0],
//          StartDate = new DateTime(2008, 5, 1, 8, 30, 52),
//          EndDate = new DateTime(2009, 5, 1, 8, 30, 52)
//        },
//        new Course {
//          CourseId = 2,
//          Name = "Sys",
//          Educator = educators[1],
//          StartDate = new DateTime(2008, 5, 1, 8, 30, 52),
//          EndDate = new DateTime(2009, 5, 1, 8, 30, 52)
//        },
//      };

//      Mock<IAbsenceRepository> mock = new Mock<IAbsenceRepository>();
//      mock.Setup(m => m.Absences).Returns((new Absence[] {
//        new Absence {
//          AbsenceId = 1, 
//          Student = students[0], 
//          Course = courses[0], 
//          Date = new DateTime(2009, 5, 1, 8, 30, 52)
//        },
//        new Absence {
//          AbsenceId = 2, 
//          Student = students[1], 
//          Course = courses[0], 
//          Date = new DateTime(2009, 5, 1, 8, 30, 52)
//        },
//        new Absence {
//          AbsenceId = 3, 
//          Student = students[2], 
//          Course = courses[1], 
//          Date = new DateTime(2009, 5, 1, 8, 30, 52)
//        },
//        new Absence {
//          AbsenceId = 4, 
//          Student = students[3], 
//          Course = courses[1], 
//          Date = new DateTime(2009, 5, 1, 8, 30, 52)
//        },
//        new Absence {
//          AbsenceId = 5, 
//          Student = students[1], 
//          Course = courses[0], 
//          Date = new DateTime(2019, 5, 1, 8, 30, 52)
//        }
//      }).AsQueryable<Absence>());

//      EducatorController controller = new EducatorController(mock.Object);

//      //Act
//      IEnumerable<Absence> result = (controller.AbsenceList() as ViewResult)?.ViewData.Model as IEnumerable<Absence>;

//      //Assert
//      Absence[] absences = result.ToArray();
//      Assert.True(absences.Length == 5);
//      Assert.Equal("Mathias", absences[0].Student.Name);
//      Assert.Equal("Jan", absences[1].Course.Educator.Name);
//    }
//  }
//}
