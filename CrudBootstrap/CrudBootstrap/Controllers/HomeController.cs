using CrudBootstrap.DB_Connect;
using CrudBootstrap.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CrudBootstrap.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:54704/");
        HttpClient Client;
        public HomeController()
        {
            Client = new HttpClient();
            Client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult Index(UserInfoModel userobj)
        {
            crudEntities1 obj = new crudEntities1();
            var UserRes = obj.user_info.Where(a => a.email == userobj.email).FirstOrDefault();

            if (UserRes == null)
            {
                TempData["Invalid"] = "Email not found or Invalid Username";
            }
            else
            {
                if(UserRes.email==userobj.email && UserRes.password == userobj.password)
                {
                    FormsAuthentication.SetAuthCookie(UserRes.email, false);

                    Session["username"] = UserRes.name;
                    Session["useremail"] = UserRes.email;
                    return RedirectToAction("IndexDashboard", "Home");
                }
                else
                {
                    TempData["Wrong"] = "Wrong Password Please Enter Valid Password";
                    return View();
                }
            }
            return View();
        }

        [Authorize]
        public ActionResult IndexDashBoard()
        {

            return View();
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        public ActionResult EmpTable()
        {
            //crudEntities1 obj = new crudEntities1();

            List<EmpModel> empobj = new List<EmpModel>();
            List<employee> empdatils = new List<employee>();

            HttpResponseMessage emp = Client.GetAsync(Client.BaseAddress + "Emp/EmpList").Result;
            if (emp.IsSuccessStatusCode)
            {
                string data = emp.Content.ReadAsStringAsync().Result;
                empdatils = JsonConvert.DeserializeObject<List<employee>>(data);
            }

           // var res = obj.employees.ToList();
            foreach (var item in empdatils)
            {
                empobj.Add(new EmpModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Mobile = item.Mobile,
                    Salary = item.Salary
                });
            }

            return View(empobj);
        }

        [HttpGet]
        public ActionResult UserReg()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UserReg(UserInfoModel useobj)
        {
            crudEntities1 obj = new crudEntities1();
            user_info infoobj = new user_info();
            infoobj.id = useobj.id;
            infoobj.name = useobj.name;
            infoobj.email = useobj.email;
            infoobj.password = useobj.password;

            obj.user_info.Add(infoobj);
            obj.SaveChanges();

            return RedirectToAction("Index", "Home");
            //return View();
        }











        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}