using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class EfStudentRepository : IStudentRepository {
    private AlsDbContext context;

    public EfStudentRepository(AlsDbContext context) {
      this.context = context;
    }

    public IQueryable<Student> Students => context.Students;

    public void Create(Student student) {
      context.Students.Add(student);
      context.SaveChanges();
    }

    public void Delete(long studentId) {
      Student student = context.Students.FirstOrDefault(s => s.StudentId == studentId);

      context.Students.Remove(student);
      context.SaveChanges();
    }
  }
}
