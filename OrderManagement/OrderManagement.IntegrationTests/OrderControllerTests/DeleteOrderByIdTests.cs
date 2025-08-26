namespace OrderManagement.IntegrationTests.OrderControllerTests;

public class DeleteOrderByIdTests : IntegrationTestBase
{
    [Fact]
    public async Task DeleteOrderAsync_WithExistingOrder_ShouldDeleteSuccessfully()
    {
        var (_testDb, _itemController) = CreateController<ItemController, ItemService, ItemRepository>();
        var _orderController = CreateController<OrderController, OrderService, OrderRepository, ItemRepository>(_testDb);

        // Arrange
        var newOrder = _fixture.Build<Order>()
            .With(o => o.Id, _testDb.GetNextOrderId())
            .Create();

        _testDb.Orders.Add(newOrder);
        _testDb.Orders.Should().Contain(newOrder);

        // Act
        var deleteResult = await _orderController.DeleteOrderAsync(newOrder.Id);

        // Assert
        deleteResult.Should().BeTrue();

        // Verify order is deleted
        var deletedOrder = await _orderController.GetOrderByIdAsync(newOrder.Id);
        deletedOrder.Should().BeNull();
    }
}
