using System.Linq;

namespace _20201110_ALS2.Models.DAL.Educator {
  public class EfEducatorRepository : IEducatorRepository {

    private readonly AlsDbContext context;

    public EfEducatorRepository(AlsDbContext context) {
      this.context = context;
    }

    public Models.Educator Get(long educatorId) {
      return context.Educators.Find(educatorId);
    }

    public IQueryable<Models.Educator> Educators => context.Educators;

    public void SaveEducator(Models.Educator educator) {
      if (educator.EducatorId == 0) {
        context.Educators.Add(educator);
      } else {
        Models.Educator dbEntryEducator =
          context.Educators.FirstOrDefault(e => e.EducatorId == educator.EducatorId);
        if (dbEntryEducator != null) {
          dbEntryEducator.Name = educator.Name;
        }
      }
      context.SaveChanges();
    }

    public Models.Educator Delete(long educatorId) {
      Models.Educator educator = context.Educators.Find(educatorId);
      if (educator != null) {
        context.Educators.Remove((educator));
        context.SaveChanges();
      }

      return educator;
    }
  }
}
