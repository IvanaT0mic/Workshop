using OrderManagement.Models;

namespace OrderManagement.DataAccess;

public interface IInMemoryDatabase
{
    List<Item> Items { get; }
    List<Order> Orders { get; }
    List<OrderItem> OrderItems { get; }

    int GetNextItemId();
    int GetNextOrderId();
    int GetNextOrderItemId();
}
