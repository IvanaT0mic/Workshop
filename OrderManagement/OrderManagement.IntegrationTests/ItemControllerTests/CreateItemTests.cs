namespace OrderManagement.IntegrationTests.ItemControllerTests;

public class CreateItemTests : IntegrationTestBase
{
    [Fact]
    public async Task CreateItemAsync_WithValidItem_ShouldAddItem()
    {
        var (_testDb, _itemController) = CreateController<ItemController, ItemService, ItemRepository>();

        var newItem = _fixture.Build<Item>()
            .With(i => i.Id, _testDb.GetNextItemId())
            .Create();

        var createdItem = await _itemController.CreateItemAsync(newItem);

        createdItem.Should().NotBeNull();
        _testDb.Items.Should().ContainEquivalentOf(newItem);
    }

    [Fact]
    public async Task CreateItemAsync_WithNullItem_ShouldThrow()
    {
        var (_testDb, _itemController) = CreateController<ItemController, ItemService, ItemRepository>();

        var invalidItem = _fixture.Build<Item>()
            .With(i => i.Id, _testDb.GetNextItemId())
            .With(i => i.Name, "    ")
            .Create();

        var error = await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _itemController.CreateItemAsync(invalidItem)
        );
        error.Message.Should().BeEquivalentTo("Item name is required.");
    }
}