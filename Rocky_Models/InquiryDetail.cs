using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rocky_Models
{
    /// <summary>
    /// Детали запроса
    /// </summary>
    public class InquiryDetail
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Идентификатор шапки запроса
        /// </summary>
        public int InquiryHeaderID { get; set; }

        /// <summary>
        /// Шапка запроса
        /// </summary>
        [ForeignKey("InquiryHeaderID")]
        public InquiryHeader InquiryHeader { get; set; }

        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Товар
        /// </summary>
        [ForeignKey("ProductID")]
        public Product Product { get; set; }
    }
}
