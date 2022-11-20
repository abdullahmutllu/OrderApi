namespace OrderAPI.Models.Entities
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMail { get; set; }
        public string CustomerGSM { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }

    }
}
