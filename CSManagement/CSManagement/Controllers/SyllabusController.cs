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
            return View(await db.Syllabus.ToListAsync());
        }

        // GET: Syllabus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
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
            return View();
        }

        // POST: Syllabus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Syllabu syllabu, HttpPostedFileBase file)
        {
            try
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

        // POST: Syllabus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Syllabu syllabu, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var myUniqueFileName = DateTime.Now.Ticks + ".jpg";
                    string physicalPath = Server.MapPath("~/img/" + myUniqueFileName);
                    file.SaveAs(physicalPath);
                    syllabu.Sy_Img = myUniqueFileName;
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
