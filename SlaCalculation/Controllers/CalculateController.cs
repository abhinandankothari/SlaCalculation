using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SlaCalculation.Controllers
{
    public class CalculateController : Controller
    {
        //
        // GET: /Calculate/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FinalDisplay()
        {
            return View();
        }
    }
}
