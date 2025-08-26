namespace OrderManagement.IntegrationTests.ItemControllerTests;

public class IsItemInStockAsync : IntegrationTestBase
{
    [Fact]
    public async Task CheckStock_WithSufficientQuantity_ShouldReturnTrue()
    {
        var (_testDb, _itemController) = CreateController<ItemController, ItemService, ItemRepository>();

        var item = _fixture.Build<Item>()
            .With(i => i.Id, _testDb.GetNextItemId())
            .With(i => i.StockQuantity, 10)
            .Create();

        _testDb.Items.Add(item);

        var result = await _itemController.CheckStock(item.Id, 5);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task CheckStock_WithInsufficientQuantity_ShouldReturnFalse()
    {
        var (_testDb, _itemController) = CreateController<ItemController, ItemService, ItemRepository>();

        var item = _fixture.Build<Item>()
            .With(i => i.Id, _testDb.GetNextItemId())
            .With(i => i.StockQuantity, 2)
            .Create();

        _testDb.Items.Add(item);

        var result = await _itemController.CheckStock(item.Id, 5);

        result.Should().BeFalse();
    }
}
