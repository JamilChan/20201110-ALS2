﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20201110_ALS2.Models {
  public class EfStudentRepository : IStudentRepository {
    private AlsDbContext context;

    public EfStudentRepository(AlsDbContext context) {
      this.context = context;
    }

    public IQueryable<Student> Students => context.Students.Include(s => s.Education);

    public void Create(Student student) {
      context.Students.Add(student);
      context.SaveChanges();
    }

    public void Delete(long studentId) {
      Student student = context.Students.FirstOrDefault(s => s.StudentId == studentId);

      context.Students.Remove(student);
      context.SaveChanges();
    }

    public void Update(Student student) {
      context.Students.Update(student);
      context.SaveChanges();
    }

    public List<Student> GetAllStudentsFromCourses(Course course) {
      IQueryable<StudentCourse> studentCourses = context.StudentCourses.Include(sc => sc.Course).Where(sc => sc.CourseId == course.CourseId).Include(sc => sc.Student);
      List<Student> studentList = new List<Student>();
      
      foreach (StudentCourse sc in studentCourses) {
        studentList.Add(Students.FirstOrDefault(s => s.StudentId == sc.StudentId));
      }

      return studentList;
    }

    public List<Student> GetAllStudentsFromEducation(Education education) {
      return context.Students.Where(s => s.Education.EducationId == education.EducationId).ToList();
    }

    public List<Student> GetAllStudentsFromEducationSemester(Education education, int semester) {
      List<Student> students = context.Students.Where(s => s.Education.EducationId == education.EducationId && s.Semester == semester).ToList();

      return students;
    }
  }
}
