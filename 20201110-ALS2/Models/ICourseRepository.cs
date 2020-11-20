using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public interface ICourseRepository {

    IQueryable<Course> Courses { get; }
    void SaveCourse(Course course);
    Course Delete(int CourseId);
  }
}
