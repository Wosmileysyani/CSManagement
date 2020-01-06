using System;
using System.Collections.Generic;
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

        [HttpGet]
        public ActionResult IndexUser()
        {
            SetSession();
            List<Picture> imgPictures = db.Pictures.ToList();
            ViewBag.Images = imgPictures;
            ViewBag.Count = imgPictures.Count;
            ViewBag.ProjectCount = db.Projects.ToList();
            ViewBag.SyllabusCount = db.Syllabus.ToList();
            ViewBag.NewsCount = db.News.ToList();
            return View(imgPictures);
        }

        [HttpGet]
        public ActionResult IndexUser1()
        {
            SetSession();
            List<Picture> imgPictures = db.Pictures.ToList();
            ViewBag.Images = imgPictures;
            ViewBag.Count = imgPictures.Count;
            ViewBag.ProjectCount = db.Projects.ToList();
            return View(db.Pictures.ToList());
        }

        [HttpGet]
        public ActionResult AnotherLink()
        {
            return View("Index");
        }

        public ActionResult GoRegister()
        {
            string link = db.RegisterLinks.FirstOrDefault().Weblink;
            return Redirect(link);
        }

        public ActionResult IndexUserGrap()
        {
            ViewBag.Year = new SelectList(db.Courses, "Course_ID", "Course_Year");
            SetSession();
            return View();
        }

        [HttpPost]
        public ActionResult IndexUserGrap(int? Year)
        {
            var coursesList = db.Departments.Where(x => x.Dep_CourseID == Year).ToList();
            List<DataPoint> dataPoints = new List<DataPoint>();
            double? total = 0;
            foreach (var item in coursesList)
            {
                total += item.Dep_Credit;
            }
            foreach (var item in coursesList)
            {
                dataPoints.Add(new DataPoint(item.Dep_Name, Math.Round((Convert.ToDouble(item.Dep_Credit * 100.00 / total)), 2)));
            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            ViewBag.Year = new SelectList(db.Courses, "Course_ID", "Course_Year");
            ViewBag.coursesList = coursesList;
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
            var coursesList = db.Departments.OrderByDescending(x => x.Dep_CourseID).ToList();
            List<DataPoint> dataPoints = new List<DataPoint>();
            double? total = 0;
            foreach (var item in coursesList)
            {
                total += item.Dep_Credit;
            }
            foreach (var item in coursesList)
            {
                dataPoints.Add(new DataPoint(item.Dep_Name, Math.Round((Convert.ToDouble(item.Dep_Credit * 100.00 / total)), 2)));
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
