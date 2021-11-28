using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rocky_Models
{
    /// <summary>
    /// Пользователь приложения
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Полное имя
        /// </summary>
        [Required]
        public string FullName { get; set; }

        /// <summary>
        /// Адрес пользователя
        /// </summary>
        [NotMapped]
        public string StreetAddress { get; set; }

        /// <summary>
        /// Город доставки
        /// </summary>
        [NotMapped]
        public string City { get; set; }

        /// <summary>
        /// Район
        /// </summary>
        [NotMapped]
        public string State { get; set; }

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        [NotMapped]
        public string PostalCode { get; set; }
    }
}
