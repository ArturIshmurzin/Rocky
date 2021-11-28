using Rocky_DataAccess.Data;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models;

namespace Rocky_DataAccess.Repository
{
    /// <summary>
    /// Репозиторий заголовка заказа
    /// </summary>
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(DBApplicationContext dBApplicationContext) : base(dBApplicationContext)
        {
            _db = dBApplicationContext;
        }

        /// <summary>
        /// Класс для взаимодействия с бд
        /// </summary>
        private readonly DBApplicationContext _db;

        public void Update(OrderDetail orderDetail)
        {
            dBSet.Update(orderDetail);
        }
    }
}
