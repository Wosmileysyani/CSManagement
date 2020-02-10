using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CSManagement.Models;

namespace CSManagement.Controllers
{
    public class NewsController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: News
        public async Task<ActionResult> Index()
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            return View(await db.News.ToListAsync());
        }

        [HttpGet]
        public ActionResult IndexUser(int id)
        {
            ViewBag.News = db.News.Where(x => x.New_ID == id).ToList();
            ViewBag.NewsCount = db.News.ToList();
            return View();
        }

        // GET: News/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            ViewBag.News = db.News.Where(x => x.New_ID == id).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(News news, HttpPostedFileBase file, string newdate)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var myUniqueFileName = DateTime.Now.Ticks + ".jpg";
                    string physicalPath = Server.MapPath("~/img/" + myUniqueFileName);
                    file.SaveAs(physicalPath);
                    news.New_Img = myUniqueFileName;
                }
                news.New_Date = DateTime.ParseExact(newdate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                db.News.Add(news);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(news);
            }
        }

        // GET: News/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(News news, HttpPostedFileBase file, string newdate)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var myUniqueFileName = DateTime.Now.Ticks + ".jpg";
                    string physicalPath = Server.MapPath("~/img/" + myUniqueFileName);
                    file.SaveAs(physicalPath);
                    news.New_Img = myUniqueFileName;
                }
                news.New_Date = DateTime.ParseExact(newdate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                db.Entry(news).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            News news = await db.News.FindAsync(id);
            db.News.Remove(news);
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
