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

        [HttpGet]
        public ActionResult Index()
        {
            SetSession();
            return View();
        }

        [HttpGet]
        public ActionResult AnotherLink()
        {
            return View("Index");
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
            var coursesList = db.Departments.ToList();
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
                total2 += item.count;
            }
            foreach (var item in hisList)
            {
                dataPoints2.Add(new DataPoint(item.JOB_Name, Math.Round((Convert.ToDouble(item.count * 100.00 / total2)), 2)));
            }
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);
        }
    }
}
