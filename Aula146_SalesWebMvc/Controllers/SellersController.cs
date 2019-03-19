using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Aula146_SalesWebMvc.Models;
using Aula146_SalesWebMvc.Models.ViewModels;
using Aula146_SalesWebMvc.Services;
using Aula146_SalesWebMvc.Services.Exceptions;
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


        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View( list );
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            // se o modelo nao foi validado retornar para conserto - acontece enquanto o usuario n preencher direito o formulario
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if ( id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIDAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete (int id)
        {
            try
            { 
                await _sellerService.RemoveAsync(id);       // faz a deleção
                return RedirectToAction(nameof(Index));     // retorna a tela inicial
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }


        public async Task<IActionResult> Details (int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error) ,new {message = "Id not provided"});
            }

            var obj = await _sellerService.FindByIDAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);

        }


        public async Task<IActionResult> Edit (int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIDAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id ,Seller seller)
        {
            // se o modelo nao foi validado retornar para conserto - acontece enquanto o usuario n preencher direito o formulario
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismath" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }

            //catch (NotFoundException e)
            //{
            //    return RedirectToAction(nameof(Error), new { message = e.Message });
            //}
            //catch (DbConcurrencyException e)
            //{
            //    return RedirectToAction(nameof(Error), new { message = e.Message });
            //}

            // em substituição as 2 de cima 
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        public IActionResult Error (string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                // "macete" do framework para pegar o Id interno da requisição
            };
            return View(viewModel);
        }
    }
}