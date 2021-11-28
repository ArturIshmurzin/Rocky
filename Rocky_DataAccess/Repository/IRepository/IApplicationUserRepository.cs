using Rocky_Models;

namespace Rocky_DataAccess.Repository.IRepository
{
    /// <summary>
    /// Интерфейс репозитория деталей запроса
    /// </summary>
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        /// <summary>
        /// Обновить пользваотеля
        /// </summary>
        void Update(ApplicationUser applicationUser);
    }
}
