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
using System.Web.WebPages;
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
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            return View(teachers.ToList());
        }

        public ActionResult EditID(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login teachers = db.Logins.Find(id);
            if (teachers == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!teachers.Log_ID.Equals(Session["UserID"].ToString()))
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(teachers);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditID(Login login)
        {
            try
            {
                var recordToUpdate = db.Logins.AsNoTracking().Single(x => x.Log_ID == login.Log_ID);
                login.Log_Role = recordToUpdate.Log_Role;
                db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "อัพเดทข้อมูลเสร็จสิิ้น กรุณาออกจากระบบและลองเข้าใหม่อีกครั้ง";
                return View(login);
            }
            catch (Exception)
            {
                ViewBag.Message = "อัพเดทข้อมูลไม่สำเร็จ กรุณาตรวจสอบข้อมูลและลองใหม่อีกครั้ง";
                return View(login);
            }
        }

        public ActionResult IndexUser()
        {
            var teachers = db.Teachers.Include(t => t.Title);
            return View(teachers.ToList());
        }

        public ActionResult showdetails(string id)
        {
            var vm = new Teacher();

            var question = db.Teachers.FirstOrDefault(x => x.Tea_ID.Equals(id));
            if (question != null)
            {
                vm = question;
            }

            return PartialView(vm);
        }

        // GET: Teachers/Details/5
        public ActionResult Details(string id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
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
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            ViewBag.Tea_TitleID = new SelectList(db.Titles, "Title_ID", "Title_Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Teacher teacher, HttpPostedFileBase file)
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
                var Tea_Birth = Request["Tea_Birth"];
                teacher.Tea_Birth = DateTime.ParseExact(Tea_Birth, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                teacher.Tea_Birth = teacher.Tea_Birth.Value.AddYears(-543);
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Tea_TitleID = new SelectList(db.Titles, "Title_ID", "Title_Name", teacher.Tea_TitleID);
                return View(teacher);
            }
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(string id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
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
        public ActionResult Edit(Teacher teacher, HttpPostedFileBase file)
        {
            try
            {
                var recordToUpdate = db.Teachers.AsNoTracking().Single(x => x.Tea_ID == teacher.Tea_ID);
                var Tea_Birth = Request["Tea_Birth"];
                teacher.Tea_Birth = DateTime.ParseExact(Tea_Birth, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                var today = DateTime.Today;
                if (teacher.Tea_Birth == today)
                {
                    teacher.Tea_Birth = recordToUpdate.Tea_Birth;
                }
                else
                {
                    teacher.Tea_Birth = teacher.Tea_Birth.Value.AddYears(-543);
                }
                if (file != null && file.ContentLength > 0)
                {
                    string ImageName = Path.GetFileName(file.FileName);
                    var myUniqueFileName = DateTime.Now.Ticks + ".jpg";
                    string physicalPath = Server.MapPath("~/img/" + myUniqueFileName);
                    file.SaveAs(physicalPath);
                    teacher.Tea_Img = myUniqueFileName;
                }
                else
                {
                    teacher.Tea_Img = recordToUpdate.Tea_Img;
                }
                db.Teachers.Add(teacher);
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Teachers", new { id = Session["UserID"].ToString() });
            }
            catch (Exception)
            {
                ViewBag.Message = "อัพเดทข้อมูลไม่สำเร็จ กรุณาตรวจสอบข้อมูลและลองใหม่อีกครั้ง";
                ViewBag.Tea_TitleID = new SelectList(db.Titles, "Title_ID", "Title_Name", teacher.Tea_TitleID);
                return View(teacher);
            }
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(string id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
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
