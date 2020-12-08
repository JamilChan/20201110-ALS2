using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _20201110_ALS2.Models {
  public class EfEducationRepository : IEducationRepository {
    private readonly AlsDbContext context;

    public EfEducationRepository(AlsDbContext context) {
      this.context = context;
    }

    public IQueryable<Education> Educations => context.Educations;

    public void Create(Education education) {
      context.Educations.Add(education);
      context.SaveChanges();
    }

    public void Update(Education education) {
      context.Educations.Update(education);
      context.SaveChanges();
    }

    public void Delete(int educationId) {
      Education education = Educations.FirstOrDefault(e => e.EducationId == educationId);
      context.Educations.Remove(education);
      context.SaveChanges();
    }

    public List<Education> EducationsByEducator(Educator educator) {
      List<Education> educations = new List<Education>();

      List<StudentCourse> studentCourses = context.StudentCourses.Include(sc => sc.Course.Week).Where(sc => sc.Course.Educator.EducatorId == educator.EducatorId).ToList();
      foreach (StudentCourse studentCourse in studentCourses) {
        IQueryable students = context.Students.Where(s => s.StudentId == studentCourse.StudentId);
        foreach (Student student in students) {
          educations.Add(context.Educations.FirstOrDefault(e => e.EducationId == student.EducationId));
        }
      }

      educations = educations.Distinct().ToList();

      return educations;
    }
  }
}
