using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using CSManagement.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CSManagement.Controllers
{
    public class Short_CourseController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: Short_Course
        public ActionResult Index()
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            return View(db.Short_Course.ToList());
        }

        public ActionResult Report_Index(int SC = 0)
        {
            var data = db.View_SC.Where(x => x.Gen_NO == SC && x.APP_Status == 1).Select(x => new ViewModelSC()
            {
                SC_NameTH = x.SC_NameTH,
                Gen_Name = x.Gen_Name,
                Gen_Date = x.Gen_Date,
                REG_Name = x.REG_Name,
                REG_Email = x.REG_Email,
                REG_Tel = x.REG_Tel,
                APP_Date = x.APP_Date
            });
            ViewBag.SC = new SelectList(db.View_SC_Substring, "Gen_NO", "SC_NameTH");
            Session["ViewSCID"] = SC;
            return View(data);
        }

        public ActionResult ExportToExcel(int SC = 0)
        {
            try
            {
                var data = db.View_SC.Where(x => x.Gen_NO == SC && x.APP_Status == 1).Select(x => new ViewModelSC()
                {
                    SC_NameTH = x.SC_NameTH,
                    Gen_Name = x.Gen_Name,
                    Gen_Date = x.Gen_Date,
                    REG_Name = x.REG_Name,
                    REG_Email = x.REG_Email,
                    REG_Tel = x.REG_Tel
                });
                var find = db.View_SC.FirstOrDefault(x => x.Gen_NO == SC);

                ExcelPackage pck = new ExcelPackage();
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("รายงาน");
                using (ExcelRange Rng = ws.Cells["a1:f5"])
                {
                    ws.Cells["a1:f1"].Merge = true;
                    ws.Cells["a1"].Value = "รายชื่อผู้เข้าอบรม";
                    ws.Cells["a2"].Value = "หลักสูตร";
                    ws.Cells["b2:f2"].Merge = true;
                    ws.Cells["b2"].Value = find.SC_NameTH;
                    ws.Cells["a3"].Value = "รุ่น";
                    ws.Cells["b3"].Value = find.Gen_Name;
                    ws.Cells["c3"].Value = "วันที่";
                    ws.Cells["d3:f3"].Merge = true;
                    ws.Cells["d3"].Value = find.Gen_Date;
                    ws.Cells["a4:f4"].Merge = true;
                    ws.Cells["a5"].Value = "ลำดับที่";
                    ws.Cells["b5:c5"].Merge = true;
                    ws.Cells["b5"].Value = "ชื่อ-นามสกุล";
                    ws.Cells["d5"].Value = "เบอร์โทร";
                    ws.Cells["e5"].Value = "อีเมลล์";
                    ws.Cells["f5"].Value = "";
                    Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }
                using (ExcelRange Rng = ws.Cells["a6:f100"])
                {
                    var startindex = 6;
                    foreach (var item in data)
                    {
                        var name = item.REG_Name;
                        var tel = item.REG_Tel;
                        var email = item.REG_Email;
                        ws.Cells["a" + startindex].Value = startindex - 5;
                        ws.Cells["b" + startindex + ":c" + startindex].Merge = true;
                        ws.Cells["b" + startindex].Value = name;
                        ws.Cells["d" + startindex].Value = tel;
                        ws.Cells["e" + startindex].Value = email;
                        ws.Cells["f" + startindex].Value = "";
                        startindex += 1;
                    }
                    Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }
                ws.Cells["a:fz"].AutoFitColumns();
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment: filename=" + find.SC_NameTH + ".xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
                return RedirectToAction("Report_Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Report_Index");
            }
        }

        // GET: Short_Course/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Short_Course short_Course = db.Short_Course.Find(id);
            if (short_Course == null)
            {
                return HttpNotFound();
            }
            return View(short_Course);
        }

        // GET: Short_Course/Create
        public ActionResult Create()
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            return View();
        }

        // POST: Short_Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Short_Course short_Course)
        {
            if (ModelState.IsValid)
            {
                db.Short_Course.Add(short_Course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(short_Course);
        }

        // GET: Short_Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Short_Course short_Course = db.Short_Course.Find(id);
            if (short_Course == null)
            {
                return HttpNotFound();
            }
            return View(short_Course);
        }

        // POST: Short_Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SC_ID,SC_NameTH")] Short_Course short_Course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(short_Course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(short_Course);
        }

        // GET: Short_Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            Short_Course short_Course = db.Short_Course.Find(id);
            db.Short_Course.Remove(short_Course);
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
