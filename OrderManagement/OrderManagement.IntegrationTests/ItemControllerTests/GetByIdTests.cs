namespace OrderManagement.IntegrationTests.ItemControllerTests;

public class GetByIdTests : IntegrationTestBase
{
    [Fact]
    public async Task GetItemByIdAsync_WithExistingItem_ShouldReturnItem()
    {
        var (_testDb, _itemController) = CreateController<ItemController, ItemService, ItemRepository>();

        var newItem = _fixture.Build<Item>()
        .With(i => i.Name, "Wireless Headphones")
        .With(i => i.Price, 199.99m)
        .With(i => i.StockQuantity, 15)
        .With(i => i.Id, _testDb.GetNextItemId())
        .Create();

        _testDb.Items.Add(newItem);

        // Act
        var retrievedItem = await _itemController.GetItemByIdAsync(newItem.Id);

        // Assert
        retrievedItem.Should().NotBeNull();
        retrievedItem!.Id.Should().Be(newItem.Id);
        retrievedItem.Name.Should().Be("Wireless Headphones");
        retrievedItem.Price.Should().Be(199.99m);
        retrievedItem.StockQuantity.Should().Be(15);
    }

    [Fact]
    public async Task GetItemByIdAsync_WithNonExistentItem_ShouldReturnNull()
    {
        var (_, _itemController) = CreateController<ItemController, ItemService, ItemRepository>();

        // Act
        var retrievedItem = await _itemController.GetItemByIdAsync(99999);

        // Assert
        retrievedItem.Should().BeNull();
    }
}
