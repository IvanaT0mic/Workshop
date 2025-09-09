namespace OrderManagement.UnitTests.OrderServiceTests
{
    public class CreateOrderTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly Mock<IItemRepository> _mockItemRepository;
        private readonly OrderService _orderService;
        private readonly Fixture _fixture;

        public CreateOrderTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockItemRepository = new Mock<IItemRepository>();
            _orderService = new OrderService(_mockOrderRepository.Object, _mockItemRepository.Object);
            _fixture = new Fixture();

            // Configure AutoFixture to handle circular references
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task CreateOrderAsync_WithValidCustomerName_ShouldCreateOrderSuccessfully()
        {
            // Arrange
            var order = _fixture.Build<Order>()
                .With(o => o.CustomerName, "John Doe")
                .Without(o => o.Id)
                .Without(o => o.OrderDate)
                .Without(o => o.Status)
                .Without(o => o.TotalAmount)
                .Without(o => o.OrderItems)
                .Create();

            var expectedOrder = _fixture.Build<Order>()
                .With(o => o.Id, 1)
                .With(o => o.CustomerName, "John Doe")
                .With(o => o.Status, "Pending")
                .With(o => o.TotalAmount, 0m)
                .Create();

            _mockOrderRepository
                .Setup(repo => repo.CreateAsync(It.IsAny<Order>()))
                .ReturnsAsync(expectedOrder);

            // Act
            var result = await _orderService.CreateOrderAsync(order);

            // Assert
            result.Should().NotBeNull();
            result.CustomerName.Should().Be("John Doe");
            result.Status.Should().Be("Pending");
            result.TotalAmount.Should().Be(0m);
            result.Id.Should().Be(1);

            _mockOrderRepository.Verify(repo => repo.CreateAsync(It.Is<Order>(o =>
                o.CustomerName == "John Doe" &&
                o.Status == "Pending" &&
                o.TotalAmount == 0m &&
                o.OrderDate != default(DateTime)
            )), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task CreateOrderAsync_WithInvalidCustomerName_ShouldThrowArgumentException(string customerName)
        {
            // Arrange
            var order = _fixture.Build<Order>()
                .With(o => o.CustomerName, customerName)
                .Create();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _orderService.CreateOrderAsync(order));
            exception.Message.Should().Be("Customer name is required.");

            _mockOrderRepository.Verify(repo => repo.CreateAsync(It.IsAny<Order>()), Times.Never);
        }
    }
}
