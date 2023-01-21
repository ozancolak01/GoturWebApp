namespace GoturWebApp.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class BasketDetail
    {
        public BasketDetail(int BasketID, int ProductID)
        {
            this.BasketID = BasketID;
            this.ProductID = ProductID;
        }
        [Key]
        public int BasketDetailId { get; set; }
        public int BasketID { get; set; }
        public int ProductID { get; set; }
        public Nullable<int> Quantity { get; set; }

        public virtual Basket Basket { get; set; }
        public virtual Product Product { get; set; }
    }
}
