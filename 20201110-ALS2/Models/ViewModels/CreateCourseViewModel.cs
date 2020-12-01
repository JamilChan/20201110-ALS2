﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models.ViewModels {
  public class CreateCourseViewModel {

    public Course Course { get; set; } = new Course();
    public IQueryable<Educator> Educators { get; set; }
    public List<string> EducatorNameList { get; set; } = new List<string>();
    public IQueryable<Education> Educations { get; set; }
    public List<string> EducationNameList { get; set; } = new List<string>();
    public IQueryable<Student> Students { get; set; } = Enumerable.Empty<Student>().AsQueryable();
    public List<Student> CheckedStudentList { get; set; }
    public bool Edit { get; set; }

    public void GetCourseInfoName() {
      foreach (Educator educator in Educators) {
        EducatorNameList.Add(educator.Name);
      }
      foreach (Education education in Educations) {
        EducationNameList.Add(education.Name);
      }
    }
  }
}
