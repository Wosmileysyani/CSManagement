using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            var generations = db.Generations.Include(g => g.Short_Course).Include(g => g.Gen_Status1);
            return View(generations.ToList());
        }

        public ActionResult IndexUser()
        {
            var generations = db.Generations.Include(g => g.Short_Course);
            if (Session["Errormessage"] != null)
            {
                if (Session["Errormessage"].ToString().Equals("สมัครได้"))
                {
                    Session.Remove("Errormessage");
                    ViewBag.Message = "ข้อมูลถูกบันทึกแล้ว เลือกคอร์สที่ต้องการสมัครได้เลย";
                }
            }
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

        [HttpPost]
        public JsonResult slipupload(string idcard)
        {
            try
            {
                var IDSC = Convert.ToInt32(Session["IDSC"]);
                var findgen = db.Applieds.AsNoTracking().SingleOrDefault(x => x.APP_ReNO.Equals(idcard) && x.APP_GenNO == IDSC);
                if (findgen != null)
                {
                    foreach (string file in Request.Files)
                    {
                        var fileContent = Request.Files[file];
                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            // get a stream
                            var stream = fileContent.InputStream;
                            // and optionally write the file to disk
                            var myUniqueFileName = DateTime.Now.Ticks + ".jpg";
                            string physicalPath = Server.MapPath("~/img/Slips/" + myUniqueFileName);
                            fileContent.SaveAs(physicalPath);
                            findgen.APP_SlipImg = myUniqueFileName;
                            db.Entry(findgen).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
                return Json("อัพโหลดสำเร็จ กรุณารอการยืนยันจากทางระบบ");
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("อัพโหลดไม่สำเร็จ");
            }
        }

        public ActionResult Registers(int? id = 0)
        {
            if (id == 0)
            {
                if (Session["Errormessage"] != null)
                {
                    if (Session["Errormessage"].ToString().Equals("บัตรซ้ำ"))
                    {
                        Session.Remove("Errormessage");
                        ViewBag.Message = "รหัสบัตรประชาชนถูกใช้แล้ว";
                    }
                }
                ViewBag.rigList = null;
                Session["IDSC"] = 0;
                return View();
            }
            var appList = db.Applieds.Where(x => x.APP_GenNO == id).ToList();
            List<Register_SC> rigList = new List<Register_SC>();
            if (appList.Count != 0)
            {
                foreach (var item in appList)
                {
                    string input = item.Register_SC.REG_Email;
                    string pattern = @"(?<=[\w]{2})[\w-\._\+%]*(?=[\w]{2}@)";
                    string result = Regex.Replace(input, pattern, m => new string('*', m.Length));
                    item.Register_SC.REG_Email = result;
                }
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
            ViewBag.rigList = appList;
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
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
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
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            ViewBag.Gen_SCID = new SelectList(db.Short_Course, "SC_ID", "SC_NameTH");
            ViewBag.Gen_Status = new SelectList(db.Gen_Status, "Gen_Status1", "Gen_Name");
            return View();
        }

        // POST: Generations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Generation generation)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            try
            {
                generation.Gen_Member = generation.Gen_MemberMax;
                db.Generations.Add(generation);
                db.SaveChanges();
                return View();
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
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
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
        public ActionResult Edit(Generation generation)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            try
            {
                var recordToUpdate = db.Generations.AsNoTracking().SingleOrDefault(x => x.Gen_NO == generation.Gen_NO);
                if (generation.Gen_Date.IsEmpty() == true)
                {
                    generation.Gen_Date = recordToUpdate.Gen_Date;
                }
                
                generation.Gen_Status = recordToUpdate.Gen_Status;
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
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
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
