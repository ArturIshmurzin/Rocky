using Rocky_Models;

namespace Rocky_DataAccess.Repository.IRepository
{
    /// <summary>
    /// Интерфейс репозитория деталей запроса
    /// </summary>
    public interface IInquiryDetailRepository : IRepository<InquiryDetail>
    {
        /// <summary>
        /// Обновить детали товара
        /// </summary>
        void Update(InquiryDetail inquiryHeader);
    }
}
