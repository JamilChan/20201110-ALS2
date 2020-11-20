using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class EfStudentRepository : IStudentRepository {
    private AlsDbContext context;

    public EfStudentRepository(AlsDbContext context) {
      this.context = context;
    }

    public IQueryable<Student> Students => context.Students;
    public List<Student> GetAllStudentsFromCourses(Course course) {
      IQueryable<StudentCourse> studentCourse = context.StudentCourses.Include(sc => sc.Course).Where(sc => sc.CourseId == course.CourseId).Include(sc => sc.Student);
      List<Student> students = new List<Student>();

      foreach (StudentCourse sc in studentCourse) {
        students.Add(sc.Student);
      }

      return students;
    }
  }
}
