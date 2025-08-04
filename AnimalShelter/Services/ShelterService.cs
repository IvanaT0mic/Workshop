using AnimalShelter.Models;

namespace AnimalShelter.Services;

public class ShelterService
{
    private List<Animal> animals;
    protected int capacity;
    internal string shelterName;
<<<<<<< HEAD
    
    public int Count => animals.Count;
    public bool IsFull => animals.Count >= capacity;
    
=======

    public int Count => animals.Count;
    public bool IsFull => animals.Count >= capacity;

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    public ShelterService(string name, int maxCapacity = 20)
    {
        animals = new List<Animal>();
        capacity = maxCapacity;
        shelterName = name;
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    public void AddCat(string name, int age, double weight, string breed, bool isIndoor = true)
    {
        if (!IsFull)
        {
            var cat = new Cat(name, age, weight, breed, isIndoor);
            animals.Add(cat);
            Console.WriteLine($"Added cat to {shelterName}");
        }
        else
        {
            Console.WriteLine($"Shelter {shelterName} is full!");
        }
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    public void AddDog(string name, int age, double weight, string breed, bool isVaccinated = true)
    {
        if (!IsFull)
        {
            var dog = new Dog(name, age, weight, breed, isVaccinated);
            animals.Add(dog);
            Console.WriteLine($"Added dog to {shelterName}");
        }
        else
        {
            Console.WriteLine($"Shelter {shelterName} is full!");
        }
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    public void FeedAllAnimals()
    {
        Console.WriteLine($"\nFeeding time at {shelterName}!");
        foreach (var animal in animals)
        {
            animal.Eat();
        }
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    public void MakeAllAnimalsSounds()
    {
        Console.WriteLine($"\nAnimals at {shelterName} are making sounds:");
        foreach (var animal in animals)
        {
            animal.MakeSound();
        }
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    public void DisplayAllAnimals()
    {
        Console.WriteLine($"\n=== Animals at {shelterName} ===");
        foreach (var animal in animals)
        {
            animal.DisplayInfo();
            Console.WriteLine();
        }
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    public void DisplayAnimalDetails()
    {
        Console.WriteLine($"\n=== Detailed info at {shelterName} ===");
        foreach (var animal in animals)
        {
            if (animal is Cat cat)
            {
                cat.DisplayInfo();
                cat.StartGrooming();
                Console.WriteLine(cat.GetInternalInfo());
            }
            else if (animal is Dog dog)
            {
                dog.DisplayInfo();
                dog.Play();
                Console.WriteLine(dog.GetInternalInfo());
            }
            Console.WriteLine();
        }
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    public void TrainDogs(string activity)
    {
        Console.WriteLine($"\nTraining session at {shelterName}:");
        foreach (var animal in animals)
        {
            if (animal is Dog dog)
            {
                dog.Train(activity);
            }
        }
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    public void LetCatsClimb()
    {
        Console.WriteLine($"\nCats climbing at {shelterName}:");
        foreach (var animal in animals)
        {
            if (animal is Cat cat)
            {
                cat.Climb();
            }
        }
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    internal void ShowShelterStats()
    {
        var cats = animals.OfType<Cat>().Count();
        var dogs = animals.OfType<Dog>().Count();
        Console.WriteLine($"\n{shelterName} Stats: {cats} cats, {dogs} dogs, {Count}/{capacity} total");
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    private void ResetShelter()
    {
        animals.Clear();
        Console.WriteLine($"Shelter {shelterName} has been reset");
    }
<<<<<<< HEAD
    
=======

>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
    protected virtual void ProcessAdmission(Animal animal)
    {
        Console.WriteLine($"Processing admission for {animal.Species}");
    }
}