using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class EfCourseRepository : ICourseRepository {
    private AlsDbContext context;

    public EfCourseRepository(AlsDbContext context) {
      this.context = context;
    }

    public IQueryable<Course> Courses => context.Courses;
  }
}
