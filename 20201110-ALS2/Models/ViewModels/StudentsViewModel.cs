using System.Collections.Generic;

namespace _20201110_ALS2.Models.ViewModels {
  public class StudentsViewModel {
    public List<CalculateAbsence> Absences { get; set; }
    public Course Course { get; set; }
    public Education Education { get; set; }
    public int SemesterNo { get; set; }
  }
}
