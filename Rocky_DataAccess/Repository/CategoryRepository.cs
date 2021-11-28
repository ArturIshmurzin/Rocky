using Rocky_DataAccess.Data;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models;

namespace Rocky_DataAccess.Repository
{
    /// <summary>
    /// Репозиторий категории
    /// </summary>
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DBApplicationContext dBApplicationContext) : base(dBApplicationContext)
        {

        }

        public void Update(Category category)
        {
            Category categoryDb = base.FirstOrDefault(x => x.Id == category.Id);

            if (categoryDb != null)
            {
                categoryDb.Name = category.Name;
                categoryDb.DisplayOrder = category.DisplayOrder;
            }
        }
    }
}
