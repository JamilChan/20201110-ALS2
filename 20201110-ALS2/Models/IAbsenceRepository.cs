using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public interface IAbsenceRepository {

    IQueryable<Absence> Absences { get; }

    void CreateAbsence(List<Absence> absenceList);
    void UpdateAbsence(List<Absence> absenceList, List<string> statusList);

    Dictionary<Course, bool> CourseHasAbsence(List<Course> courseList, DateTime date);
    IQueryable<Absence> AbsencesForDateCourse(Course course, DateTime date);

    List<Absence> AbsenceByCourse(Course course);

  }
}
