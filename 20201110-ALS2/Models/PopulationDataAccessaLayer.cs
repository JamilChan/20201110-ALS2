using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class PopulationDataAccessaLayer {
    public static List<PopulationModel> GetUsStatePopulationList() {
      var list = new List<PopulationModel>();
      list.Add(new PopulationModel { CityName = "Chennai", PopulationYear2020 = 28000, PopulationYear2010 = 15000, PopulationYear2000 = 22000, PopulationYear1990 = 50000 });
      list.Add(new PopulationModel { CityName = "Pune", PopulationYear2020 = 30000, PopulationYear2010 = 19000, PopulationYear2000 = 24000, PopulationYear1990 = 39000 });
      list.Add(new PopulationModel { CityName = "Kochi", PopulationYear2020 = 35000, PopulationYear2010 = 16000, PopulationYear2000 = 26000, PopulationYear1990 = 41000 });
      list.Add(new PopulationModel { CityName = "Kolkata", PopulationYear2020 = 37000, PopulationYear2010 = 14000, PopulationYear2000 = 28000, PopulationYear1990 = 48000 });
      list.Add(new PopulationModel { CityName = "Odisha", PopulationYear2020 = 40000, PopulationYear2010 = 18000, PopulationYear2000 = 30000, PopulationYear1990 = 54000 });

      return list;

    }
  }
}
