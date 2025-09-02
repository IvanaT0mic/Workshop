namespace OrderManagement.IntegrationTests.OrderControllerTests;

public class UpdateOrderTests : IntegrationTestBase
{
    private readonly OrderController _orderController;
    private readonly ItemController _itemController;

    public UpdateOrderTests()
    {
        _orderController = GetService<OrderController>();
        _itemController = GetService<ItemController>();
    }

    [Fact]
    public async Task UpdateOrderAsync_WithValidData_ShouldUpdateSuccessfully()
    {
        // Arrange
        var oldOrder = _fixture.Build<Order>()
            .With(o => o.CustomerName, "Order to Change")
            .Without(o => o.Id)
            .Without(o => o.OrderDate)
            .Without(o => o.Status)
            .Without(o => o.TotalAmount)
            .Without(o => o.OrderItems)
            .Create();

        var newOrder = _fixture.Build<Order>()
            .With(o => o.CustomerName, "Order is Changed")
            .Without(o => o.Id)
            .Without(o => o.OrderDate)
            .Without(o => o.Status)
            .Without(o => o.TotalAmount)
            .Without(o => o.OrderItems)
            .Create();

        var createdOrder = await _orderController.CreateOrderAsync(oldOrder);

        // Act
        var updateResult = await _orderController.UpdateOrderAsync(oldOrder.Id, newOrder);

        // Assert
        updateResult.Should().NotBeNull();
        updateResult.CustomerName.Should().Be(newOrder.CustomerName);
        updateResult.Status.Should().Be(newOrder.Status);
        updateResult.TotalAmount.Should().Be(newOrder.TotalAmount);



    }
}