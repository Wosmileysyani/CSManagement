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

namespace CSManagement.Controllers
{
    public class TeachersController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: Teachers
        public ActionResult Index()
        {
            var teachers = db.Teachers.Include(t => t.Title);
            return View(teachers.ToList());
        }

        public ActionResult IndexUser()
        {
            var teachers = db.Teachers.Include(t => t.Title);
            return View(teachers.ToList());
        }

        // GET: Teachers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            ViewBag.Tea_TitleID = new SelectList(db.Titles, "Title_ID", "Title_Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Teacher teacher,string birthday, HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string ImageName = Path.GetFileName(file.FileName);
                    var myUniqueFileName = DateTime.Now.Ticks + ".jpg";
                    string physicalPath = Server.MapPath("~/img/" + myUniqueFileName);
                    file.SaveAs(physicalPath);
                    teacher.Tea_Img = myUniqueFileName;
                }
                teacher.Tea_Birth = DateTime.ParseExact(birthday, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Tea_TitleID = new SelectList(db.Titles, "Title_ID", "Title_Name", teacher.Tea_TitleID)  ;
                return View(teacher);
            }
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tea_TitleID = new SelectList(db.Titles, "Title_ID", "Title_Name", teacher.Tea_TitleID);
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Teacher teacher, string birthday, HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string ImageName = Path.GetFileName(file.FileName);
                    var myUniqueFileName = DateTime.Now.Ticks + ".jpg";
                    string physicalPath = Server.MapPath("~/img/" + myUniqueFileName);
                    file.SaveAs(physicalPath);
                    teacher.Tea_Img = myUniqueFileName;
                }
                teacher.Tea_Birth = DateTime.ParseExact(birthday, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                db.Teachers.Add(teacher);
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Teachers", new { id = Session["UserID"].ToString() });
            }
            catch (Exception)
            {
                ViewBag.Tea_TitleID = new SelectList(db.Titles, "Title_ID", "Title_Name", teacher.Tea_TitleID);
                return View(teacher);
            }
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
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
