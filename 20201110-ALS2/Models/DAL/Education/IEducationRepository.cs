using System.Linq;

namespace _20201110_ALS2.Models.DAL.Education {
  public interface IEducationRepository {
    IQueryable<Models.Education> Educations { get; }
  }
}
