using Rocky_DataAccess.Data;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models;

namespace Rocky_DataAccess.Repository
{
    /// <summary>
    /// Репозиторий заголовка запроса
    /// </summary>
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(DBApplicationContext dBApplicationContext) : base(dBApplicationContext)
        {
            _db = dBApplicationContext;
        }

        /// <summary>
        /// Класс для взаимодействия с бд
        /// </summary>
        private readonly DBApplicationContext _db;

        public void Update(ApplicationUser applicationUser)
        {
            dBSet.Update(applicationUser);
        }
    }
}
