using OrderManagement.Controllers;
using OrderManagement.Models;
using OrderManagement.Repository;
using OrderManagement.Repository.Interfaces;
using OrderManagement.Services;
using OrderManagement.Services.Interfaces;

namespace OrderManagement.ConsoleApp
{
    class Program
    {
        private static ItemController _itemController = null!;
        private static OrderController _orderController = null!;

        static async Task Main(string[] args)
        {
            InitializeDependencies();
            
            Console.WriteLine("=== Order Management System ===");
            Console.WriteLine();

            bool running = true;
            while (running)
            {
                ShowMainMenu();
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await ItemManagementMenu();
                            break;
                        case "2":
                            await OrderManagementMenu();
                            break;
                        case "3":
                            running = false;
                            Console.WriteLine("Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private static void InitializeDependencies()
        {
            IItemRepository itemRepository = new ItemRepository();
            IOrderRepository orderRepository = new OrderRepository();
            
            IItemService itemService = new ItemService(itemRepository);
            IOrderService orderService = new OrderService(orderRepository, itemRepository);
            
            _itemController = new ItemController(itemService);
            _orderController = new OrderController(orderService);
        }

        private static void ShowMainMenu()
        {
            Console.WriteLine("=== Main Menu ===");
            Console.WriteLine("1. Item Management");
            Console.WriteLine("2. Order Management");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");
        }

        private static async Task ItemManagementMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Item Management ===");
            Console.WriteLine("1. View All Items");
            Console.WriteLine("2. Get Item by ID");
            Console.WriteLine("3. Create New Item");
            Console.WriteLine("4. Update Item");
            Console.WriteLine("5. Delete Item");
            Console.WriteLine("6. Search Items");
            Console.WriteLine("7. Check Stock");
            Console.WriteLine("8. Back to Main Menu");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ViewAllItems();
                    break;
                case "2":
                    await GetItemById();
                    break;
                case "3":
                    await CreateItem();
                    break;
                case "4":
                    await UpdateItem();
                    break;
                case "5":
                    await DeleteItem();
                    break;
                case "6":
                    await SearchItems();
                    break;
                case "7":
                    await CheckStock();
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private static async Task OrderManagementMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Order Management ===");
            Console.WriteLine("1. View All Orders");
            Console.WriteLine("2. Get Order by ID");
            Console.WriteLine("3. Create New Order");
            Console.WriteLine("4. Update Order");
            Console.WriteLine("5. Delete Order");
            Console.WriteLine("6. Get Orders by Customer");
            Console.WriteLine("7. Add Item to Order");
            Console.WriteLine("8. Remove Item from Order");
            Console.WriteLine("9. Update Order Status");
            Console.WriteLine("10. Get Order Total");
            Console.WriteLine("11. Back to Main Menu");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ViewAllOrders();
                    break;
                case "2":
                    await GetOrderById();
                    break;
                case "3":
                    await CreateOrder();
                    break;
                case "4":
                    await UpdateOrder();
                    break;
                case "5":
                    await DeleteOrder();
                    break;
                case "6":
                    await GetOrdersByCustomer();
                    break;
                case "7":
                    await AddItemToOrder();
                    break;
                case "8":
                    await RemoveItemFromOrder();
                    break;
                case "9":
                    await UpdateOrderStatus();
                    break;
                case "10":
                    await GetOrderTotal();
                    break;
                case "11":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private static async Task ViewAllItems()
        {
            Console.WriteLine("\n=== All Items ===");
            var items = await _itemController.GetAllItemsAsync();
            
            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Price: ${item.Price:F2}, Stock: {item.StockQuantity}, Category: {item.Category}");
                Console.WriteLine($"  Description: {item.Description}");
                Console.WriteLine();
            }
        }

        private static async Task GetItemById()
        {
            Console.Write("Enter Item ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var item = await _itemController.GetItemByIdAsync(id);
                if (item != null)
                {
                    Console.WriteLine($"\nID: {item.Id}");
                    Console.WriteLine($"Name: {item.Name}");
                    Console.WriteLine($"Description: {item.Description}");
                    Console.WriteLine($"Price: ${item.Price:F2}");
                    Console.WriteLine($"Stock: {item.StockQuantity}");
                    Console.WriteLine($"Category: {item.Category}");
                }
                else
                {
                    Console.WriteLine("Item not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        private static async Task CreateItem()
        {
            Console.WriteLine("\n=== Create New Item ===");
            
            Console.Write("Name: ");
            var name = Console.ReadLine() ?? "";
            
            Console.Write("Description: ");
            var description = Console.ReadLine() ?? "";
            
            Console.Write("Price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price format.");
                return;
            }
            
            Console.Write("Stock Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int stock))
            {
                Console.WriteLine("Invalid stock format.");
                return;
            }
            
            Console.Write("Category: ");
            var category = Console.ReadLine() ?? "";

            var item = new Item
            {
                Name = name,
                Description = description,
                Price = price,
                StockQuantity = stock,
                Category = category
            };

            var createdItem = await _itemController.CreateItemAsync(item);
            Console.WriteLine($"Item created successfully with ID: {createdItem.Id}");
        }

        private static async Task UpdateItem()
        {
            Console.Write("Enter Item ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            var existingItem = await _itemController.GetItemByIdAsync(id);
            if (existingItem == null)
            {
                Console.WriteLine("Item not found.");
                return;
            }

            Console.WriteLine($"\nCurrent Item: {existingItem.Name}");
            Console.Write($"New Name (current: {existingItem.Name}): ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) name = existingItem.Name;

            Console.Write($"New Description (current: {existingItem.Description}): ");
            var description = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(description)) description = existingItem.Description;

            Console.Write($"New Price (current: {existingItem.Price}): ");
            var priceInput = Console.ReadLine();
            decimal price = existingItem.Price;
            if (!string.IsNullOrWhiteSpace(priceInput) && !decimal.TryParse(priceInput, out price))
            {
                Console.WriteLine("Invalid price format.");
                return;
            }

            Console.Write($"New Stock (current: {existingItem.StockQuantity}): ");
            var stockInput = Console.ReadLine();
            int stock = existingItem.StockQuantity;
            if (!string.IsNullOrWhiteSpace(stockInput) && !int.TryParse(stockInput, out stock))
            {
                Console.WriteLine("Invalid stock format.");
                return;
            }

            Console.Write($"New Category (current: {existingItem.Category}): ");
            var category = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(category)) category = existingItem.Category;

            var updatedItem = new Item
            {
                Name = name,
                Description = description,
                Price = price,
                StockQuantity = stock,
                Category = category
            };

            var result = await _itemController.UpdateItemAsync(id, updatedItem);
            if (result != null)
            {
                Console.WriteLine("Item updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update item.");
            }
        }

        private static async Task DeleteItem()
        {
            Console.Write("Enter Item ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var result = await _itemController.DeleteItemAsync(id);
                Console.WriteLine(result ? "Item deleted successfully." : "Failed to delete item or item not found.");
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        private static async Task SearchItems()
        {
            Console.Write("Enter search term: ");
            var searchTerm = Console.ReadLine() ?? "";
            
            var items = await _itemController.SearchItemsAsync(searchTerm);
            
            Console.WriteLine($"\n=== Search Results for '{searchTerm}' ===");
            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Price: ${item.Price:F2}, Stock: {item.StockQuantity}");
            }
        }

        private static async Task CheckStock()
        {
            Console.Write("Enter Item ID: ");
            if (!int.TryParse(Console.ReadLine(), out int itemId))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid quantity format.");
                return;
            }

            var inStock = await _itemController.CheckStockAsync(itemId, quantity);
            Console.WriteLine(inStock ? "Item is in stock." : "Insufficient stock or item not found.");
        }

        private static async Task ViewAllOrders()
        {
            Console.WriteLine("\n=== All Orders ===");
            var orders = await _orderController.GetAllOrdersAsync();
            
            foreach (var order in orders)
            {
                Console.WriteLine($"ID: {order.Id}, Customer: {order.CustomerName}, Date: {order.OrderDate:yyyy-MM-dd}");
                Console.WriteLine($"Status: {order.Status}, Total: ${order.TotalAmount:F2}");
                Console.WriteLine($"Items: {order.OrderItems.Count}");
                Console.WriteLine();
            }
        }

        private static async Task GetOrderById()
        {
            Console.Write("Enter Order ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var order = await _orderController.GetOrderByIdAsync(id);
                if (order != null)
                {
                    Console.WriteLine($"\nOrder ID: {order.Id}");
                    Console.WriteLine($"Customer: {order.CustomerName}");
                    Console.WriteLine($"Date: {order.OrderDate:yyyy-MM-dd HH:mm}");
                    Console.WriteLine($"Status: {order.Status}");
                    Console.WriteLine($"Total: ${order.TotalAmount:F2}");
                    Console.WriteLine("\nOrder Items:");
                    foreach (var item in order.OrderItems)
                    {
                        Console.WriteLine($"  - {item.Item?.Name} x{item.Quantity} @ ${item.UnitPrice:F2} = ${item.TotalPrice:F2}");
                    }
                }
                else
                {
                    Console.WriteLine("Order not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        private static async Task CreateOrder()
        {
            Console.WriteLine("\n=== Create New Order ===");
            
            Console.Write("Customer Name: ");
            var customerName = Console.ReadLine() ?? "";

            var order = new Order
            {
                CustomerName = customerName
            };

            var createdOrder = await _orderController.CreateOrderAsync(order);
            Console.WriteLine($"Order created successfully with ID: {createdOrder.Id}");
        }

        private static async Task UpdateOrder()
        {
            Console.Write("Enter Order ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            var existingOrder = await _orderController.GetOrderByIdAsync(id);
            if (existingOrder == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            Console.WriteLine($"\nCurrent Order: {existingOrder.CustomerName}");
            Console.Write($"New Customer Name (current: {existingOrder.CustomerName}): ");
            var customerName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(customerName)) customerName = existingOrder.CustomerName;

            var updatedOrder = new Order
            {
                CustomerName = customerName,
                Status = existingOrder.Status,
                TotalAmount = existingOrder.TotalAmount
            };

            var result = await _orderController.UpdateOrderAsync(id, updatedOrder);
            if (result != null)
            {
                Console.WriteLine("Order updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update order.");
            }
        }

        private static async Task DeleteOrder()
        {
            Console.Write("Enter Order ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var result = await _orderController.DeleteOrderAsync(id);
                Console.WriteLine(result ? "Order deleted successfully." : "Failed to delete order or order not found.");
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        private static async Task GetOrdersByCustomer()
        {
            Console.Write("Enter Customer Name: ");
            var customerName = Console.ReadLine() ?? "";
            
            var orders = await _orderController.GetOrdersByCustomerAsync(customerName);
            
            Console.WriteLine($"\n=== Orders for '{customerName}' ===");
            foreach (var order in orders)
            {
                Console.WriteLine($"ID: {order.Id}, Date: {order.OrderDate:yyyy-MM-dd}, Status: {order.Status}, Total: ${order.TotalAmount:F2}");
            }
        }

        private static async Task AddItemToOrder()
        {
            Console.Write("Enter Order ID: ");
            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("Invalid Order ID format.");
                return;
            }

            Console.Write("Enter Item ID: ");
            if (!int.TryParse(Console.ReadLine(), out int itemId))
            {
                Console.WriteLine("Invalid Item ID format.");
                return;
            }

            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid quantity format.");
                return;
            }

            var result = await _orderController.AddItemToOrderAsync(orderId, itemId, quantity);
            if (result != null)
            {
                Console.WriteLine("Item added to order successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add item to order.");
            }
        }

        private static async Task RemoveItemFromOrder()
        {
            Console.Write("Enter Order ID: ");
            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("Invalid Order ID format.");
                return;
            }

            Console.Write("Enter Item ID: ");
            if (!int.TryParse(Console.ReadLine(), out int itemId))
            {
                Console.WriteLine("Invalid Item ID format.");
                return;
            }

            var result = await _orderController.RemoveItemFromOrderAsync(orderId, itemId);
            Console.WriteLine(result ? "Item removed from order successfully." : "Failed to remove item from order.");
        }

        private static async Task UpdateOrderStatus()
        {
            Console.Write("Enter Order ID: ");
            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("Invalid Order ID format.");
                return;
            }

            Console.WriteLine("Available statuses: Pending, Processing, Completed, Cancelled");
            Console.Write("Enter new status: ");
            var status = Console.ReadLine() ?? "";

            var result = await _orderController.UpdateOrderStatusAsync(orderId, status);
            if (result != null)
            {
                Console.WriteLine("Order status updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update order status.");
            }
        }

        private static async Task GetOrderTotal()
        {
            Console.Write("Enter Order ID: ");
            if (int.TryParse(Console.ReadLine(), out int orderId))
            {
                var total = await _orderController.GetOrderTotalAsync(orderId);
                Console.WriteLine($"Order Total: ${total:F2}");
            }
            else
            {
                Console.WriteLine("Invalid Order ID format.");
            }
        }
    }
}
