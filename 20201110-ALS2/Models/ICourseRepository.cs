using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public interface ICourseRepository {

    IQueryable<Course> Courses { get; }
    void SaveCourse(Course course);
    Course Delete(long courseId);
    List<Student> SelectedStudents(long courseId);
    List<Course> CoursesByEducator(Educator educator);
    List<Course> CoursesByEducationAndDate(Education education, in DateTime date);
  }
}
