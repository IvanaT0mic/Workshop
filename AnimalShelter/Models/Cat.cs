namespace AnimalShelter.Models;

public class Cat : Animal
{
    private new string name;
    protected int livesLeft;
    internal bool isIndoor;
    
    public override string Sound { get; protected set; } = "Meow";
    public string Breed { get; private set; }
    
    public Cat(string name, int age, double weight, string breed, bool isIndoor = true) 
        : base(name, age, weight, "Cat")
    {
        this.name = name.ToUpper();
        livesLeft = 9;
        this.isIndoor = isIndoor;
        Breed = breed;
    }
    
    public override void MakeSound()
    {
        Console.WriteLine($"{name} purrs and says: {Sound}");
    }
    
    public new void DisplayInfo()
    {
        Console.WriteLine($"Cat Name: {name}, Breed: {Breed}, Lives: {livesLeft}, Indoor: {isIndoor}");
        Console.WriteLine($"Age: {age}, Weight: {GetWeight()}kg");
    }
    
    protected override double GetWeight()
    {
        return base.GetWeight() + (isIndoor ? 0.5 : 0);
    }
    
    public void Climb()
    {
        Console.WriteLine($"{name} climbs up high");
        if (!isIndoor && livesLeft > 1)
        {
            livesLeft--;
            Console.WriteLine($"{name} used a life! Lives left: {livesLeft}");
        }
    }
    
    public override void Eat()
    {
        Console.WriteLine($"{name} delicately eats cat food");
        base.Eat();
    }
    
    internal new string GetInternalInfo()
    {
        return $"Cat Internal: {name}, Lives: {livesLeft}, Indoor: {isIndoor}";
    }
    
    private void Groom()
    {
        Console.WriteLine($"{name} is grooming itself");
    }
    
    public void StartGrooming()
    {
        Groom();
    }
}