using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models.ViewModels {
  public class StudentCRUDViewModel {
    public Student Student { get; set; } = new Student();
    public List<string> EducationNameList { get; set; } = new List<string>();

    public void GenerateModel(List<Education> educationList) {
      Student.Education = new Education();

      foreach (Education education in educationList) {
        EducationNameList.Add(education.Name);
      }
    }
  }
}
