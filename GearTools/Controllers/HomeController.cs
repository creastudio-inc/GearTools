using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace GearTools.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //string url = "https://themes.laborator.co/xenon/forms/native/"; System.Collections.Immutable, 
            //var result = WebsiteCopier.Tools.RemplirText(url);
         //   var demo = AnalyticsCode.CsharpClassParser.Parse("C:\\Users\\LaptopProBook\\source\\repos\\GearTools\\GearTools\\Models\\Student.cs");
           

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}