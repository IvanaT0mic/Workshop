using OrderManagement.DataAccess;
using OrderManagement.Models;
using OrderManagement.Repository.Interfaces;

namespace OrderManagement.Repository;

public class OrderRepository(IInMemoryDatabase database) : IOrderRepository
{
    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        await Task.Delay(1);
        return database.Orders.ToList();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        await Task.Delay(1);
        var order = database.Orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            order.OrderItems = database.OrderItems.Where(oi => oi.OrderId == id).ToList();
            foreach (var orderItem in order.OrderItems)
            {
                orderItem.Item = database.Items.FirstOrDefault(i => i.Id == orderItem.ItemId);
            }
        }
        return order;
    }

    public async Task<Order> CreateAsync(Order order)
    {
        await Task.Delay(1);
        order.Id = database.GetNextOrderId();
        order.OrderDate = DateTime.Now;
        database.Orders.Add(order);
        return order;
    }

    public async Task<Order?> UpdateAsync(int id, Order order)
    {
        await Task.Delay(1);
        var existingOrder = database.Orders.FirstOrDefault(o => o.Id == id);
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
        var order = database.Orders.FirstOrDefault(o => o.Id == id);
        if (order == null)
            return false;


        var orderItems = database.OrderItems.Where(oi => oi.OrderId == id).ToList();
        foreach (var orderItem in orderItems)
        {
            database.OrderItems.Remove(orderItem);
        }

        database.Orders.Remove(order);
        return true;
    }

    public async Task<IEnumerable<Order>> GetByCustomerAsync(string customerName)
    {
        await Task.Delay(1);
        return database.Orders.Where(o =>
            o.CustomerName.Contains(customerName, StringComparison.OrdinalIgnoreCase)
        ).ToList();
    }

    public async Task<Order?> AddItemToOrderAsync(int orderId, int itemId, int quantity)
    {
        await Task.Delay(1);
        var order = database.Orders.FirstOrDefault(o => o.Id == orderId);
        var item = database.Items.FirstOrDefault(i => i.Id == itemId);

        if (order == null || item == null || item.StockQuantity < quantity)
            return null;


        var existingOrderItem = database.OrderItems.FirstOrDefault(oi => oi.OrderId == orderId && oi.ItemId == itemId);

        if (existingOrderItem != null)
        {
            existingOrderItem.Quantity += quantity;
        }
        else
        {
            var orderItem = new OrderItem
            {
                Id = database.GetNextOrderItemId(),
                OrderId = orderId,
                ItemId = itemId,
                Quantity = quantity,
                UnitPrice = item.Price
            };
            database.OrderItems.Add(orderItem);
        }


        item.StockQuantity -= quantity;


        order.TotalAmount = database.OrderItems
            .Where(oi => oi.OrderId == orderId)
            .Sum(oi => oi.TotalPrice);

        return order;
    }

    public async Task<bool> RemoveItemFromOrderAsync(int orderId, int itemId)
    {
        await Task.Delay(1);
        var orderItem = database.OrderItems.FirstOrDefault(oi => oi.OrderId == orderId && oi.ItemId == itemId);
        var item = database.Items.FirstOrDefault(i => i.Id == itemId);
        var order = database.Orders.FirstOrDefault(o => o.Id == orderId);

        if (orderItem == null || item == null || order == null)
            return false;


        item.StockQuantity += orderItem.Quantity;


        database.OrderItems.Remove(orderItem);


        order.TotalAmount = database.OrderItems
            .Where(oi => oi.OrderId == orderId)
            .Sum(oi => oi.TotalPrice);

        return true;
    }
}
