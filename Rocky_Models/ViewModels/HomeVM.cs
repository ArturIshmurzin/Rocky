using System.Collections.Generic;

namespace Rocky_Models.ViewModels
{
    /// <summary>
    /// Модель представления главной страницы
    /// </summary>
    public class HomeVM
    {
        /// <summary>
        /// Список товаров
        /// </summary>
        public IEnumerable<Product> Products { get; set; }

        /// <summary>
        /// Список категорий
        /// </summary>
        public IEnumerable<Category> Categories { get; set; }
    }
}
