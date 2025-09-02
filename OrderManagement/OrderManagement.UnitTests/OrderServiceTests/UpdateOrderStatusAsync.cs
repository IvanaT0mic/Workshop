namespace OrderManagement.UnitTests.OrderServiceTests;

public class UpdateOrderStatusAsync
{
    
    private readonly Mock<IOrderRepository> _mockOrderRepository;
    private readonly Mock<IItemRepository> _mockItemRepository;
    private readonly OrderService _orderService;
    private readonly IFixture _fixture;

    public UpdateOrderStatusAsync()
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

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task UpdateOrderStatusAsync_WithIdLessThanOrEqualsZero_ShouldBeNull(int orderId)
    {
        // Arrange
        string status = "Processing";
        
        // Act
        var result = await _orderService.UpdateOrderStatusAsync(orderId, status);
        result.Should().BeNull();
        
        _mockOrderRepository.Verify(repo => repo.UpdateAsync(
            It.IsAny<int>(),It.IsAny<Order>()),Times.Never);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task UpdateOrderStatusAsync_WithEmptyStatusName_ShouldBeNull(string status)
    {
        // Arrange
        int orderId = 1;
        
        // Act
        var result = await _orderService.UpdateOrderStatusAsync(orderId, status);
        
        // Assert
        result.Should().BeNull();
        
        _mockOrderRepository.Verify(repo => repo.UpdateAsync(
            It.IsAny<int>(),It.IsAny<Order>()),Times.Never);
    }

    [Fact]
    public async Task UpdateOrderStatusAsync_WithInvalidStatusName_ShouldThrowArgumentException()
    {
        // Arrange
        var orderId = 1;
        var invalidOrderStatus = "Inv@lid!";
        
        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _orderService.UpdateOrderStatusAsync(orderId, invalidOrderStatus));
        
        // Assert
        exception.Message.Should().Be("Invalid order status.");
        
        _mockOrderRepository.Verify(repo => repo.UpdateAsync(
            It.IsAny<int>(),It.IsAny<Order>()),Times.Never);
    }

    [Fact]
    public async Task UpdateOrderStatusAsync_WithNullOrder_ShouldReturnNull()
    {
        // Arrange
        // random high number
        var orderId = 500;
        var orderStatus = "Processing";
        
        // Act
        var result = await _orderService.UpdateOrderStatusAsync(orderId, orderStatus);
        
        // Assert
        result.Should().BeNull();
        
        _mockOrderRepository.Verify(repo => repo.UpdateAsync(
            It.IsAny<int>(),It.IsAny<Order>()),Times.Never);
    }
}