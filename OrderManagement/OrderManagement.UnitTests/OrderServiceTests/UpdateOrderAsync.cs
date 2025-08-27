namespace OrderManagement.UnitTests.OrderServiceTests;

public class UpdateOrderAsync
{
    private readonly Mock<IOrderRepository> _mockOrderRepository;
    private readonly Mock<IItemRepository> _mockItemRepository;
    private readonly OrderService _orderService;
    private readonly IFixture _fixture;

    public UpdateOrderAsync()
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
    public async Task UpdateOrderAsync_WithValidCustomerNameAndOrder_ShouldUpdateOrderSuccessfully()
    {
        // Arrange
        // id and order
        var orderId = 1;
        var updateOrder = _fixture.Build<Order>()
            .With(o => o.CustomerName, "Josh Boe")
            .With(o => o.Status, "Processing")
            .With(o => o.TotalAmount, 5.5m)
            .Without(o => o.Id)
            .Create();

        var order = _fixture.Build<Order>()
                .With(o => o.Id, 1)
                .With(o => o.CustomerName, "John Doe")
                .With(o => o.Status, "Pending")
                .With(o => o.TotalAmount, 0m)
                .Create();
        
        var expectedOrder = _fixture.Build<Order>()
                .With(o => o.Id, 1)
                .With(o => o.CustomerName, "Josh Boe")
                .With(o => o.Status, "Processing")
                .With(o => o.TotalAmount, 5.5m)
                .Create();

        _mockOrderRepository
            .Setup(repo => repo.UpdateAsync(It.IsAny<int>(),It.IsAny<Order>()))
            .ReturnsAsync(expectedOrder);
        
        // Act
        var result = await _orderService.UpdateOrderAsync(orderId, updateOrder);
        
        // Assert
        result.Should().NotBeNull();
        result.CustomerName.Should().Be("Josh Boe");
        result.Status.Should().Be("Processing");
        result.TotalAmount.Should().Be(5.5m);
        result.Id.Should().Be(1);

        _mockOrderRepository.Verify(repo => repo.UpdateAsync(
            It.Is<int>(o => o == 1),
            It.Is<Order>(o =>
                o.CustomerName == "Josh Boe" &&
                o.Status == "Processing" &&
                o.TotalAmount == 5.5m)), Times.Once);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task UpdateOrderAsync_WithInvalidCustomerName_ShouldThrowArgumentException(string customerName)
    {
        // Arrange
        var order = _fixture.Build<Order>()
            .With(o => o.CustomerName, customerName)
            .Create();
        
        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _orderService.UpdateOrderAsync(1, order));
        exception.Message.Should().Be("Customer name is required.");
        
        _mockOrderRepository.Verify(repo => repo.UpdateAsync(
            It.IsAny<int>(),It.IsAny<Order>()), Times.Never());

    }
}