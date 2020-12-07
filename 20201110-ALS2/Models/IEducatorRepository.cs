using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public interface IEducatorRepository {

    IQueryable<Educator> Educators { get; }
    void AddEducator(Educator educator);
    void EditEducator(Educator educator);
    Educator Delete(long educatorId);
  }
}
