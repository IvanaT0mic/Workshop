namespace AnimalShelter.Models;

public class Dog : Animal
{
    protected new int age;
    private string favoriteActivity;
    internal bool isVaccinated;
    
    public override string Sound { get; protected set; } = "Woof";
    public string Breed { get; private set; }
    public bool IsTrained { get; internal set; }
    
    public Dog(string name, int age, double weight, string breed, bool isVaccinated = true) 
        : base(name, age, weight, "Dog")
    {
        this.age = age * 7;
        favoriteActivity = "fetch";
        this.isVaccinated = isVaccinated;
        Breed = breed;
        IsTrained = false;
    }
    
    public override void MakeSound()
    {
        Console.WriteLine($"{base.name} barks loudly: {Sound} {Sound}!");
    }
    
    public new virtual void DisplayInfo()
    {
        Console.WriteLine($"Dog Name: {base.name}, Breed: {Breed}, Dog Years: {age}");
        Console.WriteLine($"Vaccinated: {isVaccinated}, Trained: {IsTrained}, Activity: {favoriteActivity}");
        Console.WriteLine($"Human Age: {base.age}, Weight: {GetWeight()}kg");
    }
    
    protected override double GetWeight()
    {
        return base.GetWeight() + (IsTrained ? -0.2 : 0.3);
    }
    
    public void Play()
    {
        Console.WriteLine($"{base.name} is playing {favoriteActivity}");
        if (favoriteActivity == "fetch")
        {
            Console.WriteLine($"{base.name} brings the ball back!");
        }
    }
    
    public override void Eat()
    {
        Console.WriteLine($"{base.name} eagerly devours dog food");
        favoriteActivity = "sleeping";
        base.Eat();
    }
    
    internal override string GetInternalInfo()
    {
        return $"Dog Internal: {base.name}, Dog years: {age}, Vaccinated: {isVaccinated}";
    }
    
    private void SetActivity(string activity)
    {
        favoriteActivity = activity;
    }
    
    public void Train(string newActivity)
    {
        IsTrained = true;
        SetActivity(newActivity);
        Console.WriteLine($"{base.name} learned to {newActivity}!");
    }
}