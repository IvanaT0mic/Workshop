using OrderManagement.Models;

namespace OrderManagement.Services.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item?> GetItemByIdAsync(int id);
        Task<Item> CreateItemAsync(Item item);
        Task<Item?> UpdateItemAsync(int id, Item item);
        Task<bool> DeleteItemAsync(int id);
        Task<IEnumerable<Item>> SearchItemsAsync(string searchTerm);
        Task<bool> IsItemInStockAsync(int itemId, int quantity);
    }
}
