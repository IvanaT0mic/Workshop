using OrderManagement.DataAccess;
using OrderManagement.Models;
using OrderManagement.Repository.Interfaces;

namespace OrderManagement.Repository;

public class ItemRepository(IInMemoryDatabase database) : IItemRepository
{
    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        await Task.Delay(1);
        return [.. database.Items];
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        await Task.Delay(1);
        return database.Items.FirstOrDefault(i => i.Id == id);
    }

    public async Task<Item> CreateAsync(Item item)
    {
        await Task.Delay(1);
        item.Id = database.GetNextItemId();
        database.Items.Add(item);
        return item;
    }

    public async Task<Item?> UpdateAsync(int id, Item item)
    {
        await Task.Delay(1);
        var existingItem = database.Items.FirstOrDefault(i => i.Id == id);
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
        var item = database.Items.FirstOrDefault(i => i.Id == id);
        if (item == null)
            return false;

        database.Items.Remove(item);
        return true;
    }

    public async Task<IEnumerable<Item>> SearchAsync(string searchTerm)
    {
        await Task.Delay(1);
        return [.. database.Items.Where(i =>
            i.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            i.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            i.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
        )];
    }
}
