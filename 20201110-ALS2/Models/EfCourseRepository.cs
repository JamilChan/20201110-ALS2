﻿using Microsoft.EntityFrameworkCore;
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

    public IQueryable<Course> Courses => context.Courses.Include(c => c.Educator).Include(c => c.Week).Include(c => c.Education);

    public void SaveCourse(Course course) {
      if (course.CourseId == 0) {
        context.Courses.Add(course);
      } else {
        Course dbEntry = context.Courses.Include(c => c.StudentCourses).FirstOrDefault(c => c.CourseId == course.CourseId);
        if (dbEntry != null) {
          dbEntry.Name = course.Name;
          dbEntry.Educator = course.Educator;
          dbEntry.Week = course.Week;
          dbEntry.StartDate = course.StartDate;
          dbEntry.EndDate = course.EndDate;
          dbEntry.StudentCourses = course.StudentCourses;
        }
      }
      context.SaveChanges();
    }

    public Course Delete(long courseId) {
      Course dbEntry = context.Courses.FirstOrDefault(c => c.CourseId == courseId);
      if (dbEntry != null) {
        context.Courses.Remove(dbEntry);
        context.SaveChanges();
      }

      return dbEntry;
    }

    public List<Student> SelectedStudents(long courseId) {
      IQueryable<StudentCourse> studentCourses = context.StudentCourses.Include(sc => sc.Course).Include(sc => sc.Student).Where(sc => sc.CourseId == courseId);
      List<Student> students = new List<Student>();
      foreach (StudentCourse studentCourse in studentCourses) {
        students.Add(studentCourse.Student);
      }

      return students;
    }

    public List<Course> CoursesByEducator(Educator educator) {
      return context.Courses.Include(c => c.Week).Where(c => c.Educator.EducatorId == educator.EducatorId).ToList();
    }

    public List<Course> CoursesByEducationAndDate(Education education, in DateTime date) {
      IQueryable courses = context.Courses.Include(c => c.Week).Include(c => c.StudentCourses).Where(c => c.Education.EducationId == education.EducationId);

      List<Course> courseList = new List<Course>();

      foreach (Course course in courses) {
        if (date.DayOfWeek == DayOfWeek.Monday && course.Week.Monday) {
          courseList.Add(course);
        } else if (date.DayOfWeek == DayOfWeek.Tuesday && course.Week.Tuesday) {
          courseList.Add(course);
        } else if (date.DayOfWeek == DayOfWeek.Wednesday && course.Week.Wednesday) {
          courseList.Add(course);
        } else if (date.DayOfWeek == DayOfWeek.Thursday && course.Week.Thursday) {
          courseList.Add(course);
        } else if (date.DayOfWeek == DayOfWeek.Friday && course.Week.Friday) {
          courseList.Add(course);
        }
      }

      return courseList;
    }
  }
}
