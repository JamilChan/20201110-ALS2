using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _20201110_ALS2.Models.DAL.Course {
  public class EfCourseRepository : ICourseRepository {
    private AlsDbContext context;

    public EfCourseRepository(AlsDbContext context) {
      this.context = context;
    }

    public IQueryable<Models.Course> Courses => context.Courses.Include(c => c.Educator).Include(c => c.Week);

    public void SaveCourse(Models.Course course) {
      if (course.CourseId == 0) {
        context.Courses.Add(course);
      } else {
        Models.Course dbEntry = context.Courses.Include(c => c.StudentCourses).FirstOrDefault(c => c.CourseId == course.CourseId);
        if (dbEntry != null) {
          dbEntry.Name = course.Name;
          dbEntry.Educator = course.Educator;
          dbEntry.Week = course.Week;
          dbEntry.StartDate = course.StartDate;
          dbEntry.EndDate = course.EndDate;
          dbEntry.StudentCourses = course.StudentCourses;
        }
      }
      context.SaveChanges();
    }

    public Models.Course Delete(long courseId) {
      Models.Course dbEntry = context.Courses.FirstOrDefault(c => c.CourseId == courseId);
      if (dbEntry != null) {
        context.Courses.Remove(dbEntry);
        context.SaveChanges();
      }

      return dbEntry;
    }

    public List<Models.Student> SelectedStudents(long courseId) {
      IQueryable<StudentCourse> ss = context.StudentCourses.Include(sc => sc.Course).Include(sc => sc.Student).Where(sc => sc.CourseId == courseId);
      List<Models.Student> sStudents = new List<Models.Student>();
      foreach (StudentCourse sc in ss) {
        sStudents.Add(sc.Student);
      }

      return sStudents;
    }
  }
}
