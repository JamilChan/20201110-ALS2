using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _20201110_ALS2.Models.DAL.Student {
  public class EfStudentRepository : IStudentRepository {
    private AlsDbContext context;

    public EfStudentRepository(AlsDbContext context) {
      this.context = context;
    }

    public IQueryable<Models.Student> Students => context.Students.Include(s => s.Education);

    public void Create(Models.Student student) {
      context.Students.Add(student);
      context.SaveChanges();
    }

    public void Delete(long studentId) {
      Models.Student student = context.Students.FirstOrDefault(s => s.StudentId == studentId);

      context.Students.Remove(student);
      context.SaveChanges();
    }

    public void Update(Models.Student student) {
      context.Students.Update(student);
      context.SaveChanges();
    }

    public List<Models.Student> GetAllStudentsFromCourses(Models.Course course) {
      IQueryable<StudentCourse> studentCourse = context.StudentCourses.Include(sc => sc.Course).Where(sc => sc.CourseId == course.CourseId).Include(sc => sc.Student);
      List<Models.Student> studentList = new List<Models.Student>();
      
      foreach (StudentCourse sc in studentCourse) {
        studentList.Add(sc.Student);
      }

      return studentList;
    }
  }
}
