using ProjectForTesting.Helpers;
using ProjectForTesting.Models;
using ProjectForTesting.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static ProjectForTesting.Models.User;

public class OrderServiceTests
{
    private readonly InMemoryRepository<Tool> toolRepository = new();
    private readonly InMemoryRepository<ToolOrder> toolOrderRepository = new();
    private readonly InMemoryRepository<ToolOrderStatus> toolOrderStatusRepository = new();
    private readonly PrincipalHelper principalHelper = new();
    private readonly UserHelper userHelper = new();
    private readonly Mapper mapper = new();
    private readonly int status = (int)OrderStatus.Active;
    private readonly bool isActive = true;
    private readonly int toolOrderId = 1;

    private readonly ToolService _service;

    public OrderServiceTests()
    {
        _service = new ToolService(
            toolRepository,
            toolOrderRepository,
            toolOrderStatusRepository,
            principalHelper,
            userHelper,
            mapper,
            status,
            isActive,
            toolOrderId
        );
    }

    [Fact]
    public async Task CreateOrderAsSupervisorAsync_ShouldCreateOrder_WhenDataIsValid()
    {
        var toolData = new Tool { RefNum = "T100" };
        principalHelper.SetCurrentUserRoles(Role.Supervisor);

        var resultId = await _service.CreateOrderAsSupervisorAsync(toolData);

        var insertedTool = toolRepository.GetQueryable().FirstOrDefault();
        Assert.NotNull(insertedTool);
        Assert.Equal("T100", insertedTool.RefNum);

        var insertedOrder = toolOrderRepository.GetQueryable().FirstOrDefault();
        Assert.NotNull(insertedOrder);
        Assert.Equal(resultId, insertedOrder.Id);

        var orderStatus = toolOrderStatusRepository.GetQueryable().FirstOrDefault();
        Assert.NotNull(orderStatus);
        Assert.Equal(status, orderStatus.StatusId);
        Assert.Equal(toolOrderId, orderStatus.ToolOrderId);
        Assert.True(orderStatus.IsActive);
    }

    [Fact]
    public async Task CreateOrderAsSupervisorAsync_ShouldThrow_WhenRefNumExists()
    {
        await toolRepository.InsertAsync(new Tool { RefNum = "T200" });
        var toolData = new Tool { RefNum = "T200" };
        principalHelper.SetCurrentUserRoles(Role.Supervisor);

        var ex = await Assert.ThrowsAsync<BadRequestError>(() =>
            _service.CreateOrderAsSupervisorAsync(toolData)
        );
        Assert.Equal(LocalizationResource.RefNum, ex.Message);
    }

    [Fact]
    public async Task CreateOrderAsSupervisorAsync_ShouldThrow_WhenUserIsNotSupervisor()
    {
        var toolData = new Tool { RefNum = "T300" };
        principalHelper.SetCurrentUserRoles(Role.User);

        var ex = await Assert.ThrowsAsync<BadRequestError>(() =>
            _service.CreateOrderAsSupervisorAsync(toolData)
        );
        Assert.Equal(LocalizationResource.InvalidRole, ex.Message);
    }

    [Fact]
    public async Task CreateOrderAsSupervisorAsync_ShouldNotInsert_WhenExceptionThrown()
    {
        await toolRepository.InsertAsync(new Tool { RefNum = "T400" });
        var toolData = new Tool { RefNum = "T400" };
        principalHelper.SetCurrentUserRoles(Role.Supervisor);

        await Assert.ThrowsAsync<BadRequestError>(() =>
            _service.CreateOrderAsSupervisorAsync(toolData)
        );

        var ordersCount = toolOrderRepository.GetQueryable().Count();
        Assert.Equal(0, ordersCount);
    }

    [Fact]
    public async Task DeleteOrderAsync_ShouldNotDelete_WhenOrderIsNull()
    {

    }

    [Fact]
    public async Task DeleteOrderAsync_ShouldNotDelete_WhenStatusIsNotCancelled()
    {

    }

    [Fact]
    public async Task DeleteOrderAsync_ShouldDelete_WhenStatusIsCanceleld_AndOrderExists()
    {

    }
}
