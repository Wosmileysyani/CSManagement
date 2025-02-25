﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CSManagement.App_Start;
using CSManagement.Controllers;
using CSManagement.Models;
using NLog;

namespace CSManagement
{
    public class MvcApplication : HttpApplication
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private CsManagementEntities db = new CsManagementEntities();

        protected void Application_Start()
        {
            var hc = db.HitCounts.FirstOrDefault();
            var initialCount = hc.HitCount1;
            Application["Totaluser"] = initialCount;

            Log.Info("Starting up...");
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Log.Info("Routes and bundles registered");
            Log.Info("Started");
        }

        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }

        protected void Session_Start()
        {
            var hc = db.HitCounts.FirstOrDefault();
            var count = (int)Application["Totaluser"] + 1;
            Application.Lock();
            hc.HitCount1 = count;
            db.SaveChanges();
            Application["Totaluser"] = count;
            Session["Totaluser"] = (int)Application["Totaluser"];
            Application.UnLock();
        }

        //protected void Application_End()
        //{
        //    Log.Info("Stopped");
        //}

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    var exception = Server.GetLastError();
        //    Log.Error(exception, "Unhandled application exception");

        //    var httpContext = ((HttpApplication)sender).Context;
        //    httpContext.Response.Clear();
        //    httpContext.ClearError();

        //    if (new HttpRequestWrapper(httpContext.Request).IsAjaxRequest())
        //    {
        //        return;
        //    }

        //    ExecuteErrorController(httpContext, exception as HttpException);
        //}

        //private void ExecuteErrorController(HttpContext httpContext, HttpException exception)
        //{
        //    var routeData = new RouteData();
        //    routeData.Values["controller"] = "Error";

        //    if (exception != null && exception.GetHttpCode() == (int)HttpStatusCode.NotFound)
        //    {
        //        routeData.Values["action"] = "NotFound";
        //    }
        //    else
        //    {
        //        routeData.Values["action"] = "InternalServerError";
        //    }

        //    using (Controller controller = new ErrorController())
        //    {
        //        ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
        //    }
        //}
    }
}
