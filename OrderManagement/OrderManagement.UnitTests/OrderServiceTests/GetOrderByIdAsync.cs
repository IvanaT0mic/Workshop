namespace OrderManagement.UnitTests.OrderServiceTests;

public class GetOrderByIdAsync
{
    
    private readonly Mock<IOrderRepository> _mockOrderRepository;
    private readonly Mock<IItemRepository> _mockItemRepository;
    private readonly OrderService _orderService;
    private readonly IFixture _fixture;

    public GetOrderByIdAsync()
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
    public async Task GetOrderByIdAsync_WithIdLessThanZero_ShouldReturnNull()
    {
        // Arrange
        int id = -1;
        Order expectedReturn = null;
        
        // Act & Assert
        var result = await _orderService.GetOrderByIdAsync(id);
        result.Should().BeNull();
        
        _mockOrderRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()),Times.Never);
    }
}