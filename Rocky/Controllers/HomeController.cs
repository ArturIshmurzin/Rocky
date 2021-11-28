using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models;
using Rocky_Models.ViewModels;
using Rocky_Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Rocky.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger;
        }

        /// <summary>
        /// Репозиторий категорий
        /// </summary>
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// Репозиторий товаров
        /// </summary>
        private readonly IProductRepository _productRepository;

        public IActionResult Index()
        {
            HomeVM homeVM = new()
            {
                Categories = _categoryRepository.GetAll(),
                Products = _productRepository.GetAll(includeProperties: "Category,ApplicationType")
            };
            return View(homeVM);
        }

        public IActionResult Details(int id)
        {
            List<ShopingCart> shopingCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCart) ?? new();

            DetailsVM detailsVM = new()
            {
                Product = _productRepository.FirstOrDefault(filter: x => x.Id == id, includeProperties: "Category,ApplicationType"),
                ExistInCart = shopingCartList.Any(x => x.ProductId == id)
            };

            return View(detailsVM);
        }

        [HttpPost, ActionName("Details")]
        public IActionResult DetailsPost(int id, DetailsVM detailsVM)
        {
            List<ShopingCart> shopingCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCart) ?? new();
            shopingCartList.Add(new ShopingCart()
            {
                ProductId = id,
                Count = detailsVM.Product.TempCount
            });

            HttpContext.Session.Set(WC.SessionCart, shopingCartList);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int id)
        {
            List<ShopingCart> shopingCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCart) ?? new();
            ShopingCart shopingCart = shopingCartList.FirstOrDefault(x => x.ProductId == id);
            if (shopingCart != null)
            {
                shopingCartList.Remove(shopingCart);
                HttpContext.Session.Set(WC.SessionCart, shopingCartList);
            }
            return RedirectToAction(nameof(Index));
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
