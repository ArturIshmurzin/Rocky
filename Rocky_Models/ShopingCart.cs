namespace Rocky_Models
{
    /// <summary>
    /// Корзина
    /// </summary>
    public class ShopingCart
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// количество товара
        /// </summary>
        public int Count { get; set; }
    }
}
