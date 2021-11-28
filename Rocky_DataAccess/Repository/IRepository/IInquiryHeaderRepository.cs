using Rocky_Models;

namespace Rocky_DataAccess.Repository.IRepository
{
    /// <summary>
    /// Интерфейс репозитория заголовка запроса
    /// </summary>
    public interface IInquiryHeaderRepository: IRepository<InquiryHeader>
    {
        /// <summary>
        /// Обновить заголовок товара
        /// </summary>
        void Update(InquiryHeader inquiryHeader);
    }
}
