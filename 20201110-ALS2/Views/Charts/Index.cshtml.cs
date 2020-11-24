using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20201110_ALS2.Models;
using Google.DataTable.Net.Wrapper;
using Google.DataTable.Net.Wrapper.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _20201110_ALS2.Views.Charts {
  public class IndexModel : PageModel {

    public ActionResult OnGetChartData() {
      var pizza = new[]
      {
        new {Name = "Mushrooms", Count = 3},
        new {Name = "Onions", Count = 1},
        new {Name = "Olives", Count = 1},
        new {Name = "Zucchini", Count = 1},
        new {Name = "Pepperoni", Count = 2}
      };

      var json = pizza.ToGoogleDataTable()
        .NewColumn(new Column(ColumnType.String, "Topping"), x => x.Name)
        .NewColumn(new Column(ColumnType.Number, "Slices"), x => x.Count)
        .Build()
        .GetJson();

      return Content(json);
    }
  }
}
