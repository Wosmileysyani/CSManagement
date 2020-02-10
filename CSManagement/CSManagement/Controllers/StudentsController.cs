using CSManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages;

namespace CSManagement.Controllers
{
    public class StudentsController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(x => x.Status).ToList();
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            return View(students);
        }

        public ActionResult Indexforreport()
        {
            var students = db.Students.Include(x => x.Status).ToList();
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            return View(students);
        }

        public ActionResult Indexforschool()
        {
            var schooList = db.TotalSchools.ToList();
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            return View(schooList);
        }


        public ActionResult EditID(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login student = db.Logins.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
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
                return RedirectToAction("Logout","Logins");
            }
            catch (Exception)
            {
                ViewBag.Message = "อัพเดทข้อมูลไม่สำเร็จ กรุณาตรวจสอบข้อมูลและลองใหม่อีกครั้ง";
                return View(login);
            }
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
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var myUniqueFileName = DateTime.Now.Ticks + ".jpg";
                    string physicalPath = Server.MapPath("~/img/" + myUniqueFileName);
                    file.SaveAs(physicalPath);
                    student.Stu_Img = myUniqueFileName;
                }

                student.Stu_Birthday = DateTime.ParseExact(birthday, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                student.Stu_Tel = tel;
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Stu_School = new SelectList(db.Schools, "SCH_ID", "SCH_Name", student.Stu_School);
                ViewBag.Stu_StatusID = new SelectList(db.Status, "Status_ID", "Status_Name", student.Stu_StatusID);
                return View(student);
            }
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
        public ActionResult Edit(Student student, string birthday, string tel, HttpPostedFileBase file)
        {
            try
            {
                var recordToUpdate = db.Students.AsNoTracking().Single(x => x.Stu_ID == student.Stu_ID);
                student.Stu_Birthday = birthday.IsEmpty() == true ? recordToUpdate.Stu_Birthday : DateTime.ParseExact(birthday, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                if (file != null && file.ContentLength > 0)
                {
                    var myUniqueFileName = DateTime.Now.Ticks + ".jpg";
                    string physicalPath = Server.MapPath("~/img/" + myUniqueFileName);
                    file.SaveAs(physicalPath);
                    student.Stu_Img = myUniqueFileName;
                }
                else
                {
                    student.Stu_Img = recordToUpdate.Stu_Img;
                }
                student.Stu_Tel = tel.IsEmpty() != true ? tel : recordToUpdate.Stu_Tel;
                student.Stu_StatusID = recordToUpdate.Stu_StatusID;
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return Session["AJ"] == null ? RedirectToAction("Logout", "Logins") : RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Message = "อัพเดทข้อมูลไม่สำเร็จ กรุณาตรวจสอบข้อมูลและลองใหม่อีกครั้ง";
                ViewBag.Stu_School = new SelectList(db.Schools, "SCH_ID", "SCH_Name", student.Stu_School);
                ViewBag.Stu_StatusID = new SelectList(db.Status, "Status_ID", "Status_Name", student.Stu_StatusID);
                return View(student);
            }

        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            Student students = db.Students.Find(id);
            db.Students.Remove(students);
            Login login = db.Logins.Find(id);
            db.Logins.Remove(login);
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
