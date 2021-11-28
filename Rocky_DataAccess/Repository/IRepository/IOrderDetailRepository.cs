using Rocky_Models;

namespace Rocky_DataAccess.Repository.IRepository
{
    /// <summary>
    /// Интерфейс репозитория деталей заказа
    /// </summary>
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        /// <summary>
        /// Обновить детали заказа
        /// </summary>
        void Update(OrderDetail orderDetail);
    }
}
