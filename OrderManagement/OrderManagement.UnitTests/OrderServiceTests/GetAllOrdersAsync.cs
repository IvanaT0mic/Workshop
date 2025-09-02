namespace OrderManagement.UnitTests.OrderServiceTests;

public class GetAllOrdersAsync
{
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly Mock<IItemRepository> _mockItemRepository;
        private readonly OrderService _orderService;
        private readonly IFixture _fixture;

        public GetAllOrdersAsync()
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
        public async Task GetAllOrdersAsync_ShouldGetOrdersSuccessfully()
        {
            // Arrange
            var order1Id = 1;
            var order2Id = 2;
            var order1 = _fixture.Build<Order>()
                .With(o => o.Id, order1Id)
                .With(o => o.CustomerName, "John Doe")
                .With(o => o.Status, "Pending")
                .With(o => o.TotalAmount, 1050m)
                .Create();
            
            var order2 = _fixture.Build<Order>()
                .With(o => o.Id, order2Id)
                .With(o => o.CustomerName, "Jane Smith")
                .With(o => o.Status, "Processing")
                .With(o => o.TotalAmount, 75m)
                .Create();

            List<Order> expectedOrders = new List<Order>();
            expectedOrders.AddRange(new List<Order> { order1, order2 });

            _mockOrderRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(expectedOrders.ToList);
            
            // Act
            var result = await _orderService.GetAllOrdersAsync();
            
            // Assert
            result.Should().NotBeNullOrEmpty();
            result.ElementAt(0).CustomerName.Should().Be("John Doe");
            result.ElementAt(0).Status.Should().Be("Pending");
            result.ElementAt(0).TotalAmount.Should().Be(1050m);
            result.ElementAt(0).Id.Should().Be(1);
            result.ElementAt(1).CustomerName.Should().Be("Jane Smith");
            result.ElementAt(1).Status.Should().Be("Processing");
            result.ElementAt(1).TotalAmount.Should().Be(75m);
            result.ElementAt(1).Id.Should().Be(2);
            
            _mockOrderRepository.Verify(repo => repo.GetAllAsync(),Times.Once);


        }
}