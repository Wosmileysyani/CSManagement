﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CSManagement.Models;

namespace CSManagement.Controllers
{
    public class ComputerEquipmentsController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: ComputerEquipments
        public ActionResult Index()
        {
            var computerEquipments = db.ComputerEquipments.Include(c => c.Teacher);
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            return View(computerEquipments.ToList());
        }

        // GET: ComputerEquipments/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComputerEquipment computerEquipment = db.ComputerEquipments.Find(id);
            if (computerEquipment == null)
            {
                return HttpNotFound();
            }
            return View(computerEquipment);
        }

        public ActionResult Dispose(int? id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var chk = db.Disposes.Where(x => x.CE_ATNO == id).ToList();

                if (chk.Count == 0)
                {
                    Models.Dispose dispose = new Dispose()
                    {
                        DIS_DateOUT = DateTime.Now,
                        DIS_Status = 1,
                        CE_ATNO = id
                    };
                    db.Disposes.Add(dispose);
                    var findCE = db.ComputerEquipments.FirstOrDefault(x => x.CE_ATNO == id);
                    findCE.CE_Status = 3;
                    db.SaveChanges();
                }
                else if (chk.Count > 0)
                {
                    int i = 0;
                    foreach (var collection in chk)
                    {
                        if (collection.DIS_Status == 1 || collection.DIS_Status == 2)
                        {
                            i++;
                        }
                    }

                    if (i == 0)
                    {
                        Models.Dispose dispose = new Dispose()
                        {
                            DIS_DateOUT = DateTime.Now,
                            DIS_Status = 1,
                            CE_ATNO = id
                        };
                        db.Disposes.Add(dispose);
                        var findCE = db.ComputerEquipments.FirstOrDefault(x => x.CE_ATNO == id);
                        findCE.CE_Status = 3;
                        db.SaveChanges();
                    }
                    else
                    {
                        //แสดงข้อความว่ากดจำหน่ายไปแล้ว
                    }
                }
            }
            return RedirectToAction(nameof(Index), "Disposes");
        }

        // GET: ComputerEquipments/Create
        public ActionResult Create()
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            ViewBag.CE_TeaID = new SelectList(db.Teachers, "Tea_ID", "Tea_Name");
            return View();
        }

        // POST: ComputerEquipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ComputerEquipment computerEquipment, string datein)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            try
            {
                computerEquipment.CE_DateIN = DateTime.ParseExact(datein, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                computerEquipment.CE_Status = 1;
                db.ComputerEquipments.Add(computerEquipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.CE_TeaID = new SelectList(db.Teachers, "Tea_ID", "Tea_Name", computerEquipment.CE_TeaID);
                return View(computerEquipment);
            }
        }

        // GET: ComputerEquipments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComputerEquipment computerEquipment = db.ComputerEquipments.Find(id);
            if (computerEquipment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CE_TeaID = new SelectList(db.Teachers, "Tea_ID", "Tea_Name", computerEquipment.CE_TeaID);
            return View(computerEquipment);
        }

        // POST: ComputerEquipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CE_ATNO,CE_NO,CE_DateIN,CE_Name,CE_Noce,CE_Piece,CE_Price,CE_PriceTotal,CE_TeaID,CE_Budget,CE_Status")] ComputerEquipment computerEquipment)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                db.Entry(computerEquipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CE_TeaID = new SelectList(db.Teachers, "Tea_ID", "Tea_Name", computerEquipment.CE_TeaID);
            return View(computerEquipment);
        }

        // GET: ComputerEquipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComputerEquipment computerEquipment = db.ComputerEquipments.Find(id);
            if (computerEquipment == null)
            {
                return HttpNotFound();
            }
            return View(computerEquipment);
        }

        // POST: ComputerEquipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            ComputerEquipment computerEquipment = db.ComputerEquipments.Find(id);
            db.ComputerEquipments.Remove(computerEquipment);
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
