namespace AnimalShelter.Pizza.models;

public class PizzaMargherita : Pizza
{
    public PizzaMargherita(int diameterInCm, double price, int slices = 8)
        : base("Margherita", diameterInCm, price, new List<string>{ "Tomato", "Mozzarella" }, slices)
    {
        this.diameterInCm = diameterInCm;
        this.price = price;
        this.slices = slices;
    }
    
    public override void eatSlice()
    {
        if (slices <= 0)
        {
            Console.WriteLine("No slices left on the Margherita pizza!");
        }
        else
        {
            Console.WriteLine("You eat a slice of Margherita pizza.");
            this.slices--;
        }
    }
    
    
    public void cutSlice()
    {
        if (slices <= 0)
        {
            Console.WriteLine("There are no slices of Margherita pizza to cut!");
        }
        else
        {
            Console.WriteLine("You cut a slice of Margherita pizza");
            this.slices++;
        }
    }

    public void InspectMargherita()
    {
        Console.WriteLine("You inspect the Margherita pizza.\nIt is very cheesy.");
    }
}