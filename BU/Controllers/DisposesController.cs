﻿using System;
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
    public class DisposesController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: Disposes
        public ActionResult Index()
        {
            return View(db.Disposes.ToList());
        }

        public ActionResult DisposeAgree(int? id)
        {
            var findid = db.Disposes.FirstOrDefault(x => x.DIS_Status == 1 && x.CE_ATNO == id);
            findid.DIS_DateAPP = DateTime.Now;
            findid.DIS_Status = 2;
            db.Entry(findid).State = EntityState.Modified;
            var findCE = db.ComputerEquipments.FirstOrDefault(x => x.CE_ATNO == id);
            findCE.CE_Status = 2;
            var findCESUB = db.CESups.Where(x => x.CE_ATNO == id).ToList();
            foreach (var item in findCESUB)
            {
                item.CESUB_Status = 2;
            }
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult DisposeCancle(int? id)
        {
            var findid = db.Disposes.FirstOrDefault(x => x.DIS_Status == 1 && x.CE_ATNO == id);
            findid.DIS_Status = 3;
            db.Entry(findid).State = EntityState.Modified;
            var findCE = db.ComputerEquipments.FirstOrDefault(x => x.CE_ATNO == id);
            findCE.CE_Status = 1;
            var findCESUB = db.CESups.Where(x => x.CE_ATNO == id).ToList();
            foreach (var item in findCESUB)
            {
                item.CESUB_Status = 1;
            }
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Disposes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispose dispose = db.Disposes.Find(id);
            if (dispose == null)
            {
                return HttpNotFound();
            }
            return View(dispose);
        }

        // GET: Disposes/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dispose dispose)
        {
            if (ModelState.IsValid)
            {
                db.Disposes.Add(dispose);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dispose);
        }

        // GET: Disposes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispose dispose = db.Disposes.Find(id);
            if (dispose == null)
            {
                return HttpNotFound();
            }
            return View(dispose);
        }

        // POST: Disposes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DIS_ATNO,CE_ATNO,DIS_DateOUT,DIS_DateAPP,DIS_Status")] Dispose dispose)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dispose).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dispose);
        }

        // GET: Disposes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispose dispose = db.Disposes.Find(id);
            if (dispose == null)
            {
                return HttpNotFound();
            }
            return View(dispose);
        }

        // POST: Disposes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dispose dispose = db.Disposes.Find(id);
            db.Disposes.Remove(dispose);
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
