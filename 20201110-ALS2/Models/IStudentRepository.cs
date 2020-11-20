using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public interface IStudentRepository {

    IQueryable<Student> Students { get; }

    List<Student> GetAllStudentsFromCourses(Course course);
  }
}
