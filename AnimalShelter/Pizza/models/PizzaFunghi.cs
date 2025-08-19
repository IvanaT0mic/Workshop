namespace AnimalShelter.Pizza.models;

public class PizzaFunghi : Pizza
{
    public PizzaFunghi(int diameterInCm, double price, int slices = 8) :
        base("Pizza Funghi", diameterInCm, price, new List<string>{"Tomato", "Mozzarella","Mushrooms"}, slices)
    {
        this.diameterInCm = diameterInCm;
        this.price = price;
        this.slices = slices;
    }
    
    public override void eatSlice()
    {
        if (slices <= 0)
        {
            Console.WriteLine("No slices left on the Funghi pizza!");
        }
        else
        {
            Console.WriteLine("You eat a slice of Funghi pizza.");
            this.slices--;
        }
    }
    
    
    public void cutSlice()
    {
        if (slices <= 0)
        {
            Console.WriteLine("There are no slices of Funghi pizza to cut!");
        }
        else
        {
            Console.WriteLine("You cut a slice of Funghi pizza");
            this.slices++;
        }
    }

    public void InspectMargherita()
    {
        Console.WriteLine("You inspect the Funghi pizza.\nIt is very mushroomy.");
    }
    
}