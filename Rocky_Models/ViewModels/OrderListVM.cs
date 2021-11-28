using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Rocky_Models.ViewModels
{
    /// <summary>
    /// Модель для страницы заказов
    /// </summary>
    public class OrderListVM
    {
        /// <summary>
        /// Заказы
        /// </summary>
        public IEnumerable<OrderHeader> OrderHeaders { get; set; }

        /// <summary>
        /// Статусы
        /// </summary>
        public IEnumerable<SelectListItem> StatusList{ get; set; }

        /// <summary>
        /// Текущий статус
        /// </summary>
        public string Status { get; set; }
    }
}
