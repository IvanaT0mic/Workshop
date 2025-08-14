using OrderManagement.Controllers;

namespace OrderManagement.IntegrationTests
{
    public class OrderWorkflowIntegrationTests : IntegrationTestBase
    {
        private readonly ItemController _itemController;
        private readonly OrderController _orderController;

        public OrderWorkflowIntegrationTests()
        {
            _itemController = GetService<ItemController>();
            _orderController = GetService<OrderController>();
        }

        [Fact]
        public async Task CompleteOrderWorkflow_CreateItemsCreateOrderAddItemsAndCalculateTotal_ShouldWorkEndToEnd()
        {
            // Arrange - Create test items first
            var laptopItem = _fixture.Build<Item>()
                .With(i => i.Name, "Gaming Laptop")
                .With(i => i.Description, "High-performance gaming laptop")
                .With(i => i.Price, 1299.99m)
                .With(i => i.StockQuantity, 5)
                .With(i => i.Category, "Electronics")
                .Without(i => i.Id)
                .Create();

            var mouseItem = _fixture.Build<Item>()
                .With(i => i.Name, "Wireless Mouse")
                .With(i => i.Description, "Ergonomic wireless mouse")
                .With(i => i.Price, 49.99m)
                .With(i => i.StockQuantity, 20)
                .With(i => i.Category, "Accessories")
                .Without(i => i.Id)
                .Create();

            // Act & Assert - Step 1: Create items
            var createdLaptop = await _itemController.CreateItemAsync(laptopItem);
            var createdMouse = await _itemController.CreateItemAsync(mouseItem);

            createdLaptop.Should().NotBeNull();
            createdLaptop.Id.Should().BeGreaterThan(0);
            createdLaptop.Name.Should().Be("Gaming Laptop");
            createdLaptop.Price.Should().Be(1299.99m);

            createdMouse.Should().NotBeNull();
            createdMouse.Id.Should().BeGreaterThan(0);
            createdMouse.Name.Should().Be("Wireless Mouse");
            createdMouse.Price.Should().Be(49.99m);

            // Step 2: Create an order
            var newOrder = _fixture.Build<Order>()
                .With(o => o.CustomerName, "Alice Johnson")
                .Without(o => o.Id)
                .Without(o => o.OrderDate)
                .Without(o => o.Status)
                .Without(o => o.TotalAmount)
                .Without(o => o.OrderItems)
                .Create();

            var createdOrder = await _orderController.CreateOrderAsync(newOrder);

            createdOrder.Should().NotBeNull();
            createdOrder.Id.Should().BeGreaterThan(0);
            createdOrder.CustomerName.Should().Be("Alice Johnson");
            createdOrder.Status.Should().Be("Pending");
            createdOrder.TotalAmount.Should().Be(0m);
            createdOrder.OrderDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMinutes(1));

            // Step 3: Add items to the order
            var orderWithLaptop = await _orderController.AddItemToOrderAsync(createdOrder.Id, createdLaptop.Id, 1);
            orderWithLaptop.Should().NotBeNull();

            var orderWithBothItems = await _orderController.AddItemToOrderAsync(createdOrder.Id, createdMouse.Id, 2);
            orderWithBothItems.Should().NotBeNull();

            // Step 4: Verify the order contains the items
            var finalOrder = await _orderController.GetOrderByIdAsync(createdOrder.Id);
            finalOrder.Should().NotBeNull();
            finalOrder!.OrderItems.Should().HaveCount(2);

            var laptopOrderItem = finalOrder.OrderItems.FirstOrDefault(oi => oi.Item?.Id == createdLaptop.Id);
            laptopOrderItem.Should().NotBeNull();
            laptopOrderItem!.Quantity.Should().Be(1);
            laptopOrderItem.UnitPrice.Should().Be(1299.99m);
            laptopOrderItem.TotalPrice.Should().Be(1299.99m);

            var mouseOrderItem = finalOrder.OrderItems.FirstOrDefault(oi => oi.Item?.Id == createdMouse.Id);
            mouseOrderItem.Should().NotBeNull();
            mouseOrderItem!.Quantity.Should().Be(2);
            mouseOrderItem.UnitPrice.Should().Be(49.99m);
            mouseOrderItem.TotalPrice.Should().Be(99.98m);

            // Step 5: Calculate and verify total
            var orderTotal = await _orderController.GetOrderTotalAsync(createdOrder.Id);
            var expectedTotal = 1299.99m + (49.99m * 2); // Laptop + 2 mice
            orderTotal.Should().Be(expectedTotal);

            // Step 6: Update order status
            var updatedOrder = await _orderController.UpdateOrderStatusAsync(createdOrder.Id, "Processing");
            updatedOrder.Should().NotBeNull();
            updatedOrder!.Status.Should().Be("Processing");

