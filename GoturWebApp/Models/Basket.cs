using System.ComponentModel.DataAnnotations;

namespace GoturWebApp.Models
{
    public class Basket
    {
        public Basket()
        {
            this.BasketDetails = new HashSet<BasketDetail>();
        }

        public Basket(int CustomerID)
        {
            this.CustomerID = CustomerID;
            this.BasketDetails = new HashSet<BasketDetail>();
        }

        [Key]
        public int BasketID { get; set; }
        public int CustomerID { get; set; }
        public int ProductCount { get; set; } = 0;
        public virtual Customer Customer { get; set; }
        public virtual ICollection<BasketDetail> BasketDetails { get; set; }
    }
}
