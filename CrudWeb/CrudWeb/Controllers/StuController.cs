using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudWeb.Controllers
{
    public class StuController : Controller
    {
        // GET: Stu
        public ActionResult Index()
        {
            return View();
        }
    }
}