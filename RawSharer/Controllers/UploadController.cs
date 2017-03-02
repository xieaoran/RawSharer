using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RawSharer.Controllers
{
    public class UploadController : Controller
    {
        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult Template()
        {
            return View();
        }
    }
}