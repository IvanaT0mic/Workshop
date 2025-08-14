using OrderManagement.Models;

namespace OrderManagement.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task<Order?> UpdateAsync(int id, Order order);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Order>> GetByCustomerAsync(string customerName);
        Task<Order?> AddItemToOrderAsync(int orderId, int itemId, int quantity);
        Task<bool> RemoveItemFromOrderAsync(int orderId, int itemId);
    }
}
