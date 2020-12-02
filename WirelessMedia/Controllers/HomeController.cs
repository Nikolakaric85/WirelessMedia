using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WirelessMedia.Models;

namespace WirelessMedia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository productRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            this.productRepository = productRepository;
        }

        public IActionResult Index()
        {
            
            return View(productRepository.allProducts);
        }

        [HttpGet]
        public IActionResult AddEditProducts()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProducts(Products products)
        {
            productRepository.Add(products);
            return RedirectToAction("index","home");
        }

      
        public IActionResult EditProducts(int id)
        {
            var product = productRepository.allProducts.FirstOrDefault(a => a.Id == id);
            var model = new Products
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                Manufacturer = product.Manufacturer,
                Supplier = product.Supplier,
                Price = product.Price
            };

            return View("AddEditProducts", model);
        }


        [HttpPost]
        public IActionResult UpdateProduct(Products products, int id)
        {
            products.Id = id;
            productRepository.Update(products);
            return RedirectToAction("index", "home");
        }


        public IActionResult DeleteProduct(int id)
        {
            productRepository.Delete(id);
            return RedirectToAction("index", "home");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
