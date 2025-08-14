using OrderManagement.DataAccess;
using OrderManagement.Models;
using OrderManagement.Repository.Interfaces;

namespace OrderManagement.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly InMemoryDatabase _database;

        public OrderRepository()
        {
            _database = InMemoryDatabase.Instance;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            await Task.Delay(1);
            return _database.Orders.ToList();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            await Task.Delay(1);
            var order = _database.Orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                order.OrderItems = _database.OrderItems.Where(oi => oi.OrderId == id).ToList();
                foreach (var orderItem in order.OrderItems)
                {
                    orderItem.Item = _database.Items.FirstOrDefault(i => i.Id == orderItem.ItemId);
                }
            }
            return order;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await Task.Delay(1);
            order.Id = _database.GetNextOrderId();
            order.OrderDate = DateTime.Now;
            _database.Orders.Add(order);
            return order;
        }

        public async Task<Order?> UpdateAsync(int id, Order order)
        {
            await Task.Delay(1);
            var existingOrder = _database.Orders.FirstOrDefault(o => o.Id == id);
            if (existingOrder == null)
                return null;

            existingOrder.CustomerName = order.CustomerName;
            existingOrder.Status = order.Status;
            existingOrder.TotalAmount = order.TotalAmount;

            return existingOrder;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await Task.Delay(1);
            var order = _database.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return false;


            var orderItems = _database.OrderItems.Where(oi => oi.OrderId == id).ToList();
            foreach (var orderItem in orderItems)
            {
                _database.OrderItems.Remove(orderItem);
            }

            _database.Orders.Remove(order);
            return true;
        }

        public async Task<IEnumerable<Order>> GetByCustomerAsync(string customerName)
        {
            await Task.Delay(1);
            return _database.Orders.Where(o => 
                o.CustomerName.Contains(customerName, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        public async Task<Order?> AddItemToOrderAsync(int orderId, int itemId, int quantity)
        {
            await Task.Delay(1);
            var order = _database.Orders.FirstOrDefault(o => o.Id == orderId);
            var item = _database.Items.FirstOrDefault(i => i.Id == itemId);

            if (order == null || item == null || item.StockQuantity < quantity)
                return null;


            var existingOrderItem = _database.OrderItems.FirstOrDefault(oi => oi.OrderId == orderId && oi.ItemId == itemId);
            
            if (existingOrderItem != null)
            {
                existingOrderItem.Quantity += quantity;
            }
            else
            {
                var orderItem = new OrderItem
                {
                    Id = _database.GetNextOrderItemId(),
                    OrderId = orderId,
                    ItemId = itemId,
                    Quantity = quantity,
                    UnitPrice = item.Price
                };
                _database.OrderItems.Add(orderItem);
            }


            item.StockQuantity -= quantity;


            order.TotalAmount = _database.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .Sum(oi => oi.TotalPrice);

            return order;
        }

        public async Task<bool> RemoveItemFromOrderAsync(int orderId, int itemId)
        {
            await Task.Delay(1);
            var orderItem = _database.OrderItems.FirstOrDefault(oi => oi.OrderId == orderId && oi.ItemId == itemId);
            var item = _database.Items.FirstOrDefault(i => i.Id == itemId);
            var order = _database.Orders.FirstOrDefault(o => o.Id == orderId);

            if (orderItem == null || item == null || order == null)
                return false;


            item.StockQuantity += orderItem.Quantity;


            _database.OrderItems.Remove(orderItem);


            order.TotalAmount = _database.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .Sum(oi => oi.TotalPrice);

            return true;
        }
    }
}
