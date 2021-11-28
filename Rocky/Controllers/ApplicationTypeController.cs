using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models;
using Rocky_Utility;
using System;

namespace Rocky.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class ApplicationTypeController : Controller
    {
        public ApplicationTypeController(IApplicationTypeRepository applicationTypeRepository)
        {
            _applicationTypeRepository = applicationTypeRepository ?? throw new ArgumentNullException(nameof(applicationTypeRepository));
        }

        /// <summary>
        /// Репозиторий
        /// </summary>
        private readonly IApplicationTypeRepository _applicationTypeRepository;

        public IActionResult Index()
        {
            return View(_applicationTypeRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType applicationType)
        {
            if (ModelState.IsValid)
            {
                _applicationTypeRepository.Add(applicationType);
                _applicationTypeRepository.Save();
                return RedirectToAction("Index");
            }

            return View(applicationType);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            ApplicationType applicationType = _applicationTypeRepository.Find(id.GetValueOrDefault());

            if (applicationType == null)
                return NotFound();

            return View(applicationType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType applicationType)
        {
            if (ModelState.IsValid)
            {
                _applicationTypeRepository.Update(applicationType);
                _applicationTypeRepository.Save();
                return RedirectToAction("Index");
            }

            return View(applicationType);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            ApplicationType applicationType = _applicationTypeRepository.Find(id.GetValueOrDefault());

            if (applicationType == null)
                return NotFound();

            return View(applicationType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            ApplicationType applicationType = _applicationTypeRepository.Find(id.GetValueOrDefault());

            if (applicationType == null)
                return NotFound();

            _applicationTypeRepository.Remove(applicationType);
            _applicationTypeRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
