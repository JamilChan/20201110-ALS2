using System.Linq;

namespace _20201110_ALS2.Models.DAL.Educator {
  public interface IEducatorRepository
  {
    Models.Educator Get(long educatorId);

    IQueryable<Models.Educator> Educators { get; }

    void SaveEducator(Models.Educator educator);

    Models.Educator Delete(long educatorId);
  }
}
