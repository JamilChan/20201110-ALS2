using System.Collections.Generic;

namespace _20201110_ALS2.Models.ViewModels {
  public class StudentsDaysViewModel {
    public List<string> Dates { get; set; }
    public List<string> StudentList { get; set; }
    public Dictionary<string, Dictionary<string, string>> StudentStatuses { get; set; }
  }
}
