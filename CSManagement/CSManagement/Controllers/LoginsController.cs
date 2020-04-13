using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages;
using CSManagement.Models;

namespace CSManagement.Controllers
{
    public class LoginsController : Controller
    {
        private CsManagementEntities db = new CsManagementEntities();

        // GET: Logins
        public ActionResult Index()
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            return View(db.Logins.ToList());
        }


        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (username.IsEmpty() == false && password.IsEmpty() == false)
            {
                var dataCk = db.Logins.FirstOrDefault(x => x.Log_ID == username && x.Log_Pass == password);
                if (dataCk != null && dataCk.Log_Role == "Student")
                {
                    var data = db.Students.FirstOrDefault(x => x.Stu_ID == dataCk.Log_ID);
                    Session["UserID"] = data.Stu_ID;
                    Session["UserName"] = data.Stu_Name;
                    Session["UserSurname"] = data.Stu_Surname;
                    Session["UserImg"] = data.Stu_Img;
                    if (data.Status.Status_Name == "กำลังศึกษาอยู่" ||
                        data.Status.Status_Name == "สำเร็จการศึกษา" ||
                        data.Status.Status_Name == "เกียรตินิยมอันดับ 1" ||
                        data.Status.Status_Name == "เกียรตินิยมอันดับ 2")
                    {
                        Session["PJ"] = "PJ";
                        Session["Stuend"] = data.Stu_ID;
                    }
                    Session["Loginchk"] = "set";
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else if (dataCk != null && dataCk.Log_Role == "Admin")
                {
                    var data = db.Teachers.FirstOrDefault(x => x.Tea_ID == dataCk.Log_ID);
                    Session["UserID"] = data.Tea_ID;
                    Session["UserName"] = data.Tea_Name;
                    Session["UserSurname"] = data.Tea_Surname;
                    Session["UserImg"] = data.Tea_Img;
                    Session["AJ"] = "AJ";
                    Session["Loginchk"] = "set";
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            else return Json(false, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Register(string username2, string password2, string repassword2, string specailcode)
        {
            if (username2.IsEmpty() == false && password2.IsEmpty() == false && password2 == repassword2)
            {
                var chkid = db.Logins.FirstOrDefault(x => x.Log_ID == username2);
                if (chkid == null)
                {
                    Login loginmodel = new Login();
                    loginmodel.Log_ID = username2;
                    loginmodel.Log_Pass = password2;
                    if (specailcode == "admin")
                    {
                        try
                        {
                            loginmodel.Log_Role = "Admin";
                            Teacher teachermodel = new Teacher()
                            {
                                Tea_ID = username2,
                                Tea_Name = "รอแก้ไข",
                                Tea_Img = "รอแก้ไข",
                                Tea_Birth = DateTime.Now,
                                Tea_Export = "รอแก้ไข",
                                Tea_LvEdu = "รอแก้ไข",
                                Tea_Position = "รอแก้ไข",
                                Tea_Program = "รอแก้ไข",
                                Tea_Surname = "รอแก้ไข",
                                Tea_TitleID = 1,
                                Tea_Tel = "999999999"
                            };
                            db.Logins.Add(loginmodel);
                            db.Teachers.Add(teachermodel);
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            return RedirectToAction("IndexUser", "Home");
                        }
                    }
                    else
                    {
                        loginmodel.Log_Role = "Student";
                        Student studentmodel = new Student()
                        {
                            Stu_ID = username2,
                            Stu_Img = "รอแก้ไข",
                            Stu_Sex = true,
                            Stu_Tel = "รอแก้ไข",
                            Stu_Address = "รอแก้ไข",
                            Stu_Title = 1,
                            Stu_Name = "รอแก้ไข",
                            Stu_OldEdu = "รอแก้ไข",
                            Stu_Email = "รอแก้ไข",
                            Stu_School = 1,
                            Stu_StatusID = 1,
                            Stu_Surname = "รอแก้ไข",
                            Stu_Birthday = DateTime.Now
                        };
                        db.Logins.Add(loginmodel);
                        db.Students.Add(studentmodel);
                        db.SaveChanges();
                    }
                }
                else return Json(false, JsonRequestBehavior.AllowGet);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        // GET: Logins/Details/5
        public ActionResult Details(string id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // GET: Logins/Create
        public ActionResult Create()
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Login login)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                db.Logins.Add(login);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(login);
        }

        // GET: Logins/Edit/5
        public ActionResult Edit(string id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Login login)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(login);
        }

        // GET: Logins/Delete/5
        public ActionResult Delete(string id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (Session["AJ"] == null) return RedirectToAction("Index", "Home");
            Login login = db.Logins.Find(id);
            db.Logins.Remove(login);
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
