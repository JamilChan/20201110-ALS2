using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class EfEducationRepository : IEducationRepository {
    private readonly AlsDbContext context;

    public EfEducationRepository(AlsDbContext context) {
      this.context = context;
    }

    public IQueryable<Education> Educations => context.Educations;

    public void Create(Education education) {
      context.Educations.Add(education);
      context.SaveChanges();
    }

    public void Update(Education education) {
      context.Educations.Update(education);
      context.SaveChanges();
    }

    public void Delete(int educationId) {
      Education education = Educations.FirstOrDefault(e => e.EducationId == educationId);
      context.Educations.Remove(education);
      context.SaveChanges();
    }
  }
}
