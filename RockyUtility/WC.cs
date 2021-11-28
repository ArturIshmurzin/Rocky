using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Rocky_Utility
{
    /// <summary>
    /// Константы
    /// </summary>
    public static class WC
    {
        /// <summary>
        /// Путь к изображениям продуктов
        /// </summary>
        public const string ProductImagePath = @"\images\product\";

        /// <summary>
        /// Название сесии для корзин
        /// </summary>
        public const string SessionCart = "ShopingCartSession";

        /// <summary>
        /// Идентификатор запроса для сессии
        /// </summary>
        public const string SessionInquiryID = "InquirySession";

        /// <summary>
        /// Роль администратора
        /// </summary>
        public const string AdminRole = "Admin";

        /// <summary>
        /// Роль покупателя
        /// </summary>
        public const string CustomerRole = "Customer";

        /// <summary>
        /// почта админа
        /// </summary>
        public const string AdminEmail = "89225651308ia@gmail.com";

        /// <summary>
        /// категории
        /// </summary>
        public const string CategoryName = "Category";

        /// <summary>
        /// тип использования
        /// </summary>
        public const string ApplicationTypeName = "ApplicationType";

        /// <summary>
        /// Успех
        /// </summary>
        public const string Success = "Success";

        /// <summary>
        /// Ошибка
        /// </summary>
        public const string Error = "Error";

        /// <summary>
        /// Статусы заказа
        /// </summary>
        public class OrderStatuses
        {
            public const string Pending = "Ожидание";
            public const string Approved = "Одобрен";
            public const string Processing = "Обработка";
            public const string Shipped = "Отправлен";
            public const string Canceled = "Отменен";
            public const string Refunded = "Возврат";

            public static readonly IEnumerable<string> StatusList = new ReadOnlyCollection<string>(
                new List<string>{

                    Pending, Approved, Processing, Shipped, Canceled, Refunded
                    
                });
        }
    }
}
