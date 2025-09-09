namespace AnimalShelter.Pizza.models;

public abstract class Topping
{
    public string name { get; }
    internal double priceAsExtra { get; set; }
    private bool isExtra { get; set; }
    protected int allergens { get; }
    protected int position { get; set; }

    // TODO builder patter
    protected Topping(string name, double priceAsExtra, bool isExtra, int allergens = (int)AnimalShelter.Pizza.enums.Allergens.None,
        int position = (int)AnimalShelter.Pizza.enums.PositionOnPizza.Both)
    {
        this.name = name;
        this.priceAsExtra = priceAsExtra;
        this.isExtra = isExtra;
        this.allergens = allergens;
        this.position = position;
    }
}