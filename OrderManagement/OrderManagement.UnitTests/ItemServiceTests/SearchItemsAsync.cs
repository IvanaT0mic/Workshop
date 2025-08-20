using OrderManagement.UnitTest.ItemServiceTestsBase;

namespace OrderManagement.UnitTests.ItemServiceTests;

public class SearchItemsAsync : ItemServiceTestBase
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task SearchItemsAsync_WithEmptyOrNullSearchTerm_ShouldGetAllItems(string? searchTerm)
    {
        List<Item> expectedItems = _fixture.CreateMany<Item>(3).ToList();

        _mockItemRepository
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(expectedItems);

        var result = await _itemService.SearchItemsAsync(searchTerm!);

        result.Should().BeEquivalentTo(expectedItems);
        _mockItemRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        _mockItemRepository.Verify(repo => repo.SearchAsync(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task SearchItemsAsync_WithValidSearchTerm_ShouldCallRepositorySearch()
    {
        List<Item> expectedItems = _fixture.CreateMany<Item>(2).ToList();

        _mockItemRepository
            .Setup(repo => repo.SearchAsync("Hammer"))
            .ReturnsAsync(expectedItems);

        var result = await _itemService.SearchItemsAsync("Hammer");

        result.Should().BeEquivalentTo(expectedItems);
        _mockItemRepository.Verify(repo => repo.SearchAsync("Hammer"), Times.Once);
        _mockItemRepository.Verify(repo => repo.GetAllAsync(), Times.Never);
    }
}
