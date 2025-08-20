using OrderManagement.UnitTest.ItemServiceTestsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.UnitTests.ItemServiceTests;

public class GetAllItemsAsync : ItemServiceTestBase
{
    [Fact]
    public async Task GetAllItemsAsync_ShouldGetAllItemsSuccessfully()
    {
        List<Item> expectedItems = [.. _fixture.CreateMany<Item>(3)];

        _mockItemRepository
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(expectedItems);

        var result = await _itemService.GetAllItemsAsync();

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedItems);

        _mockItemRepository.Verify(repo => repo.GetAllAsync(), Times.Once());
    }
}
