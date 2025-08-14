using OrderManagement.Models;
using OrderManagement.Services.Interfaces;

namespace OrderManagement.Controllers
{
    public class OrderController(IOrderService _orderService)
    {
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderService.GetAllOrdersAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _orderService.GetOrderByIdAsync(id);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            return await _orderService.CreateOrderAsync(order);
        }

        public async Task<Order?> UpdateOrderAsync(int id, Order order)
        {
            return await _orderService.UpdateOrderAsync(id, order);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _orderService.DeleteOrderAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerName)
        {
            return await _orderService.GetOrdersByCustomerAsync(customerName);
        }

        public async Task<Order?> AddItemToOrderAsync(int orderId, int itemId, int quantity)
        {
            return await _orderService.AddItemToOrderAsync(orderId, itemId, quantity);
        }

        public async Task<bool> RemoveItemFromOrderAsync(int orderId, int itemId)
        {
            return await _orderService.RemoveItemFromOrderAsync(orderId, itemId);
        }

        public async Task<Order?> UpdateOrderStatusAsync(int orderId, string status)
        {
            return await _orderService.UpdateOrderStatusAsync(orderId, status);
        }

        public async Task<decimal> GetOrderTotalAsync(int orderId)
        {
            return await _orderService.CalculateOrderTotalAsync(orderId);
        }
    }
}
