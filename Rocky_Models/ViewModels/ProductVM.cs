using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Rocky_Models.ViewModels
{
    /// <summary>
    /// Модель представления для товара
    /// </summary>
    public class ProductVM
    {
        /// <summary>
        /// Товар
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Список доступных категорий
        /// </summary>
        public IEnumerable<SelectListItem> CategorySelectList { get; set; }

        /// <summary>
        /// Список доступных типов приложения
        /// </summary>
        public IEnumerable<SelectListItem> ApplicationTypeSelectList { get; set; }
    }
}
