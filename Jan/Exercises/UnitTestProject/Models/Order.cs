namespace ProjectForTesting.Models;

public enum OrderStatus
{
    Cancelled = 0,
    Active = 1
}

public class Order
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderComponent> OrderComponents { get; set; } = [];
    public List<OrderHistory> OrderHistories { get; set; } = [];
    public List<UserFavoriteOrder> UserFavoriteOrders { get; set; } = [];
}

public class OrderComponent;
public class OrderHistory;
public class UserFavoriteOrder;