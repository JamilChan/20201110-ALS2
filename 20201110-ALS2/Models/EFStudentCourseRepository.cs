using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class EFStudentCourseRepository : IStudentCourseRepository {
    private readonly AlsDbContext context;

    public EFStudentCourseRepository(AlsDbContext context) {
      this.context = context;
    }
    public void CreateStudentCourse(StudentCourse sc) {
      context.StudentCourses.Add(sc);
      context.SaveChanges();
    }
    public void UpdateStudentCourse(StudentCourse sCourse) {
      if (sCourse.Course.CourseId == 0 && sCourse.Student.StudentId == 0) {
        context.StudentCourses.Add(sCourse);
      } else {
        StudentCourse dbEntryStudentCourse = context.StudentCourses.FirstOrDefault(sc => sc.StudentId == sCourse.StudentId && sc.CourseId == sCourse.CourseId);
      }
      context.SaveChanges();
    }
  }
}
