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
    public class Picture_BannerController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: Picture_Banner
        public ActionResult Index()
        {
            return View(db.Picture_Banner.ToList());
        }

        // GET: Picture_Banner/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture_Banner picture_Banner = db.Picture_Banner.Find(id);
            if (picture_Banner == null)
            {
                return HttpNotFound();
            }
            return View(picture_Banner);
        }

        // GET: Picture_Banner/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Picture_Banner/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Picture_ID,Picture_Name,Picture_Img,Picture_Link")] Picture_Banner picture_Banner)
        {
            if (ModelState.IsValid)
            {
                db.Picture_Banner.Add(picture_Banner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(picture_Banner);
        }

        // GET: Picture_Banner/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture_Banner picture_Banner = db.Picture_Banner.Find(id);
            if (picture_Banner == null)
            {
                return HttpNotFound();
            }
            return View(picture_Banner);
        }

        // POST: Picture_Banner/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Picture_ID,Picture_Name,Picture_Img,Picture_Link")] Picture_Banner picture_Banner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(picture_Banner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(picture_Banner);
        }

        // GET: Picture_Banner/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture_Banner picture_Banner = db.Picture_Banner.Find(id);
            if (picture_Banner == null)
            {
                return HttpNotFound();
            }
            return View(picture_Banner);
        }

        // POST: Picture_Banner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Picture_Banner picture_Banner = db.Picture_Banner.Find(id);
            db.Picture_Banner.Remove(picture_Banner);
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
