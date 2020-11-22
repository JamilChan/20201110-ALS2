using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _20201110_ALS2.Models {
  public class EfAbsenceRepository : IAbsenceRepository {
    private AlsDbContext context;

    public EfAbsenceRepository(AlsDbContext context) {
      this.context = context;
    }

    public IQueryable<Absence> Absences => context.Absences.Include(a => a.Student).Include(a => a.Course);

    public void CreateAbsence(List<Absence> al) {
      foreach (Absence a in al) {
        context.Absences.Add(a);
      }
      context.SaveChanges();
    }

    public void UpdateAbsence(List<Absence> absence, List<string> status) {

      for (int i = 0; i < absence.Count; i++) {
        absence[i].Status = status[i].Split(":", 2)[1];

        context.Absences.Update(absence[i]);
      }
      context.SaveChanges();
    }

    public Dictionary<Course, bool> IsChecked(List<Course> courses, DateTime date) {
      Dictionary<Course, bool> d = new Dictionary<Course, bool>();

      foreach (Course course in courses) {
        Absence a = context.Absences.Where(a => a.Date.Date == date.Date).FirstOrDefault(a => a.Course.CourseId == course.CourseId);

        if (a != null) {
          d.Add(course, true);
        } else {
          d.Add(course, false);
        }
      }

      return d;
    }

    public IQueryable<Absence> AbsencesForDateCourse(Course course, DateTime date) {
      IQueryable<Absence> a = context.Absences.Include(a => a.Student).Include(a => a.Course).Where(a => a.Date.Date == date.Date && a.Course == course);

      return a;
    }
  }
}
