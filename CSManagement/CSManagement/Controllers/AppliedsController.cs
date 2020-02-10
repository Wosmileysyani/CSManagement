using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mail;
using System.Web.Mvc;
using System.Web.Services;
using CSManagement.Models;
using MailMessage = System.Net.Mail.MailMessage;

namespace CSManagement.Controllers
{
    public class AppliedsController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        public ActionResult Index()
        {
            var applieds = db.Applieds.Include(a => a.Generation).Include(a => a.Register_SC);
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            return View(applieds.ToList());
        }

        public ActionResult AppAgree(string id = "")
        {
            var findidall = db.Applieds.Where(x => x.APP_ReNO == id).ToList();
            int i = 0;
            foreach (var item in findidall)
            {
                if (i > 0)
                {
                    db.Applieds.Remove(item);
                }
                else
                {
                    item.APP_Status = 1;
                }
                i++;
            }
            db.SaveChanges();
            var findid = db.Applieds.FirstOrDefault(x => x.APP_ReNO == id);
            db.Entry(findid).State = EntityState.Modified;
            var findGen = db.Generations.FirstOrDefault(x => x.Gen_NO == findid.Generation.Gen_NO);
            if (findGen.Gen_Member > 0)
            {
                findGen.Gen_Member -= 1;
                if (findGen.Gen_Member <= 0) findGen.Gen_Status = 2;
            }
            else if (findGen.Gen_Member == 0)
            {
                if (findGen.Gen_Member <= 0) findGen.Gen_Status = 2;
            }
            else
            {
                //แสดงว่าคนเต็มแล้ว
            }
            db.Entry(findGen).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public JsonResult SendMailToUser(string id, int idg)
        {
            bool Result = false;

            var user = db.Register_SC.FirstOrDefault(x => x.REG_IDCard == id);
            var text = db.Generations.FirstOrDefault(x => x.Gen_NO == idg);
            Result = SendEmail(user.REG_Email, "ตอบกลับอัตโนมัติจากการสมัครคอร์สสั้น", text.Gen_TextForMail);

            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        public bool SendEmail(string toEmail, string subject, string emailBody)
        {
            try
            {
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 465)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("kruautoemail@gmail.com", "ef44ecc3"),
                    DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
                };

                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, emailBody);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = Encoding.UTF8;
                client.Send(mailMessage);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ActionResult AppCancle(string id = "")
        {
            var findidall = db.Applieds.Where(x => x.APP_ReNO == id).ToList();
            int i = 0;
            foreach (var item in findidall)
            {
                if (i > 0)
                {
                    db.Applieds.Remove(item);
                }
                else
                {
                    item.APP_Status = 2;
                }
                i++;
            }
            var findid = db.Applieds.FirstOrDefault(x => x.APP_ReNO == id);
            findid.APP_Status = 2;
            db.Entry(findid).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Applieds/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applied applied = db.Applieds.Find(id);
            if (applied == null)
            {
                return HttpNotFound();
            }
            return View(applied);
        }

        // GET: Applieds/Create
        public ActionResult Create()
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            ViewBag.APP_GenNO = new SelectList(db.Generations, "Gen_NO", "Gen_Name");
            ViewBag.APP_ReNO = new SelectList(db.Register_SC, "REG_IDCard", "REG_Name");
            return View();
        }

        // POST: Applieds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Applied applied)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                db.Applieds.Add(applied);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.APP_GenNO = new SelectList(db.Generations, "Gen_NO", "Gen_Name", applied.APP_GenNO);
            ViewBag.APP_ReNO = new SelectList(db.Register_SC, "REG_IDCard", "REG_Name", applied.APP_ReNO);
            return View(applied);
        }

        // GET: Applieds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applied applied = db.Applieds.Find(id);
            if (applied == null)
            {
                return HttpNotFound();
            }
            ViewBag.APP_GenNO = new SelectList(db.Generations, "Gen_NO", "Gen_Name", applied.APP_GenNO);
            ViewBag.APP_ReNO = new SelectList(db.Register_SC, "REG_IDCard", "REG_Name", applied.APP_ReNO);
            return View(applied);
        }

        // POST: Applieds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "APP_NO,APP_ReNO,APP_GenNO,APP_Status")] Applied applied)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                db.Entry(applied).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.APP_GenNO = new SelectList(db.Generations, "Gen_NO", "Gen_Name", applied.APP_GenNO);
            ViewBag.APP_ReNO = new SelectList(db.Register_SC, "REG_IDCard", "REG_Name", applied.APP_ReNO);
            return View(applied);
        }

        // GET: Applieds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applied applied = db.Applieds.Find(id);
            if (applied == null)
            {
                return HttpNotFound();
            }
            return View(applied);
        }

        // POST: Applieds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            Applied applied = db.Applieds.Find(id);
            db.Applieds.Remove(applied);
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
