using Microsoft.EntityFrameworkCore;
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

    public IQueryable<StudentCourse> StudentCourses => context.StudentCourses.Include(sc => sc.Course).Include(sc => sc.Student);

    public void CreateStudentCourse(List<StudentCourse> sCourse) {
      foreach (StudentCourse sc in sCourse) {
        context.StudentCourses.Add(sc);
      }
      context.SaveChanges();
    }
    public void UpdateStudentCourse(List<StudentCourse> sCourse) {
      IQueryable<StudentCourse> ss = context.StudentCourses.Include(sc => sc.Course).Include(sc => sc.Student).Where(sc => sc.CourseId == sCourse[0].Course.CourseId);
      foreach (StudentCourse sc in ss) {
          context.StudentCourses.Remove(sc);
      }

      foreach (StudentCourse sc in sCourse) {
        context.StudentCourses.Add(sc);
      } 
      context.SaveChanges();
    }
  }
}
