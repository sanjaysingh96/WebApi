using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.DB_Connect;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmpController : ApiController
    {
        [HttpGet]
        [Route("Emp/EmpList")]
        public List<EmpModel> AddEmp()
        {
            List<EmpModel> listemp = new List<EmpModel>();

            crudEntities db = new crudEntities();
            var res = db.employees.ToList();
            foreach (var item in res)
            {
                listemp.Add(new EmpModel
                {
                    Id=item.Id,
                    Name=item.Name,
                    Email=item.Email,
                    Mobile=item.Mobile,
                    Salary=item.Salary
                });
            }
            return listemp;
        }

        [HttpPost]
        [Route("Emp/SavaEmployee")]
        public HttpResponseMessage SavaEmp(employee obj)
        {
            crudEntities db = new crudEntities();
            employee tblobj = new employee();

            if (obj.Id == 0)
            {
                db.employees.Add(obj);
                db.SaveChanges();
            }
            else
            {
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            HttpResponseMessage res = new HttpResponseMessage(HttpStatusCode.OK);

            return res;
        }


    }
}