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

    public IQueryable<Course> Courses => context.Courses.Include(c => c.Educator).Include(c => c.Week).Include(c => c.StudentCourses);


    public void SaveCourse(Course course) {
      if (course.CourseId == 0) {
        context.Courses.Add(course);
      } else {
        Course dbEntry = context.Courses.FirstOrDefault(c => c.CourseId == course.CourseId);
        if (dbEntry != null) {
          dbEntry.Name = course.Name;
          dbEntry.Educator = course.Educator;
          dbEntry.StartDate = course.StartDate;
          dbEntry.EndDate = course.EndDate;
        }
      }
      context.SaveChanges();
    }

    public Course Delete(int courseId) {
      Course dbEntry = context.Courses.FirstOrDefault(c => c.CourseId == courseId);
      if (dbEntry != null) {
        context.Courses.Remove(dbEntry);
        context.SaveChanges();
      }
      return dbEntry;
    }
  }
}
