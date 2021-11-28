using Braintree;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Rocky_DataAccess.Data;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models;
using Rocky_Models.ViewModels;
using Rocky_Utility;
using Rocky_Utility.BrainTree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rocky.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _mailSender;

        [BindProperty]
        public ProductUserVM ProductUserVM { get; set; }

        public CartController(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment, IEmailSender emailSender, IApplicationUserRepository applicationUserRepository,
            IInquiryHeaderRepository inquiryHeaderRepository, IInquiryDetailRepository inquiryDetailRepository,
            IOrderHeaderRepository orderHeaderRepository, IOrderDetailRepository orderDetailRepository,
            IBraneTreeGate braneTreeGate)
        {
            _orderHeaderRepository = orderHeaderRepository ?? throw new ArgumentNullException(nameof(orderHeaderRepository));
            _orderDetailRepository = orderDetailRepository ?? throw new ArgumentNullException(nameof(orderDetailRepository));
            _inquiryHeaderRepository = inquiryHeaderRepository ?? throw new ArgumentNullException(nameof(inquiryHeaderRepository));
            _inquiryDetailRepository = inquiryDetailRepository ?? throw new ArgumentNullException(nameof(inquiryDetailRepository));
            _applicationUserRepository = applicationUserRepository ?? throw new ArgumentNullException(nameof(applicationUserRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _webHostEnvironment = webHostEnvironment;
            _mailSender = emailSender;
            _brain = braneTreeGate;
        }

        private readonly IBraneTreeGate _brain;

        /// <summary>
        /// Репозиторий заголовка запроса
        /// </summary>
        private readonly IInquiryHeaderRepository _inquiryHeaderRepository;


        /// <summary>
        /// Репозиторий деталей запроса
        /// </summary>
        private readonly IInquiryDetailRepository _inquiryDetailRepository;

        /// <summary>
        /// Репозиторий заголовка заказа
        /// </summary>
        private readonly IOrderHeaderRepository _orderHeaderRepository;


        /// <summary>
        /// Репозиторий деталей заказа
        /// </summary>
        private readonly IOrderDetailRepository _orderDetailRepository;

        /// <summary>
        /// Репозиторий товаров
        /// </summary>
        private readonly IProductRepository _productRepository;


        /// <summary>
        /// Репозиторий пользователей
        /// </summary>
        private readonly IApplicationUserRepository _applicationUserRepository;

        public IActionResult Index()
        {
            List<ShopingCart> shopingCarts = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCart) ?? new();

            List<int> productsId = shopingCarts.Select(x => x.ProductId).ToList();

            List<Product> productList = _productRepository.GetAll(filter: x => productsId.Contains(x.Id)).ToList();

            foreach (var product in productList)
            {
                product.TempCount = shopingCarts.First(x => x.ProductId == product.Id).Count;
            }

            return View(productList);
            
        }

        public IActionResult Summary()
        {
            List<ShopingCart> shopingCarts = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCart) ?? new();

            List<int> productsId = shopingCarts.Select(x => x.ProductId).ToList();

            List<Product> productList = new();

            foreach (ShopingCart shopingCart in shopingCarts)
            {
                Product product = _productRepository.Find(shopingCart.ProductId);
                product.TempCount = shopingCart.Count;
                productList.Add(product);
            }

            this.ProductUserVM = new ProductUserVM()
            {
                ApplicationUser = this.GetInquiryUser(),
                ProductList = productList.ToList()
            };

            IBraintreeGateway braintreeGateway = _brain.GetGateway();
            string token = braintreeGateway.ClientToken.Generate();
            ViewBag.ClientToken = token;

            return View(ProductUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(IFormCollection collection, ProductUserVM productUserVM)
        {
            if (User.IsInRole(WC.AdminRole))
            {
                return this.CreateOrder(collection, productUserVM);
            }
            else
            {
                return await this.CreateInquiry(productUserVM);
            }
        }

        public IActionResult InquiryConfirmation(int id = 0)
        {
            OrderHeader orderHeader = _orderHeaderRepository.Find(id);
            HttpContext.Session.Clear();
            return View(orderHeader);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost(IEnumerable<Product> products)
        {
            this.UpdateCartSession(products);

            return RedirectToAction(nameof(Summary));
        }

        public IActionResult Remove(int id)
        {
            List<ShopingCart> shopingCarts = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCart) ?? new();

            shopingCarts.Remove(shopingCarts.FirstOrDefault(x => x.ProductId == id));

            HttpContext.Session.Set<List<ShopingCart>>(WC.SessionCart, shopingCarts);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCart(IEnumerable<Product> products)
        {
            this.UpdateCartSession(products);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Обновлении корзины
        /// </summary>
        /// <param name="products"></param>
        private void UpdateCartSession(IEnumerable<Product> products)
        {
            List<ShopingCart> shopingCarts = products.Select(x => new ShopingCart() { Count = x.TempCount, ProductId = x.Id }).ToList();

            HttpContext.Session.Set(WC.SessionCart, shopingCarts);
        }

        /// <summary>
        /// Получить пользователя для которого создается запрос
        /// </summary>
        /// <returns></returns>
        private ApplicationUser GetInquiryUser()
        {
            if (User.IsInRole(WC.AdminRole))
            {
                int inquiryID = HttpContext.Session.Get<int>(WC.SessionInquiryID);

                if (inquiryID == 0)
                {
                    return new ApplicationUser();
                }
                else
                {
                    InquiryHeader inquiryHeader = _inquiryHeaderRepository.FirstOrDefault(filter: x => x.ID == inquiryID);

                    return new ApplicationUser()
                    {
                        Email = inquiryHeader.Email,
                        PhoneNumber = inquiryHeader.PhoneNumber,
                        FullName = inquiryHeader.FullName
                    };
                }
            }
            else
            {
                var userID = User.FindFirst(ClaimTypes.NameIdentifier);
                return _applicationUserRepository.FirstOrDefault(filter: x => x.Id == userID.Value);
            }
        }

        /// <summary>
        /// Создание заявки на заказ
        /// </summary>
        private async Task<IActionResult> CreateInquiry(ProductUserVM productUserVM)
        {
            string pathToTemplate = _webHostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString() + "templates" + Path.DirectorySeparatorChar.ToString() + "Inquiry.html";
            string subject = "Новая заявка";
            string htmlBody = "";

            using (StreamReader sr = System.IO.File.OpenText(pathToTemplate))
            {
                htmlBody = sr.ReadToEnd();
            }

            StringBuilder stringBuilder = new();

            foreach (Product product in productUserVM.ProductList)
            {
                stringBuilder.Append($" - Название: {product.Name} <span style='font-size:14px;'> (ID : {product.Id})</span><br />");
            }

            string messageBody = string.Format(htmlBody,
                productUserVM.ApplicationUser.FullName,
                productUserVM.ApplicationUser.Email,
                productUserVM.ApplicationUser.PhoneNumber,
                stringBuilder.ToString());

            await _mailSender.SendEmailAsync(WC.AdminEmail, subject, messageBody);

            InquiryHeader inquiryHeader = new InquiryHeader()
            {
                ApplicationUserID = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value,
                Email = productUserVM.ApplicationUser.Email,
                FullName = productUserVM.ApplicationUser.FullName,
                PhoneNumber = productUserVM.ApplicationUser.PhoneNumber,
                InquiryDate = DateTime.Now
            };

            _inquiryHeaderRepository.Add(inquiryHeader);
            _inquiryHeaderRepository.Save();

            foreach (Product product in productUserVM.ProductList)
            {
                InquiryDetail inquiryDetail = new InquiryDetail()
                {
                    InquiryHeaderID = inquiryHeader.ID,
                    ProductID = product.Id
                };

                _inquiryDetailRepository.Add(inquiryDetail);
            }

            _inquiryDetailRepository.Save();

            return RedirectToAction(nameof(InquiryConfirmation));
        }

        /// <summary>
        /// Создание заказа
        /// </summary>
        private IActionResult CreateOrder(IFormCollection collection, ProductUserVM productUserVM)
        {
            OrderHeader orderHeader = new OrderHeader()
            {
                City = productUserVM.ApplicationUser.City,
                CreatedByUserId = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value,
                Email = productUserVM.ApplicationUser.Email,
                FinalOrderTotal = productUserVM.ProductList.Sum(x => x.TempCount*x.Price),
                FullName = productUserVM.ApplicationUser.FullName,
                OrderDate = DateTime.Now,
                State = productUserVM.ApplicationUser.State,
                PhoneNumber = productUserVM.ApplicationUser.PhoneNumber,
                StreetAddress = productUserVM.ApplicationUser.StreetAddress,
                PostalCode = productUserVM.ApplicationUser.PostalCode,
                OrderStatus = WC.OrderStatuses.Pending
            };

            _orderHeaderRepository.Add(orderHeader);
            _orderHeaderRepository.Save();

            foreach (Product product in productUserVM.ProductList)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderHeaderId = orderHeader.Id,
                    PricePerSqFt = product.Price,
                    Sqft = product.TempCount,
                    ProductId = product.Id
                };

                _orderDetailRepository.Add(orderDetail);
            }

            _inquiryDetailRepository.Save();

            this.PaymentOrder(collection, orderHeader);

            return RedirectToAction(nameof(InquiryConfirmation), new {id = orderHeader.Id });
        }

        /// <summary>
        /// Оплата заказа
        /// </summary>
        private void PaymentOrder(IFormCollection collection, OrderHeader orderHeader)
        {
            string nonceFromTheClient = collection["payment_method_nonce"];

            TransactionRequest request = new TransactionRequest
            {
                Amount = Convert.ToDecimal(orderHeader.FinalOrderTotal),
                PaymentMethodNonce = nonceFromTheClient,
                OrderId = orderHeader.Id.ToString(),
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            IBraintreeGateway braintreeGateway = _brain.GetGateway();
            Result<Transaction> result = braintreeGateway.Transaction.Sale(request);

            if (result.Target.ProcessorResponseText == "Approved")
            {
                orderHeader.TransactionId = result.Target.Id;
                orderHeader.OrderStatus = WC.OrderStatuses.Approved;
            }
            else
            {
                orderHeader.OrderStatus = WC.OrderStatuses.Canceled;
            }

            _orderHeaderRepository.Save();
        }
    }
}
