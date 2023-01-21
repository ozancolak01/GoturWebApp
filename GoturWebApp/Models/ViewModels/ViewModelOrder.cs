namespace GoturWebApp.Models.ViewModels
{
    public class ViewModelOrder
    {
        public int OrderID { get; set; }

        public Nullable<System.DateTime> OrderDate { get; set; }
        public int CustomerID { get; set; }
    }
}
