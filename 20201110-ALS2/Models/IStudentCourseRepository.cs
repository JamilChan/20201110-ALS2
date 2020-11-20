using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public interface IStudentCourseRepository {
    void CreateStudentCourse(StudentCourse sc);
    void UpdateStudentCourse(StudentCourse sCourse);
  }
}
