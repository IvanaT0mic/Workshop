using OrderManagement.DataAccess;
using OrderManagement.Models;
using OrderManagement.Repository.Interfaces;

namespace OrderManagement.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly InMemoryDatabase _database;

        public ItemRepository()
        {
            _database = InMemoryDatabase.Instance;
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            await Task.Delay(1);
            return _database.Items.ToList();
        }

        public async Task<Item?> GetByIdAsync(int id)
        {
            await Task.Delay(1);
            return _database.Items.FirstOrDefault(i => i.Id == id);
        }

        public async Task<Item> CreateAsync(Item item)
        {
            await Task.Delay(1);
            item.Id = _database.GetNextItemId();
            _database.Items.Add(item);
            return item;
        }

        public async Task<Item?> UpdateAsync(int id, Item item)
        {
            await Task.Delay(1);
            var existingItem = _database.Items.FirstOrDefault(i => i.Id == id);
            if (existingItem == null)
                return null;

            existingItem.Name = item.Name;
            existingItem.Description = item.Description;
            existingItem.Price = item.Price;
            existingItem.StockQuantity = item.StockQuantity;
            existingItem.Category = item.Category;

            return existingItem;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await Task.Delay(1);
            var item = _database.Items.FirstOrDefault(i => i.Id == id);
            if (item == null)
                return false;

            _database.Items.Remove(item);
            return true;
        }

        public async Task<IEnumerable<Item>> SearchAsync(string searchTerm)
        {
            await Task.Delay(1);
            return _database.Items.Where(i => 
                i.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                i.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                i.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
    }
}
