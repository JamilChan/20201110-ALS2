using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class Educator {
    public int EducatorId { get; set; }
    public string Name { get; set; }

    public override string ToString() {
      return Name;
    }
  }
}
