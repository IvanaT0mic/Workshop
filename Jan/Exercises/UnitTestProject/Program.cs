using ProjectForTesting.Models;
using ProjectForTesting.Services;

OrderService orderService = new();
Tool tool1 = new();
Tool tool2 = new();

try
{
    int toolOrderId = await orderService.CreateOrderAsSupervisorAsync(tool1);
    int toolOrderId2 = await orderService.CreateOrderAsSupervisorAsync(tool2);
    Console.WriteLine($"Tool order was created successfully. ToolOrderId: {toolOrderId}");
    Console.WriteLine($"Tool order was created successfully. ToolOrderId: {toolOrderId2}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error while creating the order: {ex.Message}");
}

int orderIdToDelete = 0;
try
{
    await orderService.DeleteAsync(orderIdToDelete);
    Console.WriteLine($"Order with Id {orderIdToDelete} was deleted successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error while deleting the order: {ex.Message}");
}

Console.WriteLine("Done. Press any key to exit.");
Console.ReadKey();