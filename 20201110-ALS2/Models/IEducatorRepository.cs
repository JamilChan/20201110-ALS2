using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public interface IEducatorRepository
  {
    Educator Get(long educatorId);

    IQueryable<Educator> Educators { get; }

    void SaveEducator(Educator educator);

    Educator Delete(long educatorId);
  }
}
