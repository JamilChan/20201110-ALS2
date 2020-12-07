using System.Collections.Generic;
using System.Linq;

namespace _20201110_ALS2.Models.DAL.Course {
  public interface ICourseRepository {

    IQueryable<Models.Course> Courses { get; }
    void SaveCourse(Models.Course course);
    Models.Course Delete(long courseId);
    List<Models.Student> SelectedStudents(long courseId);
  }
}
