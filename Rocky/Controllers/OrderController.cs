using Braintree;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models;
using Rocky_Models.ViewModels;
using Rocky_Utility;
using Rocky_Utility.BrainTree;
using System;
using System.Linq;

namespace Rocky.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class OrderController : Controller
    {
        public OrderController(IOrderHeaderRepository orderHeaderRepository, IOrderDetailRepository orderDetailRepository,
            IBraneTreeGate braneTreeGate)
        {
            _orderHeaderRepository = orderHeaderRepository ?? throw new ArgumentNullException(nameof(orderHeaderRepository));
            _orderDetailRepository = orderDetailRepository ?? throw new ArgumentNullException(nameof(orderDetailRepository));
            _brain = braneTreeGate;
        }

        private readonly IBraneTreeGate _brain;

        /// <summary>
        /// Репозиторий заголовка заказа
        /// </summary>
        private readonly IOrderHeaderRepository _orderHeaderRepository;


        /// <summary>
        /// Репозиторий деталей заказа
        /// </summary>
        private readonly IOrderDetailRepository _orderDetailRepository;

        [BindProperty]
        public OrderVM OrderVM { get; set; }

        public IActionResult Index(string searchName = null, string searchEmail = null, string searchPhone = null, string Status=null)
        {
            OrderListVM orderListVM = new OrderListVM()
            {
                OrderHeaders = _orderHeaderRepository.GetAll(filter: x => (string.IsNullOrEmpty(searchName) || x.FullName.ToLower().Contains(searchName.ToLower())) 
                && (string.IsNullOrEmpty(searchEmail) || x.Email.ToLower().Contains(searchEmail.ToLower()))
                && (string.IsNullOrEmpty(searchPhone) || x.PhoneNumber.ToLower().Contains(searchPhone.ToLower()))
                && (string.IsNullOrEmpty(Status) || Status == "--Статус заказа--" || x.OrderStatus.ToLower().Contains(Status.ToLower()))),
                StatusList = WC.OrderStatuses.StatusList.ToList().Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Text = x, Value = x }),

            };

            return View(orderListVM);
        }

        public IActionResult Details (int id)
        {
            this.OrderVM = new OrderVM()
            {
                InquiryHeader = _orderHeaderRepository.Find(id),
                InquiryDetails = _orderDetailRepository.GetAll(x => x.OrderHeaderId == id, includeProperties: "Product")
            };

            return View(this.OrderVM);
        }

        [HttpPost]
        public IActionResult StartProcessing()
        {
            OrderHeader orderHeader = _orderHeaderRepository.Find(this.OrderVM.InquiryHeader.Id);
            orderHeader.OrderStatus = WC.OrderStatuses.Processing;
            _orderHeaderRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult CancelOrder()
        {
            OrderHeader orderHeader = _orderHeaderRepository.Find(this.OrderVM.InquiryHeader.Id);

            IBraintreeGateway brainTreeGate = _brain.GetGateway();
            Transaction transaction = brainTreeGate.Transaction.Find(orderHeader.TransactionId);

            if (transaction.Status == TransactionStatus.AUTHORIZED || transaction.Status == TransactionStatus.SUBMITTED_FOR_SETTLEMENT)
            {
                Result<Transaction> resultvoid = brainTreeGate.Transaction.Void(orderHeader.TransactionId);
            }
            else
            {
                Result<Transaction> resultRefund = brainTreeGate.Transaction.Refund(orderHeader.TransactionId);
            }

            orderHeader.OrderStatus = WC.OrderStatuses.Refunded;
            _orderHeaderRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ShipOrder()
        {
            OrderHeader orderHeader = _orderHeaderRepository.Find(this.OrderVM.InquiryHeader.Id);

            orderHeader.OrderStatus = WC.OrderStatuses.Shipped;
            orderHeader.ShippingDate = DateTime.Now;
            _orderHeaderRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult UpdateOrderDetails()
        {
            OrderHeader orderHeaderFromDB = _orderHeaderRepository.Find(this.OrderVM.InquiryHeader.Id);
            orderHeaderFromDB.FullName = this.OrderVM.InquiryHeader.FullName;
            orderHeaderFromDB.PhoneNumber = this.OrderVM.InquiryHeader.PhoneNumber;
            orderHeaderFromDB.StreetAddress = this.OrderVM.InquiryHeader.StreetAddress;
            orderHeaderFromDB.City = this.OrderVM.InquiryHeader.City;
            orderHeaderFromDB.State = this.OrderVM.InquiryHeader.State;
            orderHeaderFromDB.PostalCode = this.OrderVM.InquiryHeader.PostalCode;
            orderHeaderFromDB.Email = this.OrderVM.InquiryHeader.Email;

            _orderHeaderRepository.Save();

            return RedirectToAction(nameof(Details), new { id = orderHeaderFromDB.Id});
        }
    }
}
