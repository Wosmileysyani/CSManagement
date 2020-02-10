using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CSManagement.Models;

namespace CSManagement.Controllers
{
    public class SyllabusController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: Syllabus
        public async Task<ActionResult> Index()
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            return View(await db.Syllabus.ToListAsync());
        }

        // GET: Syllabus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Syllabu syllabu = await db.Syllabus.FindAsync(id);
            if (syllabu == null)
            {
                return HttpNotFound();
            }
            return View(syllabu);
        }

        // GET: Syllabus/Create
        public ActionResult Create()
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Syllabu syllabu, HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var myUniqueFileName = DateTime.Now.Ticks + ".jpg";
                    string physicalPath = Server.MapPath("~/img/" + myUniqueFileName);
                    file.SaveAs(physicalPath);
                    syllabu.Sy_Img = myUniqueFileName;
                }
                db.Syllabus.Add(syllabu);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(syllabu);
            }
        }

        // GET: Syllabus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Syllabu syllabu = await db.Syllabus.FindAsync(id);
            if (syllabu == null)
            {
                return HttpNotFound();
            }
            return View(syllabu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Syllabu syllabu, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var recordToUpdate = db.Syllabus.AsNoTracking().Single(x => x.Sy_ID == syllabu.Sy_ID);
                if (file != null && file.ContentLength > 0)
                {
                    var myUniqueFileName = DateTime.Now.Ticks + ".jpg";
                    string physicalPath = Server.MapPath("~/img/" + myUniqueFileName);
                    file.SaveAs(physicalPath);
                    syllabu.Sy_Img = myUniqueFileName;
                }
                else
                {
                    syllabu.Sy_Img = recordToUpdate.Sy_Img;
                }
                db.Entry(syllabu).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(syllabu);
        }

        // GET: Syllabus/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            Syllabu syllabu = await db.Syllabus.FindAsync(id);
            db.Syllabus.Remove(syllabu);
            await db.SaveChangesAsync();
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
