namespace AnimalShelter.Pizza.enums;

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