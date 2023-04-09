﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpPost]
        public ActionResult CreateLayoutDashboard(string url,string wrapperClass)
        {
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