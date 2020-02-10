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
    public class DepartmentsController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();
        // GET: Departments
        public ActionResult Index()
        {
            var departments = db.Departments.Include(d => d.Course);
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            return View(departments.ToList());
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            ViewBag.Dep_CourseID = new SelectList(db.Courses, "Course_ID", "Course_NameTH");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Dep_CourseID = new SelectList(db.Courses, "Course_ID", "Course_NameTH", department.Dep_CourseID);
            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.Dep_CourseID = new SelectList(db.Courses, "Course_ID", "Course_NameTH");
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department department)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Dep_CourseID = new SelectList(db.Courses, "Course_ID", "Course_NameTH", department.Dep_CourseID);
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
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
