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
    public class ComputerEquipmentsController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: ComputerEquipments
        public ActionResult Index()
        {
            var computerEquipments = db.ComputerEquipments.Include(c => c.Teacher);
            return View(computerEquipments.ToList());
        }

        // GET: ComputerEquipments/Details/5
        public ActionResult Details(int? id)
        {
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

        // GET: ComputerEquipments/Create
        public ActionResult Create()
        {
            ViewBag.CE_TeaID = new SelectList(db.Teachers, "Tea_ID", "Tea_Name");
            return View();
        }

        // POST: ComputerEquipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ComputerEquipment computerEquipment)
        {
            try
            {
                computerEquipment.CE_Status = 1;
                db.ComputerEquipments.Add(computerEquipment);
                db.SaveChanges();
                var getno = db.ComputerEquipments.Last().CE_ATNO;

                for (int i = 0; i < computerEquipment.CE_Piece; i++)
                {
                    CESup ceSup = new CESup()
                    {
                        CESUB_NO = computerEquipment.CE_NO,
                        CESUB_Name = computerEquipment.CE_Name,
                        CESUB_Status = 1,
                        CE_ATNO = getno
                    };
                    db.CESups.Add(ceSup);
                }
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
