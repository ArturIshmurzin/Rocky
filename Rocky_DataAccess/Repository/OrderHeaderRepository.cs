using Rocky_DataAccess.Data;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models;

namespace Rocky_DataAccess.Repository
{
    /// <summary>
    /// Репозиторий заголовка заказа
    /// </summary>
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        public OrderHeaderRepository(DBApplicationContext dBApplicationContext) : base(dBApplicationContext)
        {
            _db = dBApplicationContext;
        }

        /// <summary>
        /// Класс для взаимодействия с бд
        /// </summary>
        private readonly DBApplicationContext _db;

        public void Update(OrderHeader orderHeader)
        {
            dBSet.Update(orderHeader);
        }
    }
}
