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
    public class Department_SupController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: Department_Sup
        public async Task<ActionResult> Index()
        {
            return View(await db.Department_Sup.ToListAsync());
        }

        // GET: Department_Sup/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department_Sup department_Sup = await db.Department_Sup.FindAsync(id);
            if (department_Sup == null)
            {
                return HttpNotFound();
            }
            return View(department_Sup);
        }

        // GET: Department_Sup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Department_Sup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Deps_ID,Deps_Name")] Department_Sup department_Sup)
        {
            if (ModelState.IsValid)
            {
                db.Department_Sup.Add(department_Sup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(department_Sup);
        }

        // GET: Department_Sup/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department_Sup department_Sup = await db.Department_Sup.FindAsync(id);
            if (department_Sup == null)
            {
                return HttpNotFound();
            }
            return View(department_Sup);
        }

        // POST: Department_Sup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Deps_ID,Deps_Name")] Department_Sup department_Sup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department_Sup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(department_Sup);
        }

        // GET: Department_Sup/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department_Sup department_Sup = await db.Department_Sup.FindAsync(id);
            if (department_Sup == null)
            {
                return HttpNotFound();
            }
            return View(department_Sup);
        }

        // POST: Department_Sup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Department_Sup department_Sup = await db.Department_Sup.FindAsync(id);
            db.Department_Sup.Remove(department_Sup);
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
