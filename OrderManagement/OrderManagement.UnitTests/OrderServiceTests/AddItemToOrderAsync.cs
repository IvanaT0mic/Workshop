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

    [Fact]
    public async Task AddItemToOrderAsync_WithValidParameters_ShouldAddItemSuccessfully()
    {
        // Arrange

        var orderId = 1;
        var itemId = 1;
        var quantity = 1;
        var totalAmount = 5.5m;

        var itemToAdd = _fixture.Build<Item>()
            .With(o => o.Id, itemId)
            .With(o => o.Name, "TestItem")
            .With(o => o.Price, totalAmount)
            .With(o => o.StockQuantity, 10)
            .Create();

        var order = _fixture.Build<Order>()
                .With(o => o.Id, 1)
                .With(o => o.CustomerName, "John Doe")
                .With(o => o.Status, "Pending")
                .Create();

        var orderItem = new OrderItem
        {
            Id = 1,
            Item = itemToAdd,
            ItemId = itemId,
            Order = order,
            OrderId = orderId,
            Quantity = quantity,
            UnitPrice = totalAmount
        };

        var orderItemList = new List<OrderItem>{ orderItem };

        var expectedOrder = _fixture.Build<Order>()
            .With(o => o.Id, 1)
            .With(o => o.CustomerName, "John Doe")
            .With(o => o.Status, "Pending")
            .With(o => o.TotalAmount, totalAmount)
            .With(o => o.OrderItems, orderItemList)
            .Create();

        _mockOrderRepository
            .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(order);

        _mockItemRepository
            .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(itemToAdd);

        _mockOrderRepository
            .Setup(repo => repo.AddItemToOrderAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(expectedOrder);
        // Act
        var result = await _orderService.AddItemToOrderAsync(orderId, itemId, quantity);

        // Assert
        result.Should().NotBeNull();
        result.OrderItems.Count.Should().Be(1);

        _mockOrderRepository.Verify(repo => repo.AddItemToOrderAsync(
            It.Is<int>(o => o == orderId),
            It.Is<int>(o => o == itemId),
            It.Is<int>(o => o == 1)), Times.Once);

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