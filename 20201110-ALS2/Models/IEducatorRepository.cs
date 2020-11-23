using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public interface IEducatorRepository
  {
    Educator Get(long educatorId);
    IQueryable<Educator> GetAll();
    void Add(Educator educator);
    Educator Update(Educator educatorChanges);
    Educator Delete(long educatorId);
  }
}
