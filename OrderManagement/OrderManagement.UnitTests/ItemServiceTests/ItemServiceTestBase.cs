using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.UnitTest.ItemServiceTestsBase;

public abstract class ItemServiceTestBase
{
    protected readonly Mock<IItemRepository> _mockItemRepository;
    protected readonly ItemService _itemService;
    protected readonly IFixture _fixture;

    protected ItemServiceTestBase()
    {
        _mockItemRepository = new Mock<IItemRepository>();
        _itemService = new ItemService(_mockItemRepository.Object);
        _fixture = new Fixture();

        // Configure AutoFixture to handle circular references
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }
}
