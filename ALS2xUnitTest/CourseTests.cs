using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using _20201110_ALS2.Models;

namespace ALS2xUnitTest {
  public class CourseTests {

    //[Fact]
    //public void CanReadNumberOfStudents() {
    //  // Arrange
    //  Educator e = new Educator { Name = "Flemming" };

    //  List<Student> sl = new List<Student> {
    //    new Student { Name = "Johanne", Education = "Datamatiker", Semester = 3 },
    //    new Student { Name = "John", Education = "Datamatiker", Semester = 3 },
    //    new Student { Name = "Bob", Education = "Finansøkonom", Semester = 3 },
    //    new Student { Name = "Tom", Education = "Datamatiker", Semester = 1 },
    //    new Student { Name = "Jakob", Education = "Datamatiker", Semester = 1 }
    //  };

    //  Course c = new Course { Name = "ProTek 3", Educator = e, StudentList = sl };

    //  // Act
    //  c.StudentList.Add(new Student { Name = "Hans", Education = "Datamatiker", Semester = 3 });
    //  c.StudentList.Add(new Student { Name = "Julie", Education = "Datamatiker", Semester = 1 });

    //  // Assert
    //  Assert.Equal(7, c.StudentList.Count);

    //}

    [Fact]
    public void IsCourseActive() {
      // Arrange
      // Act
      // Assert
    }

    [Fact]
    public void CanChangeName() {
      // Arrange
      Educator e = new Educator { Name = "Flemming" };
      Course c = new Course { Name = "ProTek 3", Educator = e };

      // Act
      c.Name = "ProTek 4";

      // Assert
      Assert.Equal("ProTek 4", c.Name);
    }

    [Fact]
    public void CanChangeEducator() {
      // Arrange
      Educator e = new Educator { Name = "Flemming" };
      Course c = new Course { Name = "ProTek 3", Educator = e };

      // Act
      c.Educator = new Educator { Name = "Hans" };

      // Assert
      Assert.Equal("Hans", c.Educator.Name);
    }
  }
}
