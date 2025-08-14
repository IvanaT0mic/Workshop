namespace OrderManagement.IntegrationTests.ItemControllerTests
{
    public class GetByIdTests : IntegrationTestBase
    {
        private readonly ItemController _itemController;

        public GetByIdTests()
        {
            _itemController = GetService<ItemController>();
        }

        [Fact]
        public async Task GetItemByIdAsync_WithExistingItem_ShouldReturnItem()
        {
            // Arrange
            var newItem = _fixture.Build<Item>()
                .With(i => i.Name, "Wireless Headphones")
                .With(i => i.Price, 199.99m)
                .With(i => i.StockQuantity, 15)
                .Without(i => i.Id)
                .Create();

            var createdItem = await _itemController.CreateItemAsync(newItem);

            // Act
            var retrievedItem = await _itemController.GetItemByIdAsync(createdItem.Id);

            // Assert
            retrievedItem.Should().NotBeNull();
            retrievedItem!.Id.Should().Be(createdItem.Id);
            retrievedItem.Name.Should().Be("Wireless Headphones");
            retrievedItem.Price.Should().Be(199.99m);
            retrievedItem.StockQuantity.Should().Be(15);
        }

        [Fact]
        public async Task GetItemByIdAsync_WithNonExistentItem_ShouldReturnNull()
        {
            // Act
            var retrievedItem = await _itemController.GetItemByIdAsync(99999);

            // Assert
            retrievedItem.Should().BeNull();
        }

    }
}
