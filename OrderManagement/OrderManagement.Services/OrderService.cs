using OrderManagement.Models;
using OrderManagement.Repository.Interfaces;
using OrderManagement.Services.Interfaces;

namespace OrderManagement.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IItemRepository _itemRepository;

        public OrderService(IOrderRepository orderRepository, IItemRepository itemRepository)
        {
            _orderRepository = orderRepository;
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {

            if (string.IsNullOrWhiteSpace(order.CustomerName))
                throw new ArgumentException("Customer name is required.");


            order.OrderDate = DateTime.Now;
            order.Status = "Pending";
            order.TotalAmount = 0;

            return await _orderRepository.CreateAsync(order);
        }

        public async Task<Order?> UpdateOrderAsync(int id, Order order)
        {
            if (id <= 0)
                return null;


            if (string.IsNullOrWhiteSpace(order.CustomerName))
                throw new ArgumentException("Customer name is required.");

            return await _orderRepository.UpdateAsync(id, order);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            if (id <= 0)
                return false;


            var order = await _orderRepository.GetByIdAsync(id);
            if (order.Status == "Completed")
                throw new InvalidOperationException("Cannot delete completed orders.");

            return await _orderRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerName)
        {
            if (string.IsNullOrWhiteSpace(customerName))
                return new List<Order>();

            return await _orderRepository.GetByCustomerAsync(customerName);
        }

        public async Task<Order?> AddItemToOrderAsync(int orderId, int itemId, int quantity)
        {
            if (orderId <= 0 || itemId <= 0 || quantity <= 0)
                return null;


            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new ArgumentException("Order not found.");

            if (order.Status == "Completed" || order.Status == "Cancelled")
                throw new InvalidOperationException("Cannot modify completed or cancelled orders.");

            var item = await _itemRepository.GetByIdAsync(itemId);
            if (item.StockQuantity < quantity)
                throw new InvalidOperationException("Insufficient stock available.");

            return await _orderRepository.AddItemToOrderAsync(orderId, itemId, quantity);
        }

        public async Task<bool> RemoveItemFromOrderAsync(int orderId, int itemId)
        {
            if (orderId <= 0 || itemId <= 0)
                return false;


            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return false;

            if (order.Status == "Completed" || order.Status == "Cancelled")
                throw new InvalidOperationException("Cannot modify completed or cancelled orders.");

            return await _orderRepository.RemoveItemFromOrderAsync(orderId, itemId);
        }

        public async Task<Order?> UpdateOrderStatusAsync(int orderId, string status)
        {
            if (orderId <= 0 || string.IsNullOrWhiteSpace(status))
                return null;

            var validStatuses = new[] { "Pending", "Processing", "Completed", "Cancelled" };
            if (!validStatuses.Contains(status))
                throw new ArgumentException("Invalid order status.");

            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return null;

            order.Status = status;
            return await _orderRepository.UpdateAsync(orderId, order);
        }

        public async Task<decimal> CalculateOrderTotalAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return 0;

            return order.TotalAmount;
        }
    }
}
