using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CSManagement.Models;

namespace CSManagement.Controllers
{
    public class TeachersController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: Teachers
        public ActionResult Index()
        {
            var teachers = db.Teachers.Include(t => t.Title);
            return View(teachers.ToList());
        }

        // GET: Teachers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            ViewBag.Tea_TitleID = new SelectList(db.Titles, "Title_ID", "Title_Name");
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Tea_ID,Tea_TitleID,Tea_Name,Tea_Surname,Tea_Birth,Tea_Img,Tea_Export,Tea_LvEdu,Tea_Program,Tea_Position")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tea_TitleID = new SelectList(db.Titles, "Title_ID", "Title_Name", teacher.Tea_TitleID);
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tea_TitleID = new SelectList(db.Titles, "Title_ID", "Title_Name", teacher.Tea_TitleID);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Tea_ID,Tea_TitleID,Tea_Name,Tea_Surname,Tea_Birth,Tea_Img,Tea_Export,Tea_LvEdu,Tea_Program,Tea_Position")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Tea_TitleID = new SelectList(db.Titles, "Title_ID", "Title_Name", teacher.Tea_TitleID);
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
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
