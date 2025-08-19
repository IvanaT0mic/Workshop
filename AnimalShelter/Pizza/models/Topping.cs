namespace AnimalShelter.Pizza.models;

public abstract class Topping
{
    [Flags]
    public enum Allergens
    {
        None = 0,
        Gluten = 1,
        Egg = 2,
        Corn = 4,
        Milk = 8,
        Meat = 16,
        Cheese = 32,
        Fish = 64
    }

    [Flags]
    public enum PositionOnPizza
    {
        Left = 1,
        Right = 2,
        Both = 3
    }
    
    public string name { get; }
    internal double priceAsExtra { get; set; }
    private bool isExtra { get; set; }
    protected int allergens { get; }
    protected int position { get; set; }

    protected Topping(string name, double priceAsExtra, bool isExtra, int allergens = (int)Allergens.None,
        int position = (int)PositionOnPizza.Both)
    {
        this.name = name;
        this.priceAsExtra = priceAsExtra;
        this.isExtra = isExtra;
        this.allergens = allergens;
        this.position = position;
    }
}