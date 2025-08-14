namespace OrderManagement.UnitTests.OrderServiceTests
{
    public class DeleteOrderAsync
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly Mock<IItemRepository> _mockItemRepository;
        private readonly OrderService _orderService;
        private readonly IFixture _fixture;

        public DeleteOrderAsync()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockItemRepository = new Mock<IItemRepository>();
            _orderService = new Services.OrderService(_mockOrderRepository.Object, _mockItemRepository.Object);
            _fixture = new Fixture();

            // Configure AutoFixture to handle circular references
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task DeleteOrderAsync_WithCompletedOrder_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var orderId = 1;
            var completedOrder = _fixture.Build<Order>()
                .With(o => o.Id, orderId)
                .With(o => o.Status, "Completed")
                .Create();

            _mockOrderRepository
                .Setup(repo => repo.GetByIdAsync(orderId))
                .ReturnsAsync(completedOrder);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _orderService.DeleteOrderAsync(orderId));

            exception.Message.Should().Be("Cannot delete completed orders.");

            _mockOrderRepository.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Never);
        }
    }
}
