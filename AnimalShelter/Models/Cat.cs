namespace AnimalShelter.Models;

public class Cat : Animal
{
    private new string name;
    protected int livesLeft;
    internal bool isIndoor;
<<<<<<< HEAD
    
    public override string Sound { get; protected set; } = "Meow";
    public string Breed { get; private set; }
    
    public Cat(string name, int age, double weight, string breed, bool isIndoor = true) 
=======

    public override string Sound { get; protected set; } = "Meow";
    public string Breed { get; private set; }

    public Cat(string name, int age, double weight, string breed, bool isIndoor = true)
>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
        : base(name, age, weight, "Cat")
    {
        this.name = name.ToUpper();
        livesLeft = 9;
        this.isIndoor = isIndoor;
        Breed = breed;
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    public override void MakeSound()
    {
        Console.WriteLine($"{name} purrs and says: {Sound}");
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    public new void DisplayInfo()
    {
        Console.WriteLine($"Cat Name: {name}, Breed: {Breed}, Lives: {livesLeft}, Indoor: {isIndoor}");
        Console.WriteLine($"Age: {age}, Weight: {GetWeight()}kg");
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    protected override double GetWeight()
    {
        return base.GetWeight() + (isIndoor ? 0.5 : 0);
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    public void Climb()
    {
        Console.WriteLine($"{name} climbs up high");
        if (!isIndoor && livesLeft > 1)
        {
            livesLeft--;
            Console.WriteLine($"{name} used a life! Lives left: {livesLeft}");
        }
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    public override void Eat()
    {
        Console.WriteLine($"{name} delicately eats cat food");
        base.Eat();
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    internal new string GetInternalInfo()
    {
        return $"Cat Internal: {name}, Lives: {livesLeft}, Indoor: {isIndoor}";
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    private void Groom()
    {
        Console.WriteLine($"{name} is grooming itself");
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    public void StartGrooming()
    {
        Groom();
    }
}