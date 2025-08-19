namespace AnimalShelter.Pizza.models;

public abstract class Pizza
{
    protected string name { get; set; }
    protected int diameterInCm { get; set; }
    protected double price { get; set; }
    // TODO replace with topping model
    protected List<string> toppings { get; set; }
    
    protected int slices { get; set; }

    public Pizza(string name, int diameterInCm, double price, List<string> toppings, int slices = 8)
    {
        this.name = name;
        this.diameterInCm = diameterInCm;
        this.price = price;
        this.toppings = toppings;
    }

    public virtual void eatSlice()
    {
        if (slices <= 0)
        {
            Console.WriteLine("No slices left!");
        }
        else
        {
            Console.WriteLine("You eat a slice.");
            this.slices--;
        }
    }

    public void cutSlice()
    {
        if (slices <= 0)
        {
            Console.WriteLine("You can't cut nothing!");
        }
        else
        {
            Console.WriteLine("You cut a slice");
            this.slices++;
        }
    }

    public virtual void addTopping(string topping)
    {
        if (!this.toppings.Contains(topping))
        {
            this.toppings.Add(topping);
        }
        else
        {
            Console.WriteLine($"topping {topping} already on pizza!");
        }
    }

    public virtual void removeTopping(string topping)
    {
        if (this.toppings.Contains(topping))
        {
            Console.WriteLine($"topping {topping} already not on pizza!");
        }
        else
        {
            this.toppings.Remove(topping);
        }
    }

    public virtual void ShowPizzaInfo()
    {
        Console.WriteLine($"Pizza name: {name}");
        Console.WriteLine($"Pizza diameter: {diameterInCm}");
        Console.WriteLine($"Pizza price: {price}");
        Console.WriteLine($"Pizza toppings: { string.Join( ", ", toppings.ToArray() ) }");
        Console.WriteLine($"Slices left: {slices}");
    }
    
    
    
}