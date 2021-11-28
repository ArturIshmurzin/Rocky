using Rocky_DataAccess.Data;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models;

namespace Rocky_DataAccess.Repository
{
    /// <summary>
    /// Репозиторий категории
    /// </summary>
    public class ApplicationTypeRepository : Repository<ApplicationType>, IApplicationTypeRepository
    {
        public ApplicationTypeRepository(DBApplicationContext dBApplicationContext) : base(dBApplicationContext)
        {

        }

        public void Update(ApplicationType applicationType)
        {
            ApplicationType applicationTypeDb = base.FirstOrDefault(x => x.Id == applicationType.Id);

            if (applicationTypeDb != null)
            {
                applicationTypeDb.Name = applicationTypeDb.Name;
            }
        }
    }
}
