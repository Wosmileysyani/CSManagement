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

        public ActionResult Index()
        {
            var projects = db.Projects.Include(p => p.Stu_Pro).Include(p => p.Tea_Pro);
            if (Session["AJ"] == null && Session["PJ"] == null) return RedirectToAction("Index", "Home");
            return View(projects.ToList());
        }

        public ActionResult IndexUser()
        {
            var newDataTable = db.ProjectViews.AsEnumerable()
                .OrderBy(r => r.Pj_StuID.Remove(r.Pj_StuID.Length - 2))
                .ThenByDescending(r => r.Pj_Rate);
            ViewBag.ProjectCount = newDataTable.ToList();
            return View();
        }

        public ActionResult ShowPdf(int? id)
        {
            var projects = db.Projects.Find(id);
            //FileStream fs = new FileStream(Server.MapPath("~/FileUploaded/" + projects.Pj_File), FileMode.Open, FileAccess.Read);
            //var fsResult = new FileStreamResult(fs, "application/pdf");
            //var myUniqueFileName = DateTime.Now.Ticks + ".pdf";
            ////Response.AddHeader("content-disposition", "attachment; filename=" + myUniqueFileName);

            var fileStream = new FileStream(Server.MapPath("~/FileUploaded/" + projects.Pj_File),
                FileMode.Open,
                FileAccess.Read
            );
            var fsResult = new FileStreamResult(fileStream, "application/pdf");
            return fsResult;
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
            if (Session["AJ"] == null && Session["PJ"] == null) return RedirectToAction("Index", "Home");
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
            if (Session["AJ"] == null && Session["PJ"] == null) return RedirectToAction("Index", "Home");
            ViewBag.Pj_StuID = new SelectList(db.Students, "Stu_ID", "Stu_Title");
            ViewBag.Pj_TeaID = new SelectList(db.Teachers, "Tea_ID", "Tea_Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project, HttpPostedFileBase file)
        {
            try
            {
                project.Pj_Rate = 3;
                FileUploadCheck fs = new FileUploadCheck();
                fs.filesize = 1000000;
                string us = fs.UploadUserFile(file);
                if (us != null)
                {
                    ViewBag.ResultErrorMessage = fs.ErrorMessage;
                    ViewBag.Pj_StuID = new SelectList(db.Students, "Stu_ID", "Stu_Title", project.Pj_StuID);
                    ViewBag.Pj_TeaID = new SelectList(db.Teachers, "Tea_ID", "Tea_Name", project.Pj_TeaID);
                    return View(project);
                }
                else
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var myUniqueFileName = DateTime.Now.Ticks + ".pdf";
                        string physicalPath = Server.MapPath("~/FileUploaded/" + myUniqueFileName);
                        file.SaveAs(physicalPath);
                        project.Pj_File = myUniqueFileName;
                    }
                    project.Pj_Date = DateTime.Today;
                    string[] Split_ID = project.Pj_StuID.Split(' ');
                    if (Session["PJ"] == null)
                    {
                        project.Pj_StuID = Split_ID[0];
                    }
                    db.Projects.Add(project);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
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
            if (Session["AJ"] == null && Session["PJ"] == null) return RedirectToAction("Index", "Home");
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project project, HttpPostedFileBase file)
        {
            try
            {
                var recordToUpdate = db.Projects.AsNoTracking().Single(x => x.Pj_ID == project.Pj_ID);
                FileUploadCheck fs = new FileUploadCheck();
                fs.filesize = 1000000;
                if (file == null)
                {
                    project.Pj_File = recordToUpdate.Pj_File;
                }
                string us = fs.UploadUserFile(file);
                if (us != null)
                {
                    ViewBag.ResultErrorMessage = fs.ErrorMessage;
                    ViewBag.Pj_StuID = new SelectList(db.Students, "Stu_ID", "Stu_Title", project.Pj_StuID);
                    ViewBag.Pj_TeaID = new SelectList(db.Teachers, "Tea_ID", "Tea_Name", project.Pj_TeaID);
                    return View(project);
                }
                else
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var myUniqueFileName = DateTime.Now.Ticks + ".pdf";
                        string physicalPath = Server.MapPath("~/FileUploaded/" + myUniqueFileName);
                        file.SaveAs(physicalPath);
                        project.Pj_File = myUniqueFileName;
                    }
                    project.Pj_Date = DateTime.Today;
                    string[] Split_ID = project.Pj_StuID.Split(' ');
                    if (Session["PJ"] == null)
                    {
                        project.Pj_StuID = Split_ID[0];
                    }
                    db.Entry(project).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ViewBag.Pj_StuID = new SelectList(db.Students, "Stu_ID", "Stu_Title", project.Pj_StuID);
                ViewBag.Pj_TeaID = new SelectList(db.Teachers, "Tea_ID", "Tea_Name", project.Pj_TeaID);
                return View(project);
            }
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int id)
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
