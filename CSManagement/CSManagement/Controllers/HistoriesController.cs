using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using CSManagement.Models;

namespace CSManagement.Controllers
{
    public class HistoriesController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: Histories
        public ActionResult Index()
        {
            var histories = db.Histories.Include(h => h.Job).Include(h => h.Student);
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            return View(histories.ToList());
        }


        public JsonResult GetSearchValue(string search)
        {
            List<StudentName> allsearch = db.Students.Where(x => x.Stu_Name.Contains(search)).Select(x => new StudentName
            {
                Stu_ID = x.Stu_ID + " " + x.Stu_Name + " " + x.Stu_Surname,
                Stu_Name = x.Stu_Name + " " + x.Stu_Surname
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: Histories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        // GET: Histories/Create
        public ActionResult Create()
        {
            ViewBag.HIS_Job = new SelectList(db.Jobs, "JOB_ID", "JOB_Name");
            ViewBag.HIS_StuID = new SelectList(db.Students, "Stu_ID", "Stu_Title");
            return View();
        }

        // POST: Histories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(History history)
        {
            string[] Split_ID = history.HIS_StuID.Split(' ');
            if (history.HIS_Year == null) history.HIS_Year = " ";
            if (ModelState.IsValid)
            {
                history.HIS_StuID = Split_ID[0];
                db.Histories.Add(history);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HIS_Job = new SelectList(db.Jobs, "JOB_ID", "JOB_Name", history.HIS_Job);
            ViewBag.HIS_StuID = new SelectList(db.Students, "Stu_ID", "Stu_Title", history.HIS_StuID);
            return View(history);
        }

        // GET: Histories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            ViewBag.HIS_Job = new SelectList(db.Jobs, "JOB_ID", "JOB_Name", history.HIS_Job);
            ViewBag.HIS_StuID = new SelectList(db.Students, "Stu_ID", "Stu_Title", history.HIS_StuID);
            return View(history);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(History history)
        {
            if (ModelState.IsValid)
            {
                db.Entry(history).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HIS_Job = new SelectList(db.Jobs, "JOB_ID", "JOB_Name", history.HIS_Job);
            ViewBag.HIS_StuID = new SelectList(db.Students, "Stu_ID", "Stu_Title", history.HIS_StuID);
            return View(history);
        }

        // GET: Histories/Delete/5
        public ActionResult Delete(int? id)
        {
            History history = db.Histories.Find(id);
            db.Histories.Remove(history);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
