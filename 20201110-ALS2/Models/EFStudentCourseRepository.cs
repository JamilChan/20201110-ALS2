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

    public void DeleteStudentCourse(long courseId) {
      IQueryable<StudentCourse> ss = context.StudentCourses.Include(sc => sc.Course).Include(sc => sc.Student).Where(sc => sc.CourseId == courseId);
      foreach (StudentCourse sc in ss) {
        context.StudentCourses.Remove(sc);
      }
      context.SaveChanges();

    }

    public void UpdateStudentCourse(List<StudentCourse> sCourse) {
      IQueryable<Student> students = context.Students;
      IQueryable<StudentCourse> ss = context.StudentCourses.Include(sc => sc.Course).Include(sc => sc.Student).Where(sc => sc.CourseId == sCourse[0].Course.CourseId);
      foreach (Student s in students) {
        StudentCourse sInSCourse = new StudentCourse();
        StudentCourse sInSS = new StudentCourse();

        foreach (StudentCourse sc in sCourse) {
          if (s.StudentId == sc.Student.StudentId) {
            sInSCourse = sc;
            break;
          }
        }

        foreach (StudentCourse sc in ss) {
          if (s.StudentId == sc.Student.StudentId) {
            sInSS = sc;
            break;
          }
        }

        if (sInSCourse.Student != null && sInSS.Student == null) {
          context.StudentCourses.Add(sInSCourse);
        }
        else if (sInSCourse.Student == null && sInSS.Student != null) {
          context.StudentCourses.Remove(sInSS);
        }
      }

      //foreach (StudentCourse sc in sCourse) {
      //  //StudentCourse s = new StudentCourse() { CourseId = sc.Course.CourseId, StudentId = sc.Student.StudentId };
      //  context.StudentCourses.Add(sc);
      //} 
      context.SaveChanges();
    }
  }
}
