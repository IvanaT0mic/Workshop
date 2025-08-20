using OrderManagement.UnitTest.ItemServiceTestsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.UnitTests.ItemServiceTests;

public class GetItemByIdAsync : ItemServiceTestBase
{
    [Fact]
    public async Task GetItemByIdAsync_WithValidId_ShouldGetItemById()
    {
        Item expectedItem = _fixture.Create<Item>();

        _mockItemRepository
            .Setup(repo => repo.GetByIdAsync(expectedItem.Id))
            .ReturnsAsync(expectedItem);

        var result = await _itemService.GetItemByIdAsync(expectedItem.Id);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedItem);

        _mockItemRepository.Verify(repo => repo.GetByIdAsync(expectedItem.Id), Times.Once);
    }

    [Fact]
    public async Task GetItemByIdAsync_WhenItemDoesNotExist_ShouldReturnNull()
    {
        int validId = 42;

        _mockItemRepository
            .Setup(repo => repo.GetByIdAsync(validId))
            .ReturnsAsync((Item?)null);

        var result = await _itemService.GetItemByIdAsync(validId);

        result.Should().BeNull();

        _mockItemRepository.Verify(repo => repo.GetByIdAsync(validId), Times.Once);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    [InlineData(-17)]
    public async Task GetItemByIdAsync_WithInvalidId(int invalidId)
    {
        var result = await _itemService.GetItemByIdAsync(invalidId);

        result.Should().BeNull();

        _mockItemRepository.Verify(repo => repo.GetByIdAsync(invalidId), Times.Never);
    }
}
