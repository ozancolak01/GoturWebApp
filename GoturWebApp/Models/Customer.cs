using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GoturWebApp.Models
{
    
    public partial class Customer
    {
        public Customer()
        {
            this.Orders = new HashSet<Order>();
        }

        [Key]
        public int CustomerID { get; set; }

        [Required]
        [DisplayName("Password")]
        public string CustomerPassword { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Customer_Name { get; set; }

        [Required]
        public string Address { get; set; }

        [DisplayName("Phone Number")]
        public Nullable<int> Phone_number { get; set; }

        public virtual Basket Basket { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
