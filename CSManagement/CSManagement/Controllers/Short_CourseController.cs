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
    public class Short_CourseController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: Short_Course
        public ActionResult Index()
        {
            return View(db.Short_Course.ToList());
        }

        

        // GET: Short_Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Short_Course short_Course = db.Short_Course.Find(id);
            if (short_Course == null)
            {
                return HttpNotFound();
            }
            return View(short_Course);
        }

        // GET: Short_Course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Short_Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SC_ID,SC_NameTH")] Short_Course short_Course)
        {
            if (ModelState.IsValid)
            {
                db.Short_Course.Add(short_Course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(short_Course);
        }

        // GET: Short_Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Short_Course short_Course = db.Short_Course.Find(id);
            if (short_Course == null)
            {
                return HttpNotFound();
            }
            return View(short_Course);
        }

        // POST: Short_Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SC_ID,SC_NameTH")] Short_Course short_Course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(short_Course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(short_Course);
        }

        // GET: Short_Course/Delete/5
        public ActionResult Delete(int? id)
        {
            Short_Course short_Course = db.Short_Course.Find(id);
            db.Short_Course.Remove(short_Course);
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
