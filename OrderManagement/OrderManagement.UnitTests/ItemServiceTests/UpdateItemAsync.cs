using OrderManagement.UnitTest.ItemServiceTestsBase;

namespace OrderManagement.UnitTests.ItemServiceTests;

public class UpdateItemAsync : ItemServiceTestBase
{
    [Fact]
    public async Task UpdateItemAsync_WithValidItemAndId_ShouldUpdateSuccessfully()
    {
        const int validId = 1;
        Item expectedItem = _fixture.Build<Item>()
            .With(i => i.Name, "Sword")
            .With(i => i.Price, 5)
            .With(i => i.StockQuantity, 10)
            .Create();

        _mockItemRepository
            .Setup(repo => repo.UpdateAsync(validId, It.IsAny<Item>()))
            .ReturnsAsync(expectedItem);

        var result = await _itemService.UpdateItemAsync(validId, expectedItem);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedItem);

        _mockItemRepository.Verify(repo => repo.UpdateAsync(
            It.Is<int>(id => id == validId),
            It.Is<Item>(i =>
                i.Name == "Sword" &&
                i.Price == 5 &&
                i.StockQuantity == 10
            )), Times.Once);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-99)]
    public async Task UpdateItemAsync_WithInvalidId_ShouldReturnNull(int invalidId)
    {
        Item item = _fixture.Build<Item>()
            .With(i => i.Name, "Sword")
            .With(i => i.Price, 10)
            .With(i => i.StockQuantity, 5)
            .Create();

        var result = await _itemService.UpdateItemAsync(invalidId, item);

        result.Should().BeNull();
        _mockItemRepository.Verify(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<Item>()), Times.Never);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task UpdateItemAsync_WithInvalidName_ShouldThrowArgumentException(string invalidName)
    {
        const int validId = 1;
        Item item = _fixture.Build<Item>()
            .With(i => i.Name, invalidName)
            .With(i => i.Price, 10)
            .With(i => i.StockQuantity, 5)
            .Create();

        await AssertThrowsAndVerifies(validId, item, "Item name is required.");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public async Task UpdateItemAsync_WithNegativePrice_ShouldThrowArgumentException(int invalidPrice)
    {
        const int validId = 1;
        Item item = _fixture.Build<Item>()
            .With(i => i.Name, "Sword")
            .With(i => i.Price, invalidPrice)
            .With(i => i.StockQuantity, 5)
            .Create();

        await AssertThrowsAndVerifies(validId, item, "Item price cannot be negative.");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-50)]
    public async Task UpdateItemAsync_WithNegativeStockQuantity_ShouldThrowArgumentException(int invalidStock)
    {
        const int validId = 1;
        Item item = _fixture.Build<Item>()
            .With(i => i.Name, "Sword")
            .With(i => i.Price, 10)
            .With(i => i.StockQuantity, invalidStock)
            .Create();

        await AssertThrowsAndVerifies(validId, item, "Stock quantity cannot be negative.");
    }

    private async Task AssertThrowsAndVerifies(int id, Item item, string expectedMessage)
    {
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _itemService.UpdateItemAsync(id, item));
        exception.Message.Should().Be(expectedMessage);

        _mockItemRepository.Verify(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<Item>()), Times.Never);
    }
}
