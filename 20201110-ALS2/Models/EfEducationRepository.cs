using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class EfEducationRepository : IEducationRepository {
    private readonly AlsDbContext context;

    public EfEducationRepository(AlsDbContext context) {
      this.context = context;
    }

    public IQueryable<Education> Educations => context.Educations;
  }
}
