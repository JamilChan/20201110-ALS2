using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class CalculateAbsence {
    public double CalcAllStudentsAbsenceByCourse(List<Absence> absenceList) {
      double absenceInPercent = 0.0;



      return absenceInPercent;
    }



    public double CalcStudentAbsence(List<Absence> absencesList, Student student, Course course) {
      


      return 0.0;
    }





    private double AllDays(Course course) {
      double allSchoolDays = 0;
      DateTime currentDate = course.StartDate;

      double startEndDiff = (course.EndDate - course.StartDate).TotalDays;

      for (double i = 0; i < startEndDiff; i++) {
        switch (currentDate.DayOfWeek.ToString()) {
          case "Monday":
            if (course.Week.Monday) {
              allSchoolDays++;
            }
            break;
          case "Tuesday":
            if (course.Week.Tuesday) {
              allSchoolDays++;
            }
            break;
          case "Wednesday":
            if (course.Week.Wednesday) {
              allSchoolDays++;
            }
            break;
          case "Thursday":
            if (course.Week.Thursday) {
              allSchoolDays++;
            }
            break;
          case "Friday":
            if (course.Week.Friday) {
              allSchoolDays++;
            }
            break;
          default:
            break;
        }

        currentDate = currentDate.AddDays(1);
      }

      return allSchoolDays;
    }




  }
}
