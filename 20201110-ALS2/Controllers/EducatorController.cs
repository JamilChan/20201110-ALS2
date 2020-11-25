﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20201110_ALS2.Models;
using _20201110_ALS2.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _20201110_ALS2.Controllers {
  public class EducatorController : Controller {
    private IStudentRepository studentRepo;
    private ICourseRepository courseRepo;
    private IEducatorRepository educatorRepo;
    private IStudentCourseRepository scRepo;

    public EducatorController(IStudentRepository studentRepo , ICourseRepository courseRepo, IEducatorRepository educatorRepo, IStudentCourseRepository scRepo) {
      this.studentRepo = studentRepo;
      this.courseRepo = courseRepo;
      this.educatorRepo = educatorRepo;
      this.scRepo = scRepo;
    }

    [HttpGet]
    public ViewResult AbsenceList() {
      return View(studentRepo.Students);
    }

    [HttpPost]
    public IActionResult AbsenceList(int[] attended, int[] absence) {
      if (ModelState.IsValid) {
        
      }
      List<int[]> list = new List<int[]>();
      list.Add(attended);
      list.Add(absence);

      return View("TestView", list);
    }

    [HttpGet]
    public ViewResult CreateCourse() {
      CreateCourseViewModel ccvm = CreateCCVM();

      return View("CreateCourse", ccvm);
    }

    [HttpPost]
    public IActionResult CreateCourse(CreateCourseViewModel ccvm, IFormCollection form) {
      if (ModelState.IsValid) {
        foreach (Educator e in educatorRepo.Educators) {
          if (e.Name == ccvm.SelectedEducator) {
            ccvm.Crs.Educator = e;
            break;
          }
        }
        string selected = Request.Form["SelectedStudents"].ToString();
        string[] selectedList = selected.Split(',');
        foreach (var sId in selectedList) {
          StudentCourse sc = new StudentCourse { Course = ccvm.Crs, Student = studentRepo.Students.FirstOrDefault(s => s.StudentId == Int32.Parse(sId)) };
          scRepo.CreateStudentCourse(sc);
        }
        courseRepo.SaveCourse(ccvm.Crs);
        TempData["message"] = $"{ccvm.Crs.Name} has been saved";
        return RedirectToAction("ViewCourses");
      } else {
        ccvm.EducatorList = educatorRepo.Educators;
        ccvm.GetEducatorsName();
        return View("CreateCourse", ccvm); 
      }
    }

    [HttpGet]
    public ViewResult ViewCourses() {
      return View("ViewCourses", courseRepo.Courses);
    }

    [HttpGet]
    public ViewResult EditCourse(int courseId) {
      CreateCourseViewModel ccvm = CreateCCVM();
      ccvm.Crs = courseRepo.Courses.FirstOrDefault(c => c.CourseId == courseId);
      ccvm.SelectedEducator = ccvm.Crs.Educator.Name;

      return View("CreateCourse", ccvm);
    }

    [HttpPost]
    public IActionResult DeleteCourse(int CourseId) {
      courseRepo.Delete(CourseId);

      return View("ViewCourses", courseRepo.Courses);
    }

    private CreateCourseViewModel CreateCCVM() {
      CreateCourseViewModel ccvm = new CreateCourseViewModel();
      ccvm.EducatorList = educatorRepo.Educators;
      ccvm.GetEducatorsName();
      ccvm.StudentList = studentRepo.Students;

      return ccvm;
    }
  }
}
