using System.Collections.Generic;

namespace Rocky_Models.ViewModels
{
    /// <summary>
    /// Модель запроса
    /// </summary>
    public class InquiryVM
    {
        /// <summary>
        /// Заголовок запроса
        /// </summary>
        public InquiryHeader InquiryHeader { get; set; }

        /// <summary>
        /// Детали запроса
        /// </summary>
        public IEnumerable<InquiryDetail> InquiryDetails { get; set; }
    }
}
