namespace OrderManagement.UnitTests.OrderServiceTests;

public class GetOrdersByCustomerAsync
{
    private readonly Mock<IOrderRepository> _mockOrderRepository;
    private readonly Mock<IItemRepository> _mockItemRepository;
    private readonly OrderService _orderService;
    private readonly IFixture _fixture;

    public GetOrdersByCustomerAsync()
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
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task GetOrdersByCustomerAsync_WithInvalidCustomerName_ShouldReturnNewList(string customerName)
    {
        // Arrange
        var expectedOrders = new List<Order>();
        
        // Act & Assert
        var result = await _orderService.GetOrdersByCustomerAsync(customerName);
        result.Should().BeEmpty();
        
        _mockOrderRepository.Verify(repo => repo.GetByCustomerAsync(It.IsAny<string>()), Times.Never);
    }
}