            // Step 7: Verify stock was updated
            var updatedLaptop = await _itemController.GetItemByIdAsync(createdLaptop.Id);
            var updatedMouse = await _itemController.GetItemByIdAsync(createdMouse.Id);

            updatedLaptop.Should().NotBeNull();
            updatedLaptop!.StockQuantity.Should().Be(4); // Original 5 - 1 ordered

            updatedMouse.Should().NotBeNull();
            updatedMouse!.StockQuantity.Should().Be(18); // Original 20 - 2 ordered

            // Step 8: Test search functionality
            var customerOrders = await _orderController.GetOrdersByCustomerAsync("Alice Johnson");
            customerOrders.Should().HaveCount(1);
            customerOrders.First().Id.Should().Be(createdOrder.Id);

            // Step 9: Test item search
            var searchResults = await _itemController.SearchItemsAsync("Gaming");
            searchResults.Should().Contain(item => item.Name == "Gaming Laptop");
            var gamingLaptop = searchResults.First(item => item.Name == "Gaming Laptop");
            gamingLaptop.Price.Should().Be(1299.99m);
        }

        [Fact]
        public async Task OrderWorkflow_TryToAddMoreItemsThanInStock_ShouldHandleGracefully()
        {
            // Arrange - Create a limited stock item
            var limitedItem = _fixture.Build<Item>()
                .With(i => i.Name, "Limited Edition Headphones")
                .With(i => i.Price, 299.99m)
                .With(i => i.StockQuantity, 2) // Only 2 in stock
                .With(i => i.Category, "Audio")
                .Without(i => i.Id)
                .Create();

            var createdItem = await _itemController.CreateItemAsync(limitedItem);

            var order = _fixture.Build<Order>()
                .With(o => o.CustomerName, "Bob Smith")
                .Without(o => o.Id)
                .Without(o => o.OrderDate)
                .Without(o => o.Status)
                .Without(o => o.TotalAmount)
                .Without(o => o.OrderItems)
                .Create();

            var createdOrder = await _orderController.CreateOrderAsync(order);

            // Act - Try to add more items than available in stock
            // This should throw an exception due to insufficient stock
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _orderController.AddItemToOrderAsync(createdOrder.Id, createdItem.Id, 5)); // Requesting 5, but only 2 available

            // Assert - Should throw exception with appropriate message
            exception.Message.Should().Be("Insufficient stock available.");

            // Verify the order remains unchanged
            var unchangedOrder = await _orderController.GetOrderByIdAsync(createdOrder.Id);
            unchangedOrder.Should().NotBeNull();
            unchangedOrder!.OrderItems.Should().BeEmpty();
            unchangedOrder.TotalAmount.Should().Be(0m);

            // Verify stock wasn't affected
            var unchangedItem = await _itemController.GetItemByIdAsync(createdItem.Id);
            unchangedItem.Should().NotBeNull();
            unchangedItem!.StockQuantity.Should().Be(2); // Stock should remain unchanged
        }

        [Fact]
        public async Task OrderWorkflow_RemoveItemFromOrder_ShouldUpdateOrderCorrectly()
        {
            // Arrange - Create item and order with that item
            var testItem = _fixture.Build<Item>()
                .With(i => i.Name, "Test Product")
                .With(i => i.Price, 100.00m)
                .With(i => i.StockQuantity, 10)
                .Without(i => i.Id)
                .Create();

            var createdItem = await _itemController.CreateItemAsync(testItem);

            var order = _fixture.Build<Order>()
                .With(o => o.CustomerName, "Charlie Brown")
                .Without(o => o.Id)
                .Without(o => o.OrderDate)
                .Without(o => o.Status)
                .Without(o => o.TotalAmount)
                .Without(o => o.OrderItems)
                .Create();

            var createdOrder = await _orderController.CreateOrderAsync(order);

            // Add item to order first
            await _orderController.AddItemToOrderAsync(createdOrder.Id, createdItem.Id, 3);

            // Verify item was added
            var orderWithItem = await _orderController.GetOrderByIdAsync(createdOrder.Id);
            orderWithItem!.OrderItems.Should().HaveCount(1);

            // Act - Remove item from order
            var removeResult = await _orderController.RemoveItemFromOrderAsync(createdOrder.Id, createdItem.Id);

            // Assert
            removeResult.Should().BeTrue();

            // Verify item was removed
            var orderAfterRemoval = await _orderController.GetOrderByIdAsync(createdOrder.Id);
            orderAfterRemoval.Should().NotBeNull();
            orderAfterRemoval!.OrderItems.Should().BeEmpty();

            // Verify stock was restored
            var itemAfterRemoval = await _itemController.GetItemByIdAsync(createdItem.Id);
            itemAfterRemoval.Should().NotBeNull();
            itemAfterRemoval!.StockQuantity.Should().Be(10); // Stock should be restored to original amount
        }
    }
}
