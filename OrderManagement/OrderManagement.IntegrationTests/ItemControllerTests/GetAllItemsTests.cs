namespace OrderManagement.IntegrationTests.ItemControllerTests;

public class GetAllItemsTests : IntegrationTestBase
{
    [Fact]
    public async Task GetAllAsync_WithDataInDB_ShouldReturnAllItems()
    {
        var (_testDb, _itemController) = CreateController<ItemController, ItemService, ItemRepository>();

        var newItem = _fixture.Build<Item>()
            .With(i => i.Id, _testDb.GetNextItemId)
            .With(i => i.Name, "Item")
            .With(i => i.Price, 10)
            .With(i => i.StockQuantity, 5)
            .Create();

        _testDb.Items.Add(newItem);
        _testDb.Items.Should().Contain(newItem);

        var items = await _itemController.GetAllItems();

        items.Should().NotBeNull();
        items.Count().Should().Be(_testDb.Items.Count);
        items.Select(i => i.Id).Should().BeEquivalentTo(_testDb.Items.Select(i => i.Id));
    }

    [Fact]
    public async Task GetAllAsync_WithEmptyDB_ShouldReturnEmptyList()
    {
        var (_testDb, _itemController) = CreateController<ItemController, ItemService, ItemRepository>();
        _testDb.Items.Clear();

        var items = await _itemController.GetAllItems();

        items.Should().BeEmpty();
    }
}
