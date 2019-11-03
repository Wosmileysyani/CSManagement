using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CSManagement.Models;

namespace CSManagement.Controllers
{
    public class ProjectsController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: Projects
        public ActionResult Index()
        {
            var projects = db.Projects.Include(p => p.Student).Include(p => p.Teacher);
            return View(projects.ToList());
        }

        public FileStreamResult ShowPdf(int? id)
        {
            var projects = db.Projects.Find(id);
            FileStream fs = new FileStream(Server.MapPath("~/FileUploaded/" + projects.Pj_File), FileMode.Open, FileAccess.Read);
            return File(fs, "application/pdf");
        }

        public void DownloadPdf(int? id)
        {
            var projects = db.Projects.Find(id);
            String Filepath = Server.MapPath("~/FileUploaded/" + projects.Pj_File);
            System.IO.FileInfo file = new System.IO.FileInfo(Filepath); // full file path on disk
            Response.ClearContent(); // Clear previous content
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/pdf";
            Response.TransmitFile(file.FullName);
            Response.WriteFile(file.FullName);
            Response.End();
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.Pj_StuID = new SelectList(db.Students, "Stu_ID", "Stu_Title");
            ViewBag.Pj_TeaID = new SelectList(db.Teachers, "Tea_ID", "Tea_Name");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project, HttpPostedFileBase file)
        {
            try
            {
                FileUploadCheck fs = new FileUploadCheck();
                fs.filesize = 1000000;
                string us = fs.UploadUserFile(file);
                if (us != null)
                {
                    ViewBag.ResultErrorMessage = fs.ErrorMessage;
                }
                project.Pj_Date = DateTime.Today;
                string[] Split_ID = project.Pj_StuID.Split(' ');
                if (file != null && file.ContentLength > 0)
                {
                    var myUniqueFileName = DateTime.Now.Ticks + ".pdf";
                    string physicalPath = Server.MapPath("~/FileUploaded/" + myUniqueFileName);
                    file.SaveAs(physicalPath);
                    project.Pj_File = myUniqueFileName;
                }
                project.Pj_StuID = Split_ID[2];
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Pj_StuID = new SelectList(db.Students, "Stu_ID", "Stu_Title", project.Pj_StuID);
                ViewBag.Pj_TeaID = new SelectList(db.Teachers, "Tea_ID", "Tea_Name", project.Pj_TeaID);
                return View(project);
            }
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pj_StuID = new SelectList(db.Students, "Stu_ID", "Stu_Title", project.Pj_StuID);
            ViewBag.Pj_TeaID = new SelectList(db.Teachers, "Tea_ID", "Tea_Name", project.Pj_TeaID);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Pj_StuID = new SelectList(db.Students, "Stu_ID", "Stu_Title", project.Pj_StuID);
            ViewBag.Pj_TeaID = new SelectList(db.Teachers, "Tea_ID", "Tea_Name", project.Pj_TeaID);
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
