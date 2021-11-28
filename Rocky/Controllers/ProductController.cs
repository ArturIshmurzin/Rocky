using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rocky_DataAccess.Data;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models;
using Rocky_Models.ViewModels;
using Rocky_Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rocky.Controllers
{
    /// <summary>
    /// Контроллер товаров
    /// </summary>
    [Authorize(Roles = WC.AdminRole)]
    public class ProductController : Controller
    {
        public ProductController(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _webHostEnvironment = webHostEnvironment;
        }

        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// Контекст подключения к базе данных
        /// </summary>
        private readonly IProductRepository _productRepository;

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _productRepository.GetAll(includeProperties: "Category,ApplicationType");

            return View(productList);
        }

        [HttpGet]
        public IActionResult CreateOrUpdate(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new Product(),
                CategorySelectList = _productRepository.GetAllDropdownList(WC.CategoryName),
                ApplicationTypeSelectList = _productRepository.GetAllDropdownList(WC.ApplicationTypeName)
            };

            if (id == null)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _productRepository.Find(id.GetValueOrDefault());
                if (productVM.Product == null)
                    return NotFound();

                return View(productVM);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrUpdate(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                Product newProduct;
                if (productVM.Product.Id == 0)
                {
                    newProduct = this.CreateProduct(productVM);
                    _productRepository.Add(newProduct);
                    _productRepository.Save();
                }
                else
                    newProduct = this.UpdateProduct(productVM);

                _productRepository.Update(newProduct);
                _productRepository.Save();

                return RedirectToAction("Index");
            }

            productVM.CategorySelectList = _productRepository.GetAllDropdownList(WC.CategoryName);
            productVM.ApplicationTypeSelectList = _productRepository.GetAllDropdownList(WC.ApplicationTypeName);

            return View(productVM);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Product product = _productRepository.FirstOrDefault(filter: x => x.Id == id, includeProperties: $"{WC.CategoryName},{WC.ApplicationTypeName}");

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Product product = _productRepository.Find(id.GetValueOrDefault());

            if (product == null)
                return NotFound();

            string imagePath = _webHostEnvironment.WebRootPath + WC.ProductImagePath;
            string filePath = Path.Combine(imagePath, product.Image);

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            _productRepository.Remove(product);
            _productRepository.Save();
            
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Создание товара
        /// </summary>
        private Product CreateProduct(ProductVM productVM)
        {
            IFormFileCollection files = HttpContext.Request.Form.Files;

            string imagePath = _webHostEnvironment.WebRootPath + WC.ProductImagePath;
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(files[0].FileName);

            using (FileStream fileStream = new(Path.Combine(imagePath, fileName + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }

            productVM.Product.Image = fileName + extension;

            return productVM.Product;
        }

        /// <summary>
        /// Обновление
        /// </summary>
        private Product UpdateProduct(ProductVM productVM)
        {
            IFormFileCollection files = HttpContext.Request.Form.Files;

            Product currentProduct = _productRepository.FirstOrDefault(filter: x => x.Id == productVM.Product.Id, isTracking: false);

            if (files.Count > 0)
            {
                string imagePath = _webHostEnvironment.WebRootPath + WC.ProductImagePath;
                string oldFilePath = Path.Combine(imagePath, currentProduct.Image);

                if (System.IO.File.Exists(oldFilePath))
                    System.IO.File.Delete(oldFilePath);

                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);

                using (FileStream fileStream = new(Path.Combine(imagePath, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                productVM.Product.Image = fileName + extension;
            }
            else
            {
                productVM.Product.Image = currentProduct.Image;
            }

            return productVM.Product;
        }
    }
}
