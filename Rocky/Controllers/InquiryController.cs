using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models;
using Rocky_Models.ViewModels;
using Rocky_Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rocky.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class InquiryController : Controller
    {
        public InquiryController(IInquiryDetailRepository inquiryDetailRepository, IInquiryHeaderRepository inquiryHeaderRepository)
        {
            _inquiryDetailRepository = inquiryDetailRepository ?? throw new ArgumentNullException(nameof(inquiryDetailRepository));
            _inquiryHeaderRepository = inquiryHeaderRepository ?? throw new ArgumentNullException(nameof(inquiryHeaderRepository));
        }

        /// <summary>
        /// Репозиторий деталей запроса
        /// </summary>
        private readonly IInquiryDetailRepository _inquiryDetailRepository;

        /// <summary>
        /// Репозиторий заголовков запроса
        /// </summary>
        private readonly IInquiryHeaderRepository _inquiryHeaderRepository;

        /// <summary>
        /// Модель запроса
        /// </summary>
        [BindProperty]
        public InquiryVM InquiryVM { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            InquiryVM = new InquiryVM()
            {
                InquiryHeader = _inquiryHeaderRepository.FirstOrDefault(filter: x => x.ID == id),
                InquiryDetails = _inquiryDetailRepository.GetAll(filter: x => x.InquiryHeaderID == id, includeProperties: "Product")
            };
            return View(InquiryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details()
        {
            InquiryVM.InquiryDetails = _inquiryDetailRepository.GetAll(filter: x => x.InquiryHeaderID == InquiryVM.InquiryHeader.ID);
            List<ShopingCart> shopingCarts = InquiryVM.InquiryDetails.Select(x => new ShopingCart() { ProductId = x.ProductID }).ToList();

            HttpContext.Session.Clear();
            HttpContext.Session.Set(WC.SessionCart, shopingCarts);
            HttpContext.Session.Set(WC.SessionInquiryID, InquiryVM.InquiryHeader.ID);

            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete()
        {
            InquiryHeader inquiryHeader = _inquiryHeaderRepository.Find(InquiryVM.InquiryHeader.ID);
            List<InquiryDetail> inquiryDetails = _inquiryDetailRepository.GetAll(x => x.InquiryHeaderID == InquiryVM.InquiryHeader.ID).ToList();

            _inquiryDetailRepository.RemoveRange(inquiryDetails);
            _inquiryHeaderRepository.Remove(inquiryHeader);
            _inquiryHeaderRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        #region

        [HttpGet]
        public IActionResult GetInquiryList()
        {
            return Json(new { data = _inquiryHeaderRepository.GetAll() });
        }

        #endregion
    }
}
