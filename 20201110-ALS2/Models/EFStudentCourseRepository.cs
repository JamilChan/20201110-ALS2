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
      context.StudentCourse.Add(sc);
      context.SaveChanges();
    }
    public void UpdateStudentCourse(Course Course, Student Student) {
      if (Course.CourseId == 0 && Student.StudentId == 0) {
        context.StudentCourse.Add(new StudentCourse { CourseId = Course.CourseId, StudentId = Student.StudentId });
      } else {
        StudentCourse dbEntryStudentCourse = context.StudentCourse.FirstOrDefault(sc => sc.StudentId == Student.StudentId);
      }

    }
  }
}
