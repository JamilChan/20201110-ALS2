using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class EfWeekRepository : IWeekRepository {
    private AlsDbContext context;

    public EfWeekRepository(AlsDbContext context) {
      this.context = context;
    }

    public IQueryable<Week> Weeks => context.Weeks; 
  }
}
