using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Rocky_Models
{
    /// <summary>
    /// Модель категории
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название категории
        /// </summary>
        [DisplayName("Название категории")]
        [Required(ErrorMessage = "Название категории является обязательным")]
        public string Name { get; set; }

        /// <summary>
        /// Порядок отображения
        /// </summary>
        [DisplayName("Порядок отображения")]
        [Required(ErrorMessage = "Порядок отображения является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "Порядок отображения должен быть больше нуля")]
        public int DisplayOrder { get; set; }
    }
}
