﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _20201110_ALS2.Models.ViewModels {
  public class RegisterEducatorViewModel {
    public Educator Educator { get; set; }

    public LoginModel LoginModel { get; set; }

    public string RoleName { get; set; }

    public IEnumerable<IdentityRole> AllRoles { get; set; }

  }
}
