using System.Linq;

namespace _20201110_ALS2.Models.DAL.Week {
  public interface IWeekRepository {

    IQueryable<Models.Week> Weeks { get; }
  }
}
