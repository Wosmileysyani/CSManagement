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
            return View(await db.News.Include(n => n.Status_News).ToListAsync());
        }

        public ActionResult IndexUser(int id = 0, int Type = 0)
        {
            var chk = db.Status_News.Where(x => x.StatusN_ID == Type).Select(x => x.StatusN_Name).FirstOrDefault();
            if (chk != null && chk.Equals("ข่าวทั้งหมด"))
            {
                Session.Remove("NewType");
                Type = 0;
            }
            if (Type == 0)
            {
                if (Session["NewType"] != null) Type = Convert.ToInt32(Session["NewType"]);
            }
            if (Type != 0)
            {
                ViewBag.News = db.News.Where(x => x.New_ID == id).ToList();
                ViewBag.NewsCount = db.News.Where(x => x.New_Active == true && x.New_Type == Type).OrderByDescending(x => x.New_DateStart);
                ViewBag.Type = new SelectList(db.Status_News, "StatusN_ID", "StatusN_Name");
                Session["NewType"] = Type;
            }
            else
            {
                ViewBag.News = db.News.Where(x => x.New_ID == id).ToList();
                ViewBag.NewsCount = db.News.Where(x => x.New_Active == true).OrderByDescending(x => x.New_DateStart);
                ViewBag.Type = new SelectList(db.Status_News, "StatusN_ID", "StatusN_Name");
                Session.Remove("NewType");
            }
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
            ViewBag.New_Type = new SelectList(db.Status_News, "StatusN_ID", "StatusN_Name");
            return View();
        }

        public void checkdatenew()
        {
            var data = db.News.ToList();
            ThaiBuddhistCalendar thai = new ThaiBuddhistCalendar();
            DateTime now = DateTime.Now;
            DateTime today = new DateTime(thai.GetYear(now), thai.GetMonth(now), now.Day);
            foreach (var item in data)
            {
                if (item.New_DateEnd != null && item.New_DateStart != null)
                {
                    var time = (item.New_DateEnd.Value.Ticks - today.Ticks);
                    if (time <= 0)
                    {
                        item.New_Active = false;
                    }
                }
            }
            db.SaveChanges();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(News news, HttpPostedFileBase file)
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
                var datestart = Request["New_DateStart"];
                var datestop = Request["New_DateEnd"];
                news.New_DateStart = DateTime.ParseExact(datestart, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                news.New_DateStart = news.New_DateStart.Value.AddYears(-543);
                news.New_DateEnd = DateTime.ParseExact(datestop, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                news.New_DateEnd = news.New_DateEnd.Value.AddYears(-543);
                news.New_Active = true;
                db.News.Add(news);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.New_Type = new SelectList(db.Status_News, "StatusN_ID", "StatusN_Name", news.New_Type);
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
            ViewBag.New_Type = new SelectList(db.Status_News, "StatusN_ID", "StatusN_Name", news.New_Type);
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(News news, HttpPostedFileBase file)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            try
            {
                var recordToUpdate = db.News.AsNoTracking().Single(x => x.New_ID == news.New_ID);
                if (file != null && file.ContentLength > 0)
                {
                    var myUniqueFileName = DateTime.Now.Ticks + ".jpg";
                    string physicalPath = Server.MapPath("~/img/" + myUniqueFileName);
                    file.SaveAs(physicalPath);
                    news.New_Img = myUniqueFileName;
                }
                else
                {
                    news.New_Img = recordToUpdate.New_Img;
                }
                var datestart = Request["New_DateStart"];
                var datestop = Request["New_DateEnd"];
                news.New_DateStart = DateTime.ParseExact(datestart, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                news.New_DateStart = news.New_DateStart.Value.AddYears(-543);
                news.New_DateEnd = DateTime.ParseExact(datestop, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                news.New_DateEnd = news.New_DateEnd.Value.AddYears(-543);
                db.Entry(news).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                ViewBag.New_Type = new SelectList(db.Status_News, "StatusN_ID", "StatusN_Name", news.New_Type);
                return View(news);
            }

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
