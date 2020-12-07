using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _20201110_ALS2.Models.DAL.Absence {
  public class EfAbsenceRepository : IAbsenceRepository {
    private AlsDbContext context;

    public EfAbsenceRepository(AlsDbContext context) {
      this.context = context;
    }

    public IQueryable<Models.Absence> Absences => context.Absences.Include(a => a.Student).Include(a => a.Course);

    public void CreateAbsence(List<Models.Absence> absenceList) {
      foreach (Models.Absence a in absenceList) {
        context.Absences.Add(a);
      }
      context.SaveChanges();
    }

    public void UpdateAbsence(List<Models.Absence> absenceList, List<string> statusList) {

      for (int i = 0; i < absenceList.Count; i++) {
        absenceList[i].Status = statusList[i].Split(":", 2)[1];

        context.Absences.Update(absenceList[i]);
      }
      context.SaveChanges();
    }

    public Dictionary<Models.Course, bool> CourseHasAbsence(List<Models.Course> courseList, DateTime date) {
      Dictionary<Models.Course, bool> courseHasAbsence = new Dictionary<Models.Course, bool>();

      foreach (Models.Course course in courseList) {
        Models.Absence a = context.Absences.Where(a => a.Date.Date == date.Date).FirstOrDefault(a => a.Course.CourseId == course.CourseId);

        if (a != null) {
          courseHasAbsence.Add(course, true);
        } else {
          courseHasAbsence.Add(course, false);
        }
      }

      return courseHasAbsence;
    }

    public IQueryable<Models.Absence> AbsencesForDateCourse(Models.Course course, DateTime date) {
      IQueryable<Models.Absence> absences = context.Absences.Include(a => a.Student).Include(a => a.Course).Where(a => a.Date.Date == date.Date && a.Course == course);

      return absences;
    }

    public List<Models.Absence> AbsenceByCourse(Models.Course course) {
      List<Models.Absence> absenceByCourseList = context.Absences.Include(a => a.Course).ToList();

      return absenceByCourseList;
    }
  }
}
