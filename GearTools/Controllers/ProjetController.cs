using GearTools.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace GearTools.Controllers
{
    public class ProjetController : Controller
    {

        // GET: Projet
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateLayoutDashboard()
        {
            return View();
        }
        public JsonResult LongRunningProcess()
        {
            //THIS COULD BE SOME LIST OF DATA
            int itemsCount = 100;

            for (int i = 0; i <= itemsCount; i++)
            {
                //SIMULATING SOME TASK
                Thread.Sleep(500);

                //CALLING A FUNCTION THAT CALCULATES PERCENTAGE AND SENDS THE DATA TO THE CLIENT
                Functions.SendProgress("Process in progress..." +DateTime.Now.Second, i, itemsCount);
            }

            return Json("", JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult CreateLayoutDashboard(string baseUrl, string url,string wrapperClass)
        {
            WebsiteCopier.Tools.FolderTmp = Server.MapPath("~/tmp");
            WebsiteCopier.Tools.baseUrl = baseUrl;
            var result = WebsiteCopier.Tools.RemplirLayout(url, wrapperClass);
            ViewBag.Layout = result.Layout;
            ViewBag.Nav = result.Nav;
            ViewBag.Header = result.Header;
            ViewBag.Script = result.Script;
            ViewBag.Footer = result.Footer;
            return View();
        }
    }
}