using System.Linq;

namespace _20201110_ALS2.Models.DAL.Education {
  public class EfEducationRepository : IEducationRepository {
    private readonly AlsDbContext context;

    public EfEducationRepository(AlsDbContext context) {
      this.context = context;
    }

    public IQueryable<Models.Education> Educations => context.Educations;
  }
}
