using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class EfEducatorRepository : IEducatorRepository {

    private readonly AlsDbContext context;

    public EfEducatorRepository(AlsDbContext context) {
      this.context = context;
    }

    public Educator Get(long educatorId) {
      return context.Educators.Find(educatorId);
    }

    public IQueryable<Educator> Educators => context.Educators;

    public void SaveEducator(Educator educator) {
      if (educator.EducatorId == 0) {
        context.Educators.Add(educator);
      } else {
        Educator dbEntryEducator =
          context.Educators.FirstOrDefault(e => e.EducatorId == educator.EducatorId);
        if (dbEntryEducator != null) {
          dbEntryEducator.Name = educator.Name;
        }
      }
      context.SaveChanges();
    }

    public Educator Delete(long educatorId) {
      Educator educator = context.Educators.Find(educatorId);
      if (educator != null) {
        context.Educators.Remove((educator));
        context.SaveChanges();
      }

      return educator;
    }
  }
}
