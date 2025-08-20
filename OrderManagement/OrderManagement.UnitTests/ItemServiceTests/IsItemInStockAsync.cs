using OrderManagement.UnitTest.ItemServiceTestsBase;

namespace OrderManagement.UnitTests.ItemServiceTests;

public class IsItemInStockAsync : ItemServiceTestBase
{
    [Fact]
    public async Task IsItemInStockAsync_WhenStockIsEqualToQuantity_ShouldReturnTrue()
    {
        var item = _fixture.Build<Item>()
            .With(i => i.StockQuantity, 5)
            .Create();

        _mockItemRepository
            .Setup(repo => repo.GetByIdAsync(item.Id))
            .ReturnsAsync(item);

        var result = await _itemService.IsItemInStockAsync(item.Id, 5);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task IsItemInStockAsync_WhenStockIsGreaterThanQuantity_ShouldReturnTrue()
    {
        var item = _fixture.Build<Item>()
            .With(i => i.StockQuantity, 10)
            .Create();

        _mockItemRepository
            .Setup(repo => repo.GetByIdAsync(item.Id))
            .ReturnsAsync(item);

        var result = await _itemService.IsItemInStockAsync(item.Id, 5);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task IsItemInStockAsync_WhenItemDoesNotExist_ShouldReturnFalse()
    {
        _mockItemRepository
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync((Item?)null);

        var result = await _itemService.IsItemInStockAsync(1, 5);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task IsItemInStockAsync_WhenStockIsLessThanQuantity_ShouldReturnFalse()
    {
        var item = _fixture.Build<Item>()
            .With(i => i.StockQuantity, 3)
            .Create();

        _mockItemRepository
            .Setup(repo => repo.GetByIdAsync(item.Id))
            .ReturnsAsync(item);

        var result = await _itemService.IsItemInStockAsync(item.Id, 5);

        result.Should().BeFalse();
    }
}
