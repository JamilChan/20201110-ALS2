using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class EfAbsenceRepository : IAbsenceRepository {
    private AlsDbContext context;

    public EfAbsenceRepository(AlsDbContext context) {
      this.context = context;
    }

    public IQueryable<Absence> Absences => context.Absences;
  }
}
