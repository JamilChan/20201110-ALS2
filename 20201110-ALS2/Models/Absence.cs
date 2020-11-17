using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class Absence {
    public int AbsenceId { get; set; }
    public Student Student { get; set; }
    public Course Course { get; set; }
    public DateTime Date { get; set; }
  }
}
