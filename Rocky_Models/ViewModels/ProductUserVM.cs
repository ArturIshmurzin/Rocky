using System.Collections.Generic;

namespace Rocky_Models.ViewModels
{
    /// <summary>
    /// Модель для страницы заказа
    /// </summary>
    public class ProductUserVM
    {
        public ProductUserVM()
        {
            this.ProductList = new List<Product>();
        }

        /// <summary>
        /// Пользователь
        /// </summary>
        public ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Список товаров
        /// </summary>
        public IList<Product> ProductList { get; set; }
    }
}
