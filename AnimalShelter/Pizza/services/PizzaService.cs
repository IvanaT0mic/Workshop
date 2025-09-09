using AnimalShelter.Pizza.models;

namespace AnimalShelter.Pizza.services;

public class PizzaService (string pizzeriaName, int pizzaCapacity = 40)
{
    public int SmallPizzaSizeInCm = 20;
    public int StandardPizzaSizeInCm = 32;
    public int LargePizzaSizeInCm = 45;

    public List<models.Pizza> pizzas { get; }
    protected int pizzaCapacity;
    internal string pizzeriaName;

    public int Count => pizzas.Count;
    public bool IsFull => Count >= pizzaCapacity;

    public void addMargherita(int diameterInCm, double price, int slices = 8)
    {
        if (!IsFull)
        {
            var pMargherita = new PizzaMargherita(diameterInCm, price, slices);
            pizzas.Add(pMargherita);
            Console.WriteLine($"Added Pizza Margherita to {pizzeriaName}");
        }
        else
        {
            Console.WriteLine($"{pizzeriaName} has too many pizzas!");
        }
    }

    public void addFunghi(int diameterInCm, double price, int slices = 8)
    {
        if (!IsFull)
        {
            var pFunghi = new PizzaFunghi(diameterInCm, price, slices);
            pizzas.Add(pFunghi);
            Console.WriteLine($"Added Pizza Funghi to {pizzeriaName}");
        }
        else
        {
            Console.WriteLine($"{pizzeriaName} has too many pizzas!");
        }
    }

    public void DisplayAllPizzas()
    {
        Console.WriteLine($"\n=== Pizzas at {pizzeriaName} ===");
        foreach (var pizza in pizzas)
        {
            pizza.ShowPizzaInfo();
            Console.WriteLine();
        }
    }
}