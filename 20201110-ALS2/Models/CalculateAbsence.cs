using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class CalculateAbsence {
    public string StudentName { get; set; }
    public double AbsenceInPercent { get; set; }
    public List<Student> StudentList { get; set; } = new List<Student>();

    public List<CalculateAbsence> AbsenceForStudentsInCourse(List<Absence> absenceByCourseList) {
      List<CalculateAbsence> studentsAbsenceList = new List<CalculateAbsence>();

      foreach (Absence absence in absenceByCourseList) {
        if (!StudentList.Contains(absence.Student)) {
          StudentList.Add(absence.Student);
          studentsAbsenceList.Add(AbsenceBuilder(absenceByCourseList, absence.Student));
        }
      }

      return studentsAbsenceList;
    }

    private CalculateAbsence AbsenceBuilder(List<Absence> absenceList, Student student) {
      List<Absence> absenceForStudentList = absenceList.FindAll(a => a.Student.StudentId == student.StudentId);

      double daysOfAbsence = absenceForStudentList.FindAll(a => a.Status == "absent").Count;
      double totalCourseDays = absenceForStudentList.Count;
      double result = (daysOfAbsence / totalCourseDays) * 100;

      CalculateAbsence studentAbsence = new CalculateAbsence { StudentName = student.Name, AbsenceInPercent = result };

      return studentAbsence;
    }

    public List<int> IndicationForStudents(List<Student> students, List<Absence> absenceList) {
      List<int> indications = new List<int>();

      foreach (Student student in students) {
        List<Absence> absences = absenceList.FindAll(a => a.Student == student);
        double absenceDays = 0;
        foreach (Absence absence in absences) {
          if (absence.Status == "absent") {
            absenceDays++;
          }
        }

        indications.Add(absences.Count > 0 ? Convert.ToInt32(absenceDays / absences.Count * 100) : 0);
      }

      return indications;
    }
  }
}
