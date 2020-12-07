using System.Collections.Generic;
using System.Linq;

namespace _20201110_ALS2.Models.DAL.Student {
  public interface IStudentRepository {

    IQueryable<Models.Student> Students { get; }
    void Create(Models.Student student);
    void Delete(long studentId);
    void Update(Models.Student student);
    
    List<Models.Student> GetAllStudentsFromCourses(Models.Course course);
  }
}