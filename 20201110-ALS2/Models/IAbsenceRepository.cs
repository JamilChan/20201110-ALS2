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
    Dictionary<Education, bool> EducationHasAbsence(List<Education> educationList, DateTime modelDate);
    IQueryable<Absence> AbsencesForDateCourse(Course course, DateTime date);
    IQueryable<Absence> AbsencesForDateEducation(Education education, DateTime date);
    Absence AbsenceForDateCourseStudent(Course course, DateTime date, Student student);
    Absence AbsenceForDateEducationStudent(Education education, DateTime date, Student student);

    List<Absence> AbsenceByCourse(Course course);
    List<Absence> AbsenceBy(Education education, int semesterNo);
    List<Absence> AbsenceByEducation(Education education);
  }
}
