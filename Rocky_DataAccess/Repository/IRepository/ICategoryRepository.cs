using Rocky_Models;

namespace Rocky_DataAccess.Repository.IRepository
{
    /// <summary>
    /// Интерфейс репозитория категории
    /// </summary>
    public interface ICategoryRepository :IRepository<Category>
    {
        /// <summary>
        /// Обновить категорию
        /// </summary>
        void Update(Category category);
    }
}
