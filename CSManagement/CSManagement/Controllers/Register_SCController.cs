using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CSManagement.Models;
using Newtonsoft.Json;

namespace CSManagement.Controllers
{
    public class Register_SCController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: Register_SC
        public ActionResult Index()
        {
            return View(db.Register_SC.ToList());
        }

        // GET: Register_SC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register_SC register_SC = db.Register_SC.Find(id);
            if (register_SC == null)
            {
                return HttpNotFound();
            }
            return View(register_SC);
        }

        public JsonResult findIDCard(string idcard = "")
        {
            var list = db.Register_SC.Where(x => x.REG_IDCard == idcard).Include(x => x.Applieds).ToList();
            if (list.Count > 0)
            {
                var data = from r in list
                           select new
                           {
                               r.REG_IDCard,
                               r.REG_Address,
                               r.REG_Email,
                               r.REG_Name,
                               r.REG_Tel
                           };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(JsonConvert.DeserializeObject("null"), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Register_SC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Register_SC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Register_SC register_SC)
        {
            try
            {
                var chkIDCard = db.Register_SC.FirstOrDefault(x => x.REG_IDCard == register_SC.REG_IDCard);
                if (Convert.ToInt32(Session["IDSC"]) == 0)
                {
                    if (chkIDCard == null)
                    {
                        db.Register_SC.Add(register_SC);
                        Session["Errormessage"] = "สมัครได้";
                        db.SaveChanges();
                        return RedirectToAction("IndexUser", "Generations", new { id = 0 });
                    }
                    else
                    {
                        Session["Errormessage"] = "บัตรซ้ำ";
                        return RedirectToAction("Registers", "Generations", new { id = 0 });
                    }
                }
                Applied applied = new Applied()
                {
                    APP_ReNO = register_SC.REG_IDCard,
                    APP_GenNO = Convert.ToInt32(Session["IDSC"]),
                    APP_Status = 3
                };
                Session.Remove("IDSC");
                db.Applieds.Add(applied);
                if (chkIDCard == null)
                {
                    db.Register_SC.Add(register_SC);
                }
                db.SaveChanges();
                return RedirectToAction("IndexUser", "Generations", new { id = Convert.ToInt32(Session["IDSC"]) });
            }
            catch (Exception)
            {
                return View(register_SC);
            }


        }

        // GET: Register_SC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register_SC register_SC = db.Register_SC.Find(id);
            if (register_SC == null)
            {
                return HttpNotFound();
            }
            return View(register_SC);
        }

        // POST: Register_SC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "REG_IDCard,REG_Name,REG_Address,REG_Tel,REG_Email")] Register_SC register_SC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(register_SC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(register_SC);
        }

        // GET: Register_SC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register_SC register_SC = db.Register_SC.Find(id);
            if (register_SC == null)
            {
                return HttpNotFound();
            }
            return View(register_SC);
        }

        // POST: Register_SC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Register_SC register_SC = db.Register_SC.Find(id);
            db.Register_SC.Remove(register_SC);
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
