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
    public class GenerationsController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: Generations
        public ActionResult Index()
        {
            var generations = db.Generations.Include(g => g.Short_Course);
            return View(generations.ToList());
        }

        public ActionResult Registers(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Generation generation = db.Generations.Find(id);
            if (generation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Gen_SCID = generation.Short_Course.SC_NameTH + " รุ่นที่ " + generation.Gen_Name;
            Session.Remove("IDSC");
            Session["IDSC"] = generation.Gen_NO.ToString();
            return View();
        }

        // GET: Generations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Generation generation = db.Generations.Find(id);
            if (generation == null)
            {
                return HttpNotFound();
            }
            return View(generation);
        }

        // GET: Generations/Create
        public ActionResult Create()
        {
            ViewBag.Gen_SCID = new SelectList(db.Short_Course, "SC_ID", "SC_NameTH");
            return View();
        }

        // POST: Generations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Generation generation)
        {
            generation.Gen_Member = generation.Gen_MemberMax;
            if (ModelState.IsValid)
            {
                db.Generations.Add(generation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Gen_SCID = new SelectList(db.Short_Course, "SC_ID", "SC_NameTH", generation.Gen_SCID);
            return View(generation);
        }

        // GET: Generations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Generation generation = db.Generations.Find(id);
            if (generation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Gen_SCID = new SelectList(db.Short_Course, "SC_ID", "SC_NameTH", generation.Gen_SCID);
            return View(generation);
        }

        // POST: Generations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Gen_NO,Gen_SCID,Gen_Name,Gen_Member,Gen_MemberMax,Gen_Details,Gen_Year,Gen_Fee,Gen_TextForMail")] Generation generation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(generation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Gen_SCID = new SelectList(db.Short_Course, "SC_ID", "SC_NameTH", generation.Gen_SCID);
            return View(generation);
        }

        // GET: Generations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Generation generation = db.Generations.Find(id);
            if (generation == null)
            {
                return HttpNotFound();
            }
            return View(generation);
        }

        // POST: Generations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Generation generation = db.Generations.Find(id);
            db.Generations.Remove(generation);
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
