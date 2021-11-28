using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rocky_Models
{
    /// <summary>
    /// Модель продукта
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Краткое описание
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// изображение
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public double Price { get; set; }

        /// <summary>
        /// Идентификатор категории
        /// </summary>
        [Display(Name="Categoty Type")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Ссылка на категорию
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        /// <summary>
        /// Идентификатор типа приложения
        /// </summary>
        [Display(Name = "Application Type")]
        public int ApplicationTypeId { get; set; }

        /// <summary>
        /// Ссылка на тип приложения
        /// </summary>
        [ForeignKey("ApplicationTypeId")]
        public virtual ApplicationType ApplicationType { get; set; }

        /// <summary>
        /// Количество товара
        /// </summary>
        [NotMapped]
        [Range(1, 10000, ErrorMessage = "Количество товара должно быть больше 0")]
        public int TempCount { get; set; } = 1;
    }
}
