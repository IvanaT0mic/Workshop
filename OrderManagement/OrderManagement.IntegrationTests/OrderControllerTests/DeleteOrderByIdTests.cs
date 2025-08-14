namespace OrderManagement.IntegrationTests.OrderControllerTests
{
    public class DeleteOrderByIdTests : IntegrationTestBase
    {
        private readonly OrderController _orderController;
        private readonly ItemController _itemController;

        public DeleteOrderByIdTests()
        {
            _orderController = GetService<OrderController>();
            _itemController = GetService<ItemController>();
        }

        [Fact]
        public async Task DeleteOrderAsync_WithExistingOrder_ShouldDeleteSuccessfully()
        {
            // Arrange
            var newOrder = _fixture.Build<Order>()
                .With(o => o.CustomerName, "Order to Delete")
                .Without(o => o.Id)
                .Without(o => o.OrderDate)
                .Without(o => o.Status)
                .Without(o => o.TotalAmount)
                .Without(o => o.OrderItems)
                .Create();

            var createdOrder = await _orderController.CreateOrderAsync(newOrder);

            // Act
            var deleteResult = await _orderController.DeleteOrderAsync(createdOrder.Id);

            // Assert
            deleteResult.Should().BeTrue();

            // Verify order is deleted
            var deletedOrder = await _orderController.GetOrderByIdAsync(createdOrder.Id);
            deletedOrder.Should().BeNull();
        }
    }
}
