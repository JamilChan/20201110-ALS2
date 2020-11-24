using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public interface IAbsenceRepository {

    IQueryable<Absence> Absences { get; }

    void CreateAbsence(List<Absence> a);
    void UpdateAbsence(List<Absence> absence, List<string> status);

    Dictionary<Course, bool> IsChecked(List<Course> courses, DateTime date);
    IQueryable<Absence> AbsencesForDateCourse(Course course, DateTime date);

  }
}
