using OrderManagement.UnitTest.ItemServiceTestsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.UnitTests.ItemServiceTests;

public class DeleteItemAsync : ItemServiceTestBase
{
    [Fact]
    public async Task DeleteItemAsync_WithValidId_ShouldDeleteItem()
    {
        const int id = 1;
        const bool expectedResult = true;

        _mockItemRepository
            .Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedResult);

        var result = await _itemService.DeleteItemAsync(id);

        result.Should().BeTrue();

        _mockItemRepository.Verify(repo => repo.DeleteAsync(It.Is<int>(i =>
            i == 1
        )), Times.Once());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public async Task DeleteItemAsync_WithInvalidId_ShouldNotDelete(int invalidId)
    {
        var result = await _itemService.DeleteItemAsync(invalidId);

        result.Should().BeFalse();

        _mockItemRepository.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Never());
    }
}
