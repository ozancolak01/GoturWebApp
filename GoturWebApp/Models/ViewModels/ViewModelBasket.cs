namespace GoturWebApp.Models.ViewModels
{
    public class ViewModelBasket
    {
        public int BasketID { get; set; }
        public int CustomerID { get; set; }
        public int BasketDetailsID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public string ProductPhoto { get; set; }
    }
}
