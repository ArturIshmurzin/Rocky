using Microsoft.AspNetCore.Mvc.Rendering;
using Rocky_Models;
using System.Collections.Generic;

namespace Rocky_DataAccess.Repository.IRepository
{
    /// <summary>
    /// Интерфейс репозитория категории
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Обновить товар
        /// </summary>
        void Update(Product category);

        /// <summary>
        /// Получить элементы выпадающего списка
        /// </summary>
        /// <param name="prorertyName">название свойства элементов которые необходимо получить</param>
        IEnumerable<SelectListItem> GetAllDropdownList(string prorertyName);
    }
}
