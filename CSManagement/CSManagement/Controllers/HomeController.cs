﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using CSManagement.Models;
using Newtonsoft.Json;

namespace CSManagement.Controllers
{
    public class HomeController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        public ActionResult Index()
        {
            SetSession();
            return View();
        }

        public ActionResult Index2()
        {
            List<Picture> imgPictures = db.Pictures.ToList();
            ViewBag.Images = imgPictures;
            ViewBag.Count = imgPictures.Count;
            return View(imgPictures);
        }

        [HttpGet]
        public ActionResult IndexUser()
        {
            SetSession();
            List<Picture> imgPictures = db.Pictures.ToList();
            ViewBag.Images = imgPictures;
            ViewBag.Count = imgPictures.Count;
            var newDataTable = db.ProjectViews.AsEnumerable().Where(x => x.Pj_Rate == 5)
                .OrderByDescending(r => r.Pj_StuID.Remove(r.Pj_StuID.Length - 2))
                .ThenByDescending(r => r.Pj_Rate);
            ViewBag.ProjectCount = newDataTable.ToList();
            ViewBag.SyllabusCount = db.Syllabus.ToList();
            ViewBag.NewsCount = db.News.Where(x => x.New_Active == true).OrderByDescending(x => x.New_DateStart);
            ViewBag.Link = db.Picture_Banner.ToList();
            ViewBag.Syll = db.Syllabus.ToList();
            return View(imgPictures);
        }

        [HttpGet]
        public ActionResult AnotherLink()
        {
            return View("Index");
        }

        public ActionResult IndexUserGrap()
        {
            ViewBag.Year = new SelectList(db.Courses, "Course_Year", "Course_Year");
            SetSession();
            return View();
        }

        [HttpPost]
        public ActionResult IndexUserGrap(int? Year)
        {
            var coursesList = db.Departments.Where(x => x.Course.Course_Year == Year).Include(x => x.Department_Sup).ToList();
            var pdf = db.Courses.FirstOrDefault(x => x.Course_Year == Year)?.Couese_PDF;
            List<DataPoint> dataPoints = new List<DataPoint>();
            double? total = 0;
            foreach (var item in coursesList)
            {
                total += item.Dep_Credit;
            }
            foreach (var item in coursesList)
            {
                dataPoints.Add(new DataPoint(item.Department_Sup.Deps_Name, Math.Round((Convert.ToDouble(item.Dep_Credit * 100.00 / total)), 2)));
            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            ViewBag.Year = new SelectList(db.Courses, "Course_Year", "Course_Year");
            ViewBag.coursesList = coursesList;
            ViewBag.total += total.ToString();
            ViewBag.pdf = pdf;
            return View();
        }

        public ActionResult Indexoldstudent()
        {
            ViewBag.oldstud = db.Histories.Include(x => x.Student).ToList();
            return View();
        }

        public void SetSession()
        {
            //เก็บคนเข้าชมเว็ป
            var hc = db.HitCounts.FirstOrDefault().HitCount1;
            Session["Totaluser"] = hc;
            //นักศึกษา
            var stCount = db.Students.Count();
            Session["Totalstudent"] = stCount;
            //ผลงาน
            var pjCount = db.Projects.Count();
            Session["Totalpj"] = pjCount;
            //ไอดี
            var logCount = db.Logins.Count();
            Session["Totallogin"] = logCount;
            //Chartหลักสูตร
            var course = db.Courses.ToList();
            var last = course.LastOrDefault()?.Course_Year;
            var coursesList = db.Departments.Where(x => x.Course.Course_Year == last).ToList();
            List<DataPoint> dataPoints = new List<DataPoint>();
            double? total = 0;
            foreach (var item in coursesList)
            {
                total += item.Dep_Credit;
            }
            foreach (var item in coursesList)
            {
                dataPoints.Add(new DataPoint(item.Department_Sup.Deps_Name, Math.Round((Convert.ToDouble(item.Dep_Credit * 100.00 / total)), 2)));
            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            //Chartทำงาน
            var hisList = db.Totaljobs.ToList();
            List<DataPoint> dataPoints2 = new List<DataPoint>();
            double? total2 = 0;
            foreach (var item in hisList)
            {
                total2 += item.JOB;
            }
            foreach (var item in hisList)
            {
                dataPoints2.Add(new DataPoint(item.JOB_Name, Math.Round((Convert.ToDouble(item.JOB * 100.00 / total2)), 2)));
            }
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);
        }
    }
}
