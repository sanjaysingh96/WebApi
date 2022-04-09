using CrudBootstrap.DB_Connect;
using CrudBootstrap.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CrudBootstrap.Controllers
{
    public class EmpController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:54704/");
        HttpClient Client;
        public EmpController()
        {
            Client = new HttpClient();
            Client.BaseAddress = baseAddress;
        }

        [Authorize]
        public ActionResult Index()
        {
            crudEntities1 obj = new crudEntities1();

            List<EmpModel> empobj = new List<EmpModel>();

            var res = obj.employees.ToList();
            foreach (var item in res)
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
        [Authorize]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(EmpModel empobj)
        {

            // crudEntities1 obj = new crudEntities1();
            

            employee tblobj = new employee();
            tblobj.Id = empobj.Id;
            tblobj.Name = empobj.Name;
            tblobj.Email = empobj.Email;
            tblobj.Mobile = empobj.Mobile;
            tblobj.Salary = empobj.Salary;

            string data = JsonConvert.SerializeObject(tblobj);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage res = Client.PostAsync(Client.BaseAddress + "Emp/SavaEmployee", content).Result;

            if (res.IsSuccessStatusCode)
            {

            }

            return RedirectToAction("EmpTable","Home");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            crudEntities1 obj = new crudEntities1();
            var deleteitem = obj.employees.Where(b => b.Id == id).First();
            obj.employees.Remove(deleteitem);
            obj.SaveChanges();
            return RedirectToAction("EmpTable","Home");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            EmpModel empobj = new EmpModel();
            crudEntities1 obj = new crudEntities1();
            var edititem = obj.employees.Where(b => b.Id == id).First();
            empobj.Id = edititem.Id;
            empobj.Name = edititem.Name;
            empobj.Email = edititem.Email;
            empobj.Mobile = edititem.Mobile;
            empobj.Salary = edititem.Salary;


            ViewBag.id = edititem.Id;
            return View("Add", empobj);
        }
    }

}