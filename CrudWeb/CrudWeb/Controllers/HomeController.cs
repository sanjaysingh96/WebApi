using CrudWeb.DB_connect;
using CrudWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<studetail> stuobj = new List<studetail>();
            crudEntities obj = new crudEntities();
            var res = obj.students.ToList();
            foreach (var item in res)
            {
                stuobj.Add(new studetail
                {
                    id=item.id,
                    name=item.name,
                    email=item.email
                });
            }

            return View(stuobj);
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