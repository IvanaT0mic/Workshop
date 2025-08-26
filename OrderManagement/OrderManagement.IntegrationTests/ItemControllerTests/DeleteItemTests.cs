namespace OrderManagement.IntegrationTests.ItemControllerTests;

public class DeleteItemTests : IntegrationTestBase
{
    [Fact]
    public async Task DeleteItemAsyn_WithValidId_ShouldDeleteItem()
    {
        var (_testDb, _itemController) = CreateController<ItemController, ItemService, ItemRepository>();

        var newItem = _fixture.Build<Item>()
            .With(i => i.Id, _testDb.GetNextItemId())
            .Create();

        _testDb.Items.Add(newItem);
        _testDb.Items.Should().Contain(newItem);

        var createdItem = _testDb.Items.Find(i => i.Id == newItem.Id);

        var isDeleted = await _itemController.DeleteItemAsync(newItem.Id);

        isDeleted.Should().BeTrue();
        _testDb.Items.Should().NotContain(createdItem!);
    }

    [Fact]
    public async Task DeleteItemAsyn_WithInvalidId_ShouldNotDeleteItem()
    {
        var (_testDb, _itemController) = CreateController<ItemController, ItemService, ItemRepository>();

        _testDb.Items.Clear();

        var isDeleted = await _itemController.DeleteItemAsync(1);

        isDeleted.Should().BeFalse();
    }
}
