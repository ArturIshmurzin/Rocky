using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rocky_Models
{
    /// <summary>
    /// Модель шапки запроса
    /// </summary>
    public class InquiryHeader
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string ApplicationUserID { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        [ForeignKey("ApplicationUserID")]
        public ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// дата заказа
        /// </summary>
        public DateTime InquiryDate { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        public string FullName { get; set; }

        /// <summary>
        /// Электронный адрес
        /// </summary>
        [Required]
        public string Email { get; set; }
    }
}
