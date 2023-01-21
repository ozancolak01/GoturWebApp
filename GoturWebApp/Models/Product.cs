namespace GoturWebApp.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Product
    {
        public Product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Description { get; set; }
        public Nullable<int> Quantity { get; set; }

        public string PhotoLink { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
