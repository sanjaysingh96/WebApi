using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudBootstrap.Models
{
    public class EmpModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int Salary { get; set; }
    }
}