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
    public class RegisterLinksController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: RegisterLinks
        public ActionResult Index()
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            return View(db.RegisterLinks.ToList());
        }

        // GET: RegisterLinks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterLink registerLink = db.RegisterLinks.Find(id);
            if (registerLink == null)
            {
                return HttpNotFound();
            }
            return View(registerLink);
        }

        // GET: RegisterLinks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegisterLinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterLink registerLink)
        {
            if (ModelState.IsValid)
            {
                db.RegisterLinks.Add(registerLink);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(registerLink);
        }

        // GET: RegisterLinks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterLink registerLink = db.RegisterLinks.Find(id);
            if (registerLink == null)
            {
                return HttpNotFound();
            }
            return View(registerLink);
        }

        // POST: RegisterLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegisterLink registerLink)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registerLink).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(registerLink);
        }

        // GET: RegisterLinks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterLink registerLink = db.RegisterLinks.Find(id);
            if (registerLink == null)
            {
                return HttpNotFound();
            }
            return View(registerLink);
        }

        // POST: RegisterLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegisterLink registerLink = db.RegisterLinks.Find(id);
            db.RegisterLinks.Remove(registerLink);
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
