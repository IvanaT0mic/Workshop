using ProjectForTesting.Helpers;
using ProjectForTesting.Models;
using ProjectForTesting.Repositories;
using System.Transactions;

namespace ProjectForTesting.Services;

internal class OrderService
{
    private readonly InMemoryRepository<Tool> toolRepository = new();
    private readonly InMemoryRepository<ToolOrder> toolOrderRepository = new();
    private readonly InMemoryRepository<ToolOrderStatus> toolOrderStatusRepository = new();
    private readonly InMemoryRepository<Order> orderRepository = new();
    private readonly InMemoryRepository<OrderHistory> orderHistoryRepository = new();
    private readonly InMemoryRepository<OrderComponent> orderComponentRepository = new();
    private readonly InMemoryRepository<UserFavoriteOrder> userFavoriteOrderRepository = new();

    private readonly PrincipalHelper principalHelper = new();
    private readonly UserHelper userHelper = new();

    private readonly Mapper mapper = new();

    private readonly int status = (int)OrderStatus.Active;
    private readonly bool isActive = true;
    private readonly int toolOrderId = 1;

    public async Task<int> CreateOrderAsSupervisorAsync(Tool data)
    {
        var doesRefIdExists = await toolRepository.GetQueryable().AnyAsync(x => x.RefNum == data.RefNum);
        if (doesRefIdExists)
        {
            throw new BadRequestError(LocalizationResource.RefNum);
        }

        var currentUser = principalHelper.GetCurrent();
        if (!currentUser.Roles.Contains(Role.Supervisor))
        {
            throw new BadRequestError(LocalizationResource.InvalidRole);
        }

        Tool tool = new()
        {
            RefNum = data.RefNum
        };

        var toolOrder = mapper.Map<ToolOrder>(data);

        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var toolDb = await toolRepository.InsertAsync(tool);

        toolOrder.ToolId = toolDb.Id;
        var toolOrderDb = await toolOrderRepository.InsertAsync(toolOrder);

        ToolOrderStatus orderStatus = new()
        {
            StatusId = status,
            ChangedBy = await userHelper.GetCurrentUserIdAsync(),
            ChangeDate = DateTime.Now,
            IsActive = isActive,
            ToolOrderId = toolOrderId
        };

        await toolOrderStatusRepository.InsertAsync(orderStatus);

        transactionScope.Complete();
        transactionScope.Dispose();

        return toolOrderDb.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var orderFromDb = await orderRepository
            .GetQueryable()
            .Select(x =>
                new Order()
                {
                    Id = x.Id,
                    Status = x.Status,
                    OrderComponents = x.OrderComponents,
                    OrderHistories = x.OrderHistories,
                    UserFavoriteOrders = x.UserFavoriteOrders,
                })
            .FirstOrDefaultAsync(x => x.Id == id);

        if (orderFromDb == null)
        {
            throw new BadRequestError(LocalizationResource.OrderNotFound, id);
        }

        if (orderFromDb.Status != OrderStatus.Cancelled)
        {
            throw new BadRequestError(LocalizationResource.OrderInvalidStatus, id);
        }

        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await orderHistoryRepository.DeleteRangeAsync(orderFromDb.OrderHistories);
        await orderComponentRepository.DeleteRangeAsync(orderFromDb.OrderComponents);
        await userFavoriteOrderRepository.DeleteRangeAsync(orderFromDb.UserFavoriteOrders);
        await orderRepository.DeleteAsync(orderFromDb);

        transactionScope.Complete();
    }

    class Mapper()
    {

    }
}