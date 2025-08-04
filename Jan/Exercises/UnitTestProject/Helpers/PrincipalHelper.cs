using ProjectForTesting.Services;

namespace ProjectForTesting.Helpers;

public class PrincipalHelper
{
    OrderService orderService = new OrderService();
    public CurrentUser GetCurrent() => new();
}

public class CurrentUser
{
    public List<CurrentUser> Users { get; set; } = [];
}