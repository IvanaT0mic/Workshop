using System.Reflection.Emit;

namespace OrderManagement.Models
{
  public class Item
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string Category { get; set; } = string.Empty;

    public Item() { }
    
    public Item(int id, string name, string description, decimal price, int stockQuantity, string category)
    {
      Id = id;
      Name = name;
      Description = description;
      Price = price;
      StockQuantity = stockQuantity;
      Category = category;
    }
  }
}
