namespace Rocky_Models.ViewModels
{
    /// <summary>
    /// Модель для представления страницы деталей о товаре
    /// </summary>
    public class DetailsVM
    {
        public DetailsVM()
        {
            this.Product = new Product();
        }

        /// <summary>
        /// Товар
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Товар содержится в корзине
        /// </summary>
        public bool ExistInCart { get; set; }
    }
}
