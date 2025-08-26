namespace OrderManagement.IntegrationTests.ItemControllerTests;

public class SearchItemTests : IntegrationTestBase
{
    [Fact]
    public async Task SearchItemsAsync_WithValidName_ShouldGetItemWithName()
    {
        var (_, _itemController ) = CreateController<ItemController, ItemService, ItemRepository>();
        const string itemName = "Pen";

        var matchingItems = await _itemController.SearchItemsAsync(itemName);

        matchingItems.Should().NotBeNull();
        matchingItems.Should().HaveCount(2);
        matchingItems.All(i => i.Name.StartsWith("Pen")).Should().BeTrue();
    }

    [Fact]
    public async Task SearchItemsAsync_WithNonExistingName_ShouldReturnEmptyList()
    {
        var (_, _itemController) = CreateController<ItemController, ItemService, ItemRepository>();
        const string itemName = "fjaeöijfjlieamn     fjei";

        var matchingItems = await _itemController.SearchItemsAsync(itemName);

        matchingItems.Should().HaveCount(0);
        matchingItems.Should().BeEmpty();
    }
}