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
    public class CESupsController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: CESups
        public ActionResult Index()
        {
            return View(db.CESups.ToList());
        }

        // GET: CESups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CESup cESup = db.CESups.Find(id);
            if (cESup == null)
            {
                return HttpNotFound();
            }
            return View(cESup);
        }

        // GET: CESups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CESups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CESUB_ATNO,CESUB_NO,CESUB_Name,CESUB_Status,CE_ATNO")] CESup cESup)
        {
            if (ModelState.IsValid)
            {
                db.CESups.Add(cESup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cESup);
        }

        // GET: CESups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CESup cESup = db.CESups.Find(id);
            if (cESup == null)
            {
                return HttpNotFound();
            }
            return View(cESup);
        }

        // POST: CESups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CESUB_ATNO,CESUB_NO,CESUB_Name,CESUB_Status,CE_ATNO")] CESup cESup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cESup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cESup);
        }

        // GET: CESups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CESup cESup = db.CESups.Find(id);
            if (cESup == null)
            {
                return HttpNotFound();
            }
            return View(cESup);
        }

        // POST: CESups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CESup cESup = db.CESups.Find(id);
            db.CESups.Remove(cESup);
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
