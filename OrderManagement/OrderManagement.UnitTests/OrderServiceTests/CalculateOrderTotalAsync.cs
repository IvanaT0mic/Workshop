namespace OrderManagement.UnitTests.OrderServiceTests;

public class CalculateOrderTotalAsync
{
    
    private readonly Mock<IOrderRepository> _mockOrderRepository;
    private readonly Mock<IItemRepository> _mockItemRepository;
    private readonly OrderService _orderService;
    private readonly IFixture _fixture;

    public CalculateOrderTotalAsync()
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
    public async Task CalculateOrderTotalAsync_WithValidOrderId_ShouldCalculateTotalSuccessfully()
    {
        // Arrange
        var orderId = 1;
        var expectedTotal = 15.5m;
        var order = _fixture.Build<Order>()
                .With(o => o.Id, 1)
                .With(o => o.CustomerName, "John Doe")
                .With(o => o.Status, "Pending")
                .With(o => o.TotalAmount, expectedTotal)
                .Create();

        _mockOrderRepository
            .Setup(repo => repo.GetByIdAsync(It.Is<int>(o => o == orderId)))
            .ReturnsAsync(order);
        
        // Act
        var result = await _orderService.CalculateOrderTotalAsync(orderId);
        
        // Assert
        result.Should().Be(expectedTotal);
        
        _mockOrderRepository.Verify(repo => repo.GetByIdAsync(It.Is<int>(o => o == orderId)), Times.Once);
    }

    [Fact]
    public async Task CalculateOrderTotalAsync_WithNullOrder_ShouldReturnZero()
    {
        // Arrange
        // random high number
        var orderId = 500;
        
        // Act
        var result = await _orderService.CalculateOrderTotalAsync(orderId);
        
        // Assert
        result.Should().Be(0m);
        
        _mockOrderRepository.Verify(repo => repo.GetByIdAsync(It.Is<int>(o => o == orderId)), Times.Once);
    }
}
