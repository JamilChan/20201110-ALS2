using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models.ViewModels {
  public class CreateCourseViewModel {

    public Course Crs { get; set; } = new Course();
    public IQueryable<Educator> EducatorList { get; set; }
    public List<string> EducatorNameList { get; set; } = new List<string>();
    [Required]
    public string SelectedEducator { get; set; }
    public IQueryable<Student> StudentList { get; set; } = Enumerable.Empty<Student>().AsQueryable();
    public List<Student> CheckedStudents { get; set; }
    public bool Edit { get; set; }

    public void GetEducatorsName() {
      foreach (Educator e in EducatorList) {
        EducatorNameList.Add(e.Name);
      }
    }
  }
}
