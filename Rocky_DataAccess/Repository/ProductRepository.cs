using Microsoft.AspNetCore.Mvc.Rendering;
using Rocky_DataAccess.Data;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models;
using Rocky_Utility;
using System.Collections.Generic;
using System.Linq;

namespace Rocky_DataAccess.Repository
{
    /// <summary>
    /// Репозиторий категории
    /// </summary>
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DBApplicationContext dBApplicationContext) : base(dBApplicationContext)
        {
            _db = dBApplicationContext;
        }

        /// <summary>
        /// Класс для взаимодействия с бд
        /// </summary>
        private readonly DBApplicationContext _db;

        public IEnumerable<SelectListItem> GetAllDropdownList(string prorertyName)
        {
            if(prorertyName == WC.CategoryName)
            {
                return _db.Category.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            }
            if(prorertyName == WC.ApplicationTypeName)
            {
                return _db.ApplicationType.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            }

            return null;
        }

        public void Update(Product product)
        {
            dBSet.Update(product);
        }
    }
}
