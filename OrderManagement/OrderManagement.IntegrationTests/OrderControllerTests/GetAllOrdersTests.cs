namespace OrderManagement.IntegrationTests.OrderControllerTests;

public class GetAllOrdersTests : IntegrationTestBase
{
    private readonly OrderController _orderController;
    private readonly ItemController _itemController;

    public GetAllOrdersTests()
    {
        _orderController = GetService<OrderController>();
        _itemController = GetService<ItemController>();
    }

    [Fact]
    public async Task GetAllOrdersAsync_WithExistingOrders_ShouldReturnSuccessfully()
    {
        // Arrange
        var newOrder = _fixture.Build<Order>()
            .With(o => o.CustomerName, "Order to Return")
            .Without(o => o.Id)
            .Without(o => o.OrderDate)
            .Without(o => o.Status)
            .Without(o => o.TotalAmount)
            .Without(o => o.OrderItems)
            .Create();

        var createdOrder = await _orderController.CreateOrderAsync(newOrder);

        // Act
        var getAllResults = await _orderController.GetAllOrdersAsync();

        // Assert
        getAllResults.Should().NotBeEmpty();
    }
}