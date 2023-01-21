namespace GoturWebApp.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class OrderDetail
    {
        public OrderDetail(int OrderID, int ProductID)
        {
            this.OrderID = OrderID;
            this.ProductID = ProductID;
        }
        [Key]
        public int OrderDetailsID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public Nullable<int> Quantity { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
