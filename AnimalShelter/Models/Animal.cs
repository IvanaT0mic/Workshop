namespace AnimalShelter.Models;

public abstract class Animal
{
    protected string name;
    internal int age;
    private double weight;
    
    public string Species { get; protected set; }
    public virtual string Sound { get; protected set; } = "Unknown sound";
    
    public Animal(string name, int age, double weight, string species)
    {
        this.name = name;
        this.age = age;
        this.weight = weight;
        Species = species;
    }
    
    public virtual void MakeSound()
    {
        Console.WriteLine($"{name} makes: {Sound}");
    }
    
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Name: {name}, Age: {age}, Species: {Species}, Weight: {GetWeight()}kg");
    }
    
    protected virtual double GetWeight()
    {
        return weight;
    }
    
    public virtual void Eat()
    {
        Console.WriteLine($"{name} is eating");
        weight += 0.1;
    }
    
    internal virtual string GetInternalInfo()
    {
        return $"Internal: {name} weighs {weight}kg";
    }
}