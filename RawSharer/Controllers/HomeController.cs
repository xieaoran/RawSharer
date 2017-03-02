using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RawSharer.Models;
using RawSharer.ViewModels;

namespace RawSharer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Base()
        {
            return View();
        }
    }
}