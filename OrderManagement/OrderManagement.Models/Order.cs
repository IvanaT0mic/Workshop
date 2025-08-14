namespace OrderManagement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Processing, Completed, Cancelled
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
