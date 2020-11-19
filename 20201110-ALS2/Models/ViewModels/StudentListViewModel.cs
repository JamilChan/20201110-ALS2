using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models.ViewModels {
  public class StudentListViewModel {

    public string[] StatusList { get; set; }
    public string IsChecked { get; set; }
    public List<Student> StudentsList { get; set; }
    public Course Course { get; set; }
  }
}