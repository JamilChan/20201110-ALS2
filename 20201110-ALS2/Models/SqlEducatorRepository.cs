using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class SqlEducatorRepository : IEducatorRepository {

    private readonly AlsDbContext context;

    public SqlEducatorRepository(AlsDbContext context) {
      this.context = context;
    }
    public Educator Get(long educatorId) {
      return context.Educators.Find(educatorId);
    }

    public IQueryable<Educator> Educators() {
      return context.Educators;
    }

    public void Add(Educator educator) {
      context.Educators.Add(educator);
      context.SaveChanges();
    }
    public Educator Update(Educator educatorChanges) {
      if (educatorChanges.EducatorId == 0) {
        context.Educators.Add(educatorChanges); //kan den ikke bare kalde linje 18, Add();
      } else {
        Educator dbEntryEducator =
          context.Educators.FirstOrDefault(educator => educator.EducatorId == educatorChanges.EducatorId);
        if (dbEntryEducator != null) {
          dbEntryEducator.Name = educatorChanges.Name;
        }
      }

      context.SaveChanges();

      return educatorChanges;
    }
    public Educator Delete(int educatorId) {
      Educator educator = context.Educators.Find(educatorId);
      if (educator != null) {
        context.Educators.Remove((educator));
        context.SaveChanges();
      }
      return educator;
    }

  }
}
