using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aula146_SalesWebMvc.Models;

namespace Aula146_SalesWebMvc.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            // instanciar uma lista de Department chamada : list
            List<Department> list = new List<Department>();
            list.Add(new Department { Id = 1 ,Name = "Eletronics" });
            list.Add(new Department { Id = 2, Name = "Fashion" });

            return View(list);
        }
    }
}