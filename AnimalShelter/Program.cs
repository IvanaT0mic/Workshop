using AnimalShelter.Models;
using AnimalShelter.Pizza.services;
using AnimalShelter.Services;

var shelter = new ShelterService("Happy Paws Shelter", 10);

shelter.AddCat("whiskers", 3, 4.2, "Persian", true);
shelter.AddCat("shadow", 5, 3.8, "Siamese", false);
shelter.AddDog("buddy", 2, 15.5, "Golden Retriever", true);
shelter.AddDog("max", 4, 12.3, "Beagle", true);

Console.WriteLine("=== INHERITANCE AND SHADOWING DEMO ===\n");

Console.WriteLine("1. Different DisplayInfo methods (new vs override):");
shelter.DisplayAllAnimals();

Console.WriteLine("\n2. Detailed view showing shadowed fields:");
shelter.DisplayAnimalDetails();

Console.WriteLine("\n3. Sound polymorphism:");
shelter.MakeAllAnimalsSounds();

Console.WriteLine("\n4. Training dogs (changing internal state):");
shelter.TrainDogs("sit");

Console.WriteLine("\n5. Cats climbing (lives shadowing):");
shelter.LetCatsClimb();

Console.WriteLine("\n6. Feeding all (weight changes):");
shelter.FeedAllAnimals();

Console.WriteLine("\n7. After feeding - weight changes visible:");
shelter.DisplayAnimalDetails();

shelter.ShowShelterStats();

Console.WriteLine("\n=== SHADOWING EFFECTS SUMMARY ===");
Console.WriteLine("- Cat.name shadows Animal.name (uppercase vs original)");
Console.WriteLine("- Dog.age shadows Animal.age (dog years vs human years)");
Console.WriteLine("- Cat.DisplayInfo uses 'new' (hides base method)");
Console.WriteLine("- Dog.DisplayInfo uses 'new virtual' (creates new hierarchy)");
Console.WriteLine("- GetInternalInfo shows different internal access levels");
Console.WriteLine("- Weight calculations affected by protected overrides");

var pizzaService = new PizzaService("Pizzeria String", 32);

pizzaService.addFunghi(pizzaService.LargePizzaSizeInCm, 35);
pizzaService.addMargherita(pizzaService.StandardPizzaSizeInCm,19);
pizzaService.addMargherita(pizzaService.SmallPizzaSizeInCm,14, 4);

// test eating slice
pizzaService.DisplayAllPizzas();

pizzaService.pizzas[2].eatSlice(); 

pizzaService.pizzas[2].ShowPizzaInfo();

pizzaService.pizzas[2].cutSlice();
pizzaService.pizzas[2].cutSlice();
pizzaService.pizzas[2].cutSlice();

pizzaService.pizzas[2].ShowPizzaInfo();


