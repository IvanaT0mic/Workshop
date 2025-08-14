using OrderManagement.Models;

namespace OrderManagement.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task<Order> CreateOrderAsync(Order order);
        Task<Order?> UpdateOrderAsync(int id, Order order);
        Task<bool> DeleteOrderAsync(int id);
        Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerName);
        Task<Order?> AddItemToOrderAsync(int orderId, int itemId, int quantity);
        Task<bool> RemoveItemFromOrderAsync(int orderId, int itemId);
        Task<Order?> UpdateOrderStatusAsync(int orderId, string status);
        Task<decimal> CalculateOrderTotalAsync(int orderId);
    }
}
