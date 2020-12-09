using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
      foreach (string s in statusList) {
        foreach (Absence a in absenceList) {
          if (s.Split(":", 2)[0] == a.Student.StudentId.ToString()) {
            a.Status = s.Split(":", 2)[1];

            context.Absences.Update(a);
          }
        }
      }

      context.SaveChanges();
    }

    public Dictionary<Course, bool> CourseHasAbsence(List<Course> courseList, DateTime date) {
      Dictionary<Course, bool> courseHasAbsence = new Dictionary<Course, bool>();

      foreach (Course course in courseList) {
        Absence absence = context.Absences.Where(a => a.Date.Date == date.Date).FirstOrDefault(a => a.Course.CourseId == course.CourseId);

        courseHasAbsence.Add(course, absence != null);
      }

      return courseHasAbsence;
    }

    public Dictionary<Education, bool> EducationHasAbsence(List<Education> educationList, DateTime date) {
      Dictionary<Education, bool> educationHasAbsence = new Dictionary<Education, bool>();

      foreach (Education education in educationList) {
        bool wasTrue = false;
        foreach (Student student in education.Students) {
          foreach (StudentCourse studentCourse in student.StudentCourses) {
            Absence absence = context.Absences.Where(a => a.Date.Date == date.Date).FirstOrDefault(a => a.Course.CourseId == studentCourse.Course.CourseId);

            if (absence != null) {
              educationHasAbsence.Add(education, true);
              wasTrue = true;
              break;
            }
          }

          if (wasTrue) {
            break;
          }
        }

        if (!wasTrue) {
          educationHasAbsence.Add(education, false);
        }
      }

      return educationHasAbsence;
    }

    public IQueryable<Absence> AbsencesForDateCourse(Course course, DateTime date) {
      IQueryable<Absence> absences = context.Absences.Include(a => a.Student).Include(a => a.Course).Where(a => a.Date.Date == date.Date && a.Course == course);

      return absences;
    }

    public IQueryable<Absence> AbsencesForDateEducation(Education education, DateTime date) {
      IQueryable<Absence> absences = context.Absences.Include(a => a.Student).Include(a => a.Course).Where(a => a.Date.Date == date.Date && a.Student.Education == education);

      return absences;
    }

    public Absence AbsenceForDateCourseStudent(Course course, DateTime date, Student student) {
      Absence absence = context.Absences.Include(a => a.Student).Include(a => a.Course).FirstOrDefault(a => a.Date.Date == date.Date && a.Course == course && a.Student == student);

      return absence;
    }

    public Absence AbsenceForDateEducationStudent(Education education, DateTime date, Student student) {
      Absence absence = context.Absences.Include(a => a.Student).Include(a => a.Course)
        .FirstOrDefault(a => a.Date.Date == date.Date && a.Student.Education == education && a.Student == student);

      return absence;
    }

    public List<Absence> AbsenceByCourse(Course course) {
      List<Absence> absenceByCourseList = context.Absences.Include(a => a.Course).Include(a => a.Student).Where(a => a.CourseId == course.CourseId).ToList();

      return absenceByCourseList;
    }

    public List<Absence> AbsenceBy(Education education, int semesterNo) {
      List<Absence> absencyByEducationAndSemester = context.Absences.Include(a => a.Student).Include(a => a.Course).Include(a => a.Course.Education).Where(a =>
        (a.Student.Education == education) && (a.Student.Semester == semesterNo)).ToList();

      return absencyByEducationAndSemester;
    }

    public List<Absence> AbsenceByEducation(Education education) {
      return context.Absences.Include(a => a.Student).Include(a => a.Course).Include(a => a.Course.Education).Where(a =>
       (a.Student.Education == education)).ToList();
    }
  }
}
