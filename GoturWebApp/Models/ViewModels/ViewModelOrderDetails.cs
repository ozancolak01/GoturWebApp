namespace GoturWebApp.Models.ViewModels
{
    public class ViewModelOrderDetails
    {
        public int OrderID { get; set; }
        public int OrderDetailsID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public string ProductPhoto { get; set; }
    }
}
