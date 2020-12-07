using System.Linq;

namespace _20201110_ALS2.Models.DAL.Week {
  public class EfWeekRepository : IWeekRepository {
    private AlsDbContext context;

    public EfWeekRepository(AlsDbContext context) {
      this.context = context;
    }

    public IQueryable<Models.Week> Weeks => context.Weeks; 
  }
}
