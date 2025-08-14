using OrderManagement.Models;

namespace OrderManagement.DataAccess
{
    public class InMemoryDatabase
    {
        private static InMemoryDatabase? _instance;
        private static readonly object _lock = new object();

        public List<Item> Items { get; set; } = new List<Item>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        private int _nextItemId = 1;
        private int _nextOrderId = 1;
        private int _nextOrderItemId = 1;

        private InMemoryDatabase()
        {
            SeedData();
        }

        public static InMemoryDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new InMemoryDatabase();
                    }
                }
                return _instance;
            }
        }

        public int GetNextItemId() => _nextItemId++;
        public int GetNextOrderId() => _nextOrderId++;
        public int GetNextOrderItemId() => _nextOrderItemId++;

        private void SeedData()
        {

            Items.AddRange(new List<Item>
            {
                new Item { Id = GetNextItemId(), Name = "Laptop", Description = "High-performance laptop", Price = 999.99m, StockQuantity = 10, Category = "Electronics" },
                new Item { Id = GetNextItemId(), Name = "Mouse", Description = "Wireless optical mouse", Price = 25.50m, StockQuantity = 50, Category = "Electronics" },
                new Item { Id = GetNextItemId(), Name = "Keyboard", Description = "Mechanical gaming keyboard", Price = 75.00m, StockQuantity = 30, Category = "Electronics" },
                new Item { Id = GetNextItemId(), Name = "Monitor", Description = "24-inch LED monitor", Price = 199.99m, StockQuantity = 15, Category = "Electronics" },
                new Item { Id = GetNextItemId(), Name = "Notebook", Description = "Spiral notebook", Price = 3.99m, StockQuantity = 100, Category = "Office Supplies" },
                new Item { Id = GetNextItemId(), Name = "Pen", Description = "Blue ballpoint pen", Price = 1.50m, StockQuantity = 200, Category = "Office Supplies" }
            });


            var order1 = new Order 
            { 
                Id = GetNextOrderId(), 
                CustomerName = "John Doe", 
                OrderDate = DateTime.Now.AddDays(-5), 
                Status = "Completed",
                TotalAmount = 0 
            };

            var order2 = new Order 
            { 
                Id = GetNextOrderId(), 
                CustomerName = "Jane Smith", 
                OrderDate = DateTime.Now.AddDays(-2), 
                Status = "Processing",
                TotalAmount = 0 
            };

            Orders.AddRange(new List<Order> { order1, order2 });


            var orderItem1 = new OrderItem 
            { 
                Id = GetNextOrderItemId(), 
                OrderId = order1.Id, 
                ItemId = 1, 
                Quantity = 1, 
                UnitPrice = 999.99m 
            };

            var orderItem2 = new OrderItem 
            { 
                Id = GetNextOrderItemId(), 
                OrderId = order1.Id, 
                ItemId = 2, 
                Quantity = 2, 
                UnitPrice = 25.50m 
            };

            var orderItem3 = new OrderItem 
            { 
                Id = GetNextOrderItemId(), 
                OrderId = order2.Id, 
                ItemId = 3, 
                Quantity = 1, 
                UnitPrice = 75.00m 
            };

            OrderItems.AddRange(new List<OrderItem> { orderItem1, orderItem2, orderItem3 });


            order1.TotalAmount = OrderItems.Where(oi => oi.OrderId == order1.Id).Sum(oi => oi.TotalPrice);
            order2.TotalAmount = OrderItems.Where(oi => oi.OrderId == order2.Id).Sum(oi => oi.TotalPrice);


            foreach (var orderItem in OrderItems)
            {
                orderItem.Order = Orders.FirstOrDefault(o => o.Id == orderItem.OrderId);
                orderItem.Item = Items.FirstOrDefault(i => i.Id == orderItem.ItemId);
            }

            foreach (var order in Orders)
            {
                order.OrderItems = OrderItems.Where(oi => oi.OrderId == order.Id).ToList();
            }
        }
    }
}
