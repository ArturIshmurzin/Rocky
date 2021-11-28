using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models;
using Rocky_Utility;
using System;
using System.Collections.Generic;

namespace Rocky.Controllers
{
    /// <summary>
    /// Контроллер категорий
    /// </summary>
    [Authorize(Roles = WC.AdminRole)]
    public class CategoryController : Controller
    {
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        /// <summary>
        /// Контекст подключения к базе данных
        /// </summary>
        private readonly ICategoryRepository _categoryRepository;

        public IActionResult Index()
        {
            IEnumerable<Category> categotyList = _categoryRepository.GetAll();
            return View(categotyList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                _categoryRepository.Save();

                return RedirectToAction("Index");
            }


            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Category category = _categoryRepository.Find(id.GetValueOrDefault());

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                _categoryRepository.Save();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Category category = _categoryRepository.Find(id.GetValueOrDefault());

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Category category = _categoryRepository.Find(id.GetValueOrDefault());

            if (category == null)
                return NotFound();

            _categoryRepository.Remove(category);
            _categoryRepository.Save();
            
            return RedirectToAction("Index");
        }
    }
}
