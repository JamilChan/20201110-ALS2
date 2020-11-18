using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public interface IAbsenceRepository {

    IQueryable<Absence> Absences { get; }

    void CreateAbsence(List<Absence> a);
  }
}
