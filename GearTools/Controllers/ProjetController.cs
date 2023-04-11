using GearTools.Hubs;
using GearTools.Util;
using Microsoft.AspNet.SignalR;
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
                //Functions.SendProgress("Process in progress..." + DateTime.Now.Second, i, itemsCount);
                //Functions.SendMessage("SendMessage" + DateTime.Now.Second);
            }

            return Json("", JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult CreateLayoutDashboard(string baseUrl, string url,string wrapperClass)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressHub>();
            WebsiteCopier.Tools.FolderTmp = Server.MapPath("~/tmp");
            WebsiteCopier.Tools.FolderZip = Server.MapPath("~/Zip");
            WebsiteCopier.Tools.baseUrl = baseUrl; 
            WebsiteCopier.Tools.RemplirLayout(url, wrapperClass, hubContext); 
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}