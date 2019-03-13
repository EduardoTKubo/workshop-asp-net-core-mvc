using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aula146_SalesWebMvc.Models;
using Aula146_SalesWebMvc.Models.ViewModels;
using Aula146_SalesWebMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aula146_SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        // declara uma dependencia para o SellerService
        private readonly SellerService _sellerService;

        // declara uma dependencia para o DepartmentService
        private readonly DepartmentService _departmentService;

        // construtor <> injecao de dependencia
        public SellersController (SellerService  sellerService ,DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }


        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View( list );
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }


    }
}