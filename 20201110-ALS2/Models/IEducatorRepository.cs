using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public interface IEducatorRepository
  {
    Educator Get(int educatorId);
    void Add(Educator educator);
    Educator Update(Educator educatorChanges);
    Educator Delete()
  }
}
