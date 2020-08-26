using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel() { Departments = departments };
            return View(viewModel);
        }

        [HttpPost] //Declara que atenderá a requisição via post
        [ValidateAntiForgeryToken] //Protege contra ataques CSRF/XSRF
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); //Redireciona para a página index => Dessa forma tbm funciona : return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if(id.Value == 0)
            {
                return NotFound(); //Retorna um notfound para a tela
            }

            var seller = _sellerService.FindById(id.Value);

            if(seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
