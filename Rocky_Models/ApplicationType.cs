using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Rocky_Models
{
    /// <summary>
    /// Модель типа приложения
    /// </summary>
    public class ApplicationType
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [DisplayName("Название")]
        [Required(ErrorMessage ="Название является обязательным полем")]
        public string Name { get; set; }
    }
}
