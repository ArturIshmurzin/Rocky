using Rocky_Models;

namespace Rocky_DataAccess.Repository.IRepository
{
    /// <summary>
    /// Интерфейс репозитория категории
    /// </summary>
    public interface IApplicationTypeRepository : IRepository<ApplicationType>
    {
        /// <summary>
        /// Обновить категорию
        /// </summary>
        void Update(ApplicationType category);
    }
}
