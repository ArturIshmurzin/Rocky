using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rocky_Models;

namespace Rocky_DataAccess.Data
{
    /// <summary>
    /// Контекст подключения в базе данных
    /// </summary>
    public class DBApplicationContext : IdentityDbContext
    {
        public DBApplicationContext(DbContextOptions<DBApplicationContext> options) :base(options)
        {

        }

        /// <summary>
        /// Таблица категорий
        /// </summary>
        public DbSet<Category> Category { get; set; }

        /// <summary>
        /// Таблица товаров
        /// </summary>
        public DbSet<Product> Product { get; set; }

        /// <summary>
        /// Таблица типов
        /// </summary>
        public DbSet<ApplicationType> ApplicationType { get; set; }

        /// <summary>
        /// Таблица юзеров
        /// </summary>
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        /// <summary>
        /// Таблица заголовка запроса
        /// </summary>
        public DbSet<InquiryHeader> InquiryHeader { get; set; }

        /// <summary>
        /// Таблица деталей запроса
        /// </summary>
        public DbSet<InquiryDetail> InquiryDetail { get; set; }

        /// <summary>
        /// Таблица заголовка заказа
        /// </summary>
        public DbSet<OrderHeader> OrderHeader { get; set; }

        /// <summary>
        /// Таблица деталей заказа
        /// </summary>
        public DbSet<OrderDetail> OrderDetail { get; set; }
    }
}
