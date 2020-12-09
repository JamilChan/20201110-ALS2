using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public interface IEducationRepository {

    IQueryable<Education> Educations { get; }

    void Create(Education education);
    void Update(Education education);
    void Delete(int educationId);
    List<Education> EducationsByEducator(Educator educator);
  }
}
