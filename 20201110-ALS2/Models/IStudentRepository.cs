﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public interface IStudentRepository {

    IQueryable<Student> Students { get; }
    void Create(Student student);
    void Delete(long studentId);
    void Update(Student student);
    
    List<Student> GetAllStudentsFromCourses(Course course);
    List<Student> GetAllStudentsFromEducation(Education education);
    List<Student> GetAllStudentsFromEducationSemester(Education education, int semester);
  }
}