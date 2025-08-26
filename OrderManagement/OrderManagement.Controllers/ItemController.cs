using OrderManagement.Models;
using OrderManagement.Services;
using OrderManagement.Services.Interfaces;

namespace OrderManagement.Controllers
{
    public class ItemController(IItemService _itemService)
    {
        public async Task<IEnumerable<Item>> GetAllItems()
        {
            return await _itemService.GetAllItemsAsync();
        }

        public async Task<Item?> GetItemByIdAsync(int id)
        {
            return await _itemService.GetItemByIdAsync(id);
        }

        public async Task<Item> CreateItemAsync(Item item)
        {
            return await _itemService.CreateItemAsync(item);
        }

        public Task<Item?> UpdateItem(int id, Item item)
        {
            return _itemService.UpdateItemAsync(id, item);
        }

        public Task<bool> DeleteItemAsync(int id)
        {
            return _itemService.DeleteItemAsync(id);
        }

        public async Task<IEnumerable<Item>> SearchItemsAsync(string searchTerm)
        {
            return await _itemService.SearchItemsAsync(searchTerm);
        }

        public async Task<bool> CheckStock(int itemId, int quantity)
        {
            return await _itemService.IsItemInStockAsync(itemId, quantity);
        }
    }
}