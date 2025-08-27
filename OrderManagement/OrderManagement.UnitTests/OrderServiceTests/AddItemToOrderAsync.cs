namespace OrderManagement.UnitTests.OrderServiceTests;

public class AddItemToOrderAsync
{
    
    private readonly Mock<IOrderRepository> _mockOrderRepository;
    private readonly Mock<IItemRepository> _mockItemRepository;
    private readonly OrderService _orderService;
    private readonly IFixture _fixture;

    public AddItemToOrderAsync()
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
    public async Task AddItemToOrderAsync_WithOrderIdLessThanOrEqualsZero_ShouldReturnNull(int orderId)
    {
        // Assert
        int itemId = 1;
        int quantity = 1;
        
        // Act
        var result = await _orderService.AddItemToOrderAsync(orderId, itemId, quantity);
        result.Should().BeNull();
        
        _mockOrderRepository.Verify(repo => repo.AddItemToOrderAsync
            (It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>()),Times.Never);
        
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task AddItemToOrderAsync_WithItemIdLessThanOrEqualsZero_ShouldReturnNull(int itemId)
    {
        // Arrange
        int orderId = 1;
        int quantity = 1;
        
        // Act
        var result = await _orderService.AddItemToOrderAsync(orderId, itemId, quantity);
        result.Should().BeNull();
        
        _mockOrderRepository.Verify(repo => repo.AddItemToOrderAsync
            (It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>()),Times.Never);
        
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task AddItemToOrderAsync_WithQuantityLessThanOrEqualsZero_ShouldReturnNull(int quantity)
    {
        // Arrange
        int itemId = 1;
        int orderId = 1;
        
        // Act
        var result = await _orderService.AddItemToOrderAsync(orderId, itemId, quantity);
        result.Should().BeNull();
        
        _mockOrderRepository.Verify(repo => repo.AddItemToOrderAsync
            (It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>()),Times.Never);
        
    }

    [Fact]
    public async Task AddItemToOrderAsync_WithOrderEqualsNull_ShouldThrowArgumentException()
    {
        // Arrange
        
        // random high number
        var orderId = 500;
        var itemId = 1;
        var quantity = 1;
        
        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _orderService.AddItemToOrderAsync(orderId, itemId, quantity));
        exception.Message.Should().Be("Order not found.");
        
        _mockOrderRepository.Verify(repo => repo.AddItemToOrderAsync
            (It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>()),Times.Never);

    }

    [Theory]
    [InlineData("Completed")]
    [InlineData("Cancelled")]
    public async Task AddItemToOrderAsync_WithCompletedOrCancelledOrder_ShouldThrowInvalidOperationException(
        string orderStatus)
    {
        // Arrange
       
        var orderId = 1;
        var itemId = 1;
        var quantity = 1;

        var order = _fixture.Build<Order>()
            .With(o => o.Status, orderStatus)
            .With(o => o.Id, orderId)
            .Create();

        _mockOrderRepository
            .Setup(repo => repo.GetByIdAsync(orderId))
            .ReturnsAsync(order);
        
        
        // Act & Assert
        var exception =
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _orderService.AddItemToOrderAsync(orderId, itemId, quantity));

        exception.Message.Should().Be("Cannot modify completed or cancelled orders.");
        
        _mockOrderRepository.Verify(repo => repo.AddItemToOrderAsync
            (It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>()),Times.Never);
    }
}