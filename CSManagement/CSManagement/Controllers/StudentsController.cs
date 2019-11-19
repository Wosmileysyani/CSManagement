using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CSManagement.Models;
using Microsoft.Ajax.Utilities;

namespace CSManagement.Controllers
{
    public class StudentsController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(x=>x.Status).ToList();
            return View(students);
        }

       

        // GET: Students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            List<History> history = db.Histories.Where(x => x.HIS_StuID == id).OrderByDescending(x => x.HIS_Year).ToList();
            if (history.Any())
            {
                Session.Remove("HistoryStu");
                Session["HistoryStu"] = history;
            }
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.Stu_School = new SelectList(db.Schools, "SCH_ID", "SCH_Name");
            ViewBag.Stu_StatusID = new SelectList(db.Status, "Status_ID", "Status_Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student, string birthday, string tel, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string ImageName = Path.GetFileName(file.FileName);
                var myUniqueFileName = DateTime.Now.Ticks + ".jpg";
                string physicalPath = Server.MapPath("~/img/" + myUniqueFileName);
                file.SaveAs(physicalPath);
                student.Stu_Img = myUniqueFileName;
            }
            ViewBag.Stu_School = new SelectList(db.Schools, "SCH_ID", "SCH_Name", student.Stu_School);
            ViewBag.Stu_StatusID = new SelectList(db.Status, "Status_ID", "Status_Name", student.Stu_StatusID);
            student.Stu_Birthday = DateTime.ParseExact(birthday, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            student.Stu_Tel = tel;
                db.Students.Add(student);
                db.SaveChanges();
            return RedirectToAction("Index");

            //return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.Stu_School = new SelectList(db.Schools, "SCH_ID", "SCH_Name", student.Stu_School);
            ViewBag.Stu_StatusID = new SelectList(db.Status, "Status_ID", "Status_Name", student.Stu_StatusID);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Students", new { id = Session["UserID"].ToString() });
            }
            ViewBag.Stu_School = new SelectList(db.Schools, "SCH_ID", "SCH_Name", student.Stu_School);
            return View(student);
        }

        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
