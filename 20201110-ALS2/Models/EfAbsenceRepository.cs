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

    public void CreateAbsence(List<Absence> absenceList) {
      foreach (Absence a in absenceList) {
        context.Absences.Add(a);
      }
      context.SaveChanges();
    }

    public void UpdateAbsence(List<Absence> absenceList, List<string> statusList) {

      for (int i = 0; i < absenceList.Count; i++) {
        absenceList[i].Status = statusList[i].Split(":", 2)[1];

        context.Absences.Update(absenceList[i]);
      }
      context.SaveChanges();
    }

    public Dictionary<Course, bool> CourseHasAbsence(List<Course> courseList, DateTime date) {
      Dictionary<Course, bool> courseHasAbsence = new Dictionary<Course, bool>();

      foreach (Course course in courseList) {
        Absence a = context.Absences.Where(a => a.Date.Date == date.Date).FirstOrDefault(a => a.Course.CourseId == course.CourseId);

        if (a != null) {
          courseHasAbsence.Add(course, true);
        } else {
          courseHasAbsence.Add(course, false);
        }
      }

      return courseHasAbsence;
    }

    public IQueryable<Absence> AbsencesForDateCourse(Course course, DateTime date) {
      IQueryable<Absence> absences = context.Absences.Include(a => a.Student).Include(a => a.Course).Where(a => a.Date.Date == date.Date && a.Course == course);

      return absences;
    }

    public List<Absence> AbsenceByCourse(Course course) {
      List<Absence> absenceByCourseList = context.Absences.Include(a => a.Course).ToList();

      return absenceByCourseList;
    }
  }
}
