using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using CSManagement.Models;

namespace CSManagement.Controllers
{
    public class GenerationsController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: Generations
        public ActionResult Index()
        {
            var generations = db.Generations.Include(g => g.Short_Course).Include(g => g.Gen_Status1);
            return View(generations.ToList());
        }

        public ActionResult IndexUser()
        {
            var generations = db.Generations.Include(g => g.Short_Course);
            return View(db.Generations.ToList());
        }

        public ActionResult showdetails(int id)
        {
            var vm = new Generation();

            var question = db.Generations.FirstOrDefault(a => a.Gen_NO == id);
            if (question != null)
            {
                vm = question;
            }

            return PartialView(vm);
        }

        public ActionResult Registers(int? id)
        {
            var appList = db.Applieds.Where(x => x.APP_GenNO == id).ToList();
            List<Register_SC> rigList = new List<Register_SC>();
            foreach (var item in appList)
            {
                rigList = db.Register_SC.Where(x => x.REG_IDCard == item.APP_ReNO).ToList();
            }
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
            ViewBag.rigList = rigList;
            ViewBag.genList = generation;
            Session.Remove("IDSC");
            Session["IDSC"] = generation.Gen_NO.ToString();
            return View();
        }

        public ActionResult DetailsUser(int? id)
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
            ViewBag.Gen_Status = new SelectList(db.Gen_Status, "Gen_Status1", "Gen_Name");
            return View();
        }

        // POST: Generations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Generation generation, string gendate)
        {
            try
            {
                generation.Gen_Member = generation.Gen_MemberMax;
                generation.Gen_Date = DateTime.ParseExact(gendate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                db.Generations.Add(generation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Gen_SCID = new SelectList(db.Short_Course, "SC_ID", "SC_NameTH", generation.Gen_SCID);
                ViewBag.Gen_Status = new SelectList(db.Gen_Status, "Gen_Status1", "Gen_Name", generation.Gen_Status);
                return View(generation);
            }


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
            ViewBag.Gen_Status = new SelectList(db.Gen_Status, "Gen_Status1", "Gen_Name", generation.Gen_Status);
            return View(generation);
        }

        // POST: Generations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Generation generation, string gendate)
        {
            try
            {
                var recordToUpdate = db.Generations.AsNoTracking().SingleOrDefault(x => x.Gen_NO == generation.Gen_NO);
                if (gendate.IsEmpty() == true)
                {
                    generation.Gen_Date = recordToUpdate.Gen_Date;
                }
                else
                {
                    generation.Gen_Date = DateTime.ParseExact(gendate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                }

                generation.Gen_Member = (generation.Gen_MemberMax - recordToUpdate.Gen_MemberMax) + recordToUpdate.Gen_Member;
                if (generation.Gen_Member < 0)
                {
                    ViewBag.Message = "จำนวนสมาชิกที่เหลืออยู่ไม่ถูกต้อง";
                    ViewBag.Gen_SCID = new SelectList(db.Short_Course, "SC_ID", "SC_NameTH", generation.Gen_SCID);
                    ViewBag.Gen_Status = new SelectList(db.Gen_Status, "Gen_Status1", "Gen_Name", generation.Gen_Status);
                    return View(generation);
                }
                db.Entry(generation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Gen_SCID = new SelectList(db.Short_Course, "SC_ID", "SC_NameTH", generation.Gen_SCID);
                ViewBag.Gen_Status = new SelectList(db.Gen_Status, "Gen_Status1", "Gen_Name", generation.Gen_Status);
                return View(generation);
            }
        }

        // GET: Generations/Delete/5
        public ActionResult Delete(int? id)
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
