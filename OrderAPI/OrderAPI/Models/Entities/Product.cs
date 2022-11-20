namespace OrderAPI.Models.Entities
{
    public class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Status { get; set; }    
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
