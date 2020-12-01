using System.Collections.Generic;

namespace _20201110_ALS2.Models.ViewModels {
  public class StudentsViewModel {
    public List<CalculateAbsence> Absences { get; set; }
    public int CourseId { get; set; }
    public int EducationId { get; set; }
  }
}
