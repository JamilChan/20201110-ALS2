using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _20201110_ALS2.Models {
  public class EFStudentCourseRepository : IStudentCourseRepository {
    private readonly AlsDbContext context;

    public EFStudentCourseRepository(AlsDbContext context) {
      this.context = context;
    }

    public IQueryable<StudentCourse> StudentCourses => context.StudentCourses.Include(sc => sc.Course).Include(sc => sc.Student);

    public void CreateStudentCourse(StudentCourse sc) {
      context.StudentCourses.Add(sc);
      context.SaveChanges();
    }

    public void UpdateStudentCourse(Course course, Student student) {
      if (course.CourseId == 0 && student.StudentId == 0) {
        context.StudentCourses.Add(new StudentCourse { CourseId = course.CourseId, StudentId = student.StudentId });
      } else {
        StudentCourse dbEntryStudentCourse = context.StudentCourses.FirstOrDefault(sc => sc.StudentId == student.StudentId);
      }
    }
  }
}
