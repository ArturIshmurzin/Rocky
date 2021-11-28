using System.Collections.Generic;

namespace Rocky_Models.ViewModels
{
    /// <summary>
    /// Модель заказа
    /// </summary>
    public class OrderVM
    {
        /// <summary>
        /// Заголовок заказа
        /// </summary>
        public OrderHeader InquiryHeader { get; set; }

        /// <summary>
        /// Детали заказа
        /// </summary>
        public IEnumerable<OrderDetail> InquiryDetails { get; set; }
    }
}
