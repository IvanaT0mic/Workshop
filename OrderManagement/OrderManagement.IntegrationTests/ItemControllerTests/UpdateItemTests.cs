namespace OrderManagement.IntegrationTests.ItemControllerTests;

public class UpdateItemTests : IntegrationTestBase
{
    [Fact]
    public async Task UpdateItemAsync_WithValidId_ShouldUpdateItem()
    {
        var (_testDb, _itemController) = CreateController<ItemController, ItemService, ItemRepository>();

        var existingItem = _fixture.Build<Item>()
            .With(i => i.Id, _testDb.GetNextItemId())
            .With(i => i.Name, "OldName")
            .With(i => i.StockQuantity, 12)
            .Create();

        _testDb.Items.Add(existingItem);

        var updatedItem = _fixture.Build<Item>()
            .With(i => i.Id, existingItem.Id)
            .With(i => i.Name, "NewName")
            .With(i => i.StockQuantity, 5)
            .Create();

        var result = await _itemController.UpdateItem(existingItem.Id, updatedItem);

        result.Should().NotBeNull();
        result!.Name.Should().Be("NewName");
        result!.StockQuantity.Should().Be(5);
    }

    [Fact]
    public async Task UpdateItemAsync_WithInvalidId_ShouldReturnNull()
    {
        var invalidId = 99999;
        var (_testDb, _itemController) = CreateController<ItemController, ItemService, ItemRepository>();

        var updatedItem = _fixture.Build<Item>()
            .With(i => i.Id, invalidId)
            .Create();

        var result = await _itemController.UpdateItem(invalidId, updatedItem);

        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateItemAsync_WithInvalidName_ShouldThrowException()
    {
        var (_testDb, _itemController) = CreateController<ItemController, ItemService, ItemRepository>();

        var updatedItem = _fixture.Build<Item>()
            .With(i => i.Id, 1)
            .With(i => i.Name, "      ")
            .Create();

        var error = await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _itemController.UpdateItem(updatedItem.Id, updatedItem)
        );

        error.Message.Should().BeEquivalentTo("Item name is required.");
    }
}
