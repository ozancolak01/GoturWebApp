using GoturWebApp.Data;
namespace GoturWebApp.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Order
    {
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }

        public Order(int CustomerID, string Status)
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.CustomerID = CustomerID;
            this.Status = Status;
        }

        [Key]
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public Nullable<System.DateTime> DateOrdered { get; set; } = DateTime.Now;

        [Required]
        public string Status { get; set; } //basket, ordered
    
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
