using OrderManagement.Repository;
using OrderManagement.UnitTest.ItemServiceTestsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.UnitTests.ItemServiceTests;

public class CreateItemTests : ItemServiceTestBase
{
    [Fact]
    public async Task CreateItemAsync_WithValidItem_ShouldCreateSuccessfully()
    {
        var item = _fixture.Build<Item>()
            .With(i => i.Name, "Hammer")
            .With(i => i.Price, 1.50m)
            .With(i => i.StockQuantity, 12)
            .Without(i => i.Id)
            .Create();

        var expectedItem = _fixture.Build<Item>()
            .With(i => i.Id, 1)
            .With(i => i.Name, "Hammer")
            .With(i => i.Price, 1.50m)
            .With(i => i.StockQuantity, 12)
            .Create();

        _mockItemRepository
            .Setup(repo => repo.CreateAsync(It.IsAny<Item>()))
            .ReturnsAsync(expectedItem);

        var result = await _itemService.CreateItemAsync(item);

        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Hammer");
        result.Price.Should().Be(1.50m);
        result.StockQuantity.Should().Be(12);

        _mockItemRepository.Verify(repo => repo.CreateAsync(It.Is<Item>(i =>
            i.Name == "Hammer" &&
            i.Price == 1.50m &&
            i.StockQuantity == 12
        )), Times.Once());
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public async Task CreateItemAsync_WithInvalidName_ShouldThrowArgumentException(string? name)
    {
        var item = _fixture.Build<Item>()
            .With(i => i.Name, name)
            .Create();

        await AssertThrowsAndVerifies(item, "Item name is required.");
    }

    [Fact]
    public async Task CreateItemAsync_WithInvalidPrice_ShouldThrowArgumentException()
    {
        var item = _fixture.Build<Item>()
            .With(i => i.Name, "Hammer")
            .With(i => i.Price, -5)
            .Create();

        await AssertThrowsAndVerifies(item, "Item price cannot be negative.");
    }

    [Fact]
    public async Task CreateItemAsync_WithInvalidStockQuantity_ShouldThrowArgumentException()
    {
        var item = _fixture.Build<Item>()
            .With(i => i.Name, "Hammer")
            .With(i => i.Price, 51)
            .With(i => i.StockQuantity, -3)
            .Create();

        await AssertThrowsAndVerifies(item, "Stock quantity cannot be negative.");
    }

    private async Task AssertThrowsAndVerifies(Item item, string expectedMessage)
    {
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _itemService.CreateItemAsync(item));
        exception.Message.Should().Be(expectedMessage);

        _mockItemRepository.Verify(repo => repo.CreateAsync(It.IsAny<Item>()), Times.Never);
    }
}