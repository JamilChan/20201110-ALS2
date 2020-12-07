using System;
using System.Collections.Generic;
using System.Linq;

namespace _20201110_ALS2.Models.DAL.Absence {
  public interface IAbsenceRepository {

    IQueryable<Models.Absence> Absences { get; }

    void CreateAbsence(List<Models.Absence> absenceList);
    void UpdateAbsence(List<Models.Absence> absenceList, List<string> statusList);

    Dictionary<Models.Course, bool> CourseHasAbsence(List<Models.Course> courseList, DateTime date);
    IQueryable<Models.Absence> AbsencesForDateCourse(Models.Course course, DateTime date);

    List<Models.Absence> AbsenceByCourse(Models.Course course);
  }
}
