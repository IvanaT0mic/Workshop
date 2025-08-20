using OrderManagement.Models;
using OrderManagement.Repository.Interfaces;
using OrderManagement.Services.Interfaces;

namespace OrderManagement.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _itemRepository.GetAllAsync();
        }

        public async Task<Item?> GetItemByIdAsync(int id)
        {
            if (id <= 0) 
                return null;

            return await _itemRepository.GetByIdAsync(id);
        }

        public async Task<Item> CreateItemAsync(Item item)
        {

            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Item name is required.");

            if (item.Price < 0)
                throw new ArgumentException("Item price cannot be negative.");

            if (item.StockQuantity < 0)
                throw new ArgumentException("Stock quantity cannot be negative.");

            return await _itemRepository.CreateAsync(item);
        }

        //TODO: Use an ItemDTO for update without the id
        public async Task<Item?> UpdateItemAsync(int id, Item item)
        {
            if (id <= 0)
                return null;


            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Item name is required.");

            if (item.Price < 0)
                throw new ArgumentException("Item price cannot be negative.");

            if (item.StockQuantity < 0)
                throw new ArgumentException("Stock quantity cannot be negative.");

            return await _itemRepository.UpdateAsync(id, item);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            if (id <= 0)
                return false;

            return await _itemRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Item>> SearchItemsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllItemsAsync();

            return await _itemRepository.SearchAsync(searchTerm);
        }

        public async Task<bool> IsItemInStockAsync(int itemId, int quantity)
        {
            var item = await _itemRepository.GetByIdAsync(itemId);
            return item != null && item.StockQuantity >= quantity;
        }
    }
}
