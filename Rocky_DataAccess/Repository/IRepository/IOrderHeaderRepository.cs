using Rocky_Models;

namespace Rocky_DataAccess.Repository.IRepository
{
    /// <summary>
    /// Интерфейс репозитория заголовка заказа
    /// </summary>
    public interface IOrderHeaderRepository: IRepository<OrderHeader>
    {
        /// <summary>
        /// Обновить заголовок заказа
        /// </summary>
        void Update(OrderHeader orderHeader);
    }
}
