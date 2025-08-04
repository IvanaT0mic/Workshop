<<<<<<< HEAD
﻿using AnimalShelter.Models;
=======
﻿using AnimalShelter.Jan.Models;
using AnimalShelter.Jan.Services;
using AnimalShelter.Models;
>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
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
<<<<<<< HEAD
=======
shelter.DisplayAnimalDetails();
>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)

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
<<<<<<< HEAD
=======



// Additional methods of magicians
List<Magician> magicians = new List<Magician>
{
    new IceMagician("Elsa", 5, 200, new string[5], new string[5], 0.5, 0, 0, TimeSpan.Zero, false, 0, false),
    new FireMagician("Hades", 6, 220, new string[5], new string[5], 0.3, 10, TimeSpan.FromSeconds(5)),
    new FireMagician("Zuko", 3, 180, new string[5], new string[5], 0.4, 8, TimeSpan.FromSeconds(3)),
    new IceMagician("Sub-Zero", 4, 190, new string[5], new string[5], 0.6, 1, 1, TimeSpan.FromSeconds(2), false, 1, false),
};

FightingService fightingService = new FightingService(magicians, 20);
fightingService.PrintLeaderboard();
fightingService.RunTournament();
fightingService.PrintLeaderboard();

MagicalSchoolService magicalSchoolService = new MagicalSchoolService();
magicalSchoolService.EnrollStudent(new IceMagician("Frosty", 2, 150, new string[5], new string[5], 0.4, 0, 0, TimeSpan.Zero, false, 0, false), "Tree");
magicalSchoolService.EnrollStudent(new IceMagician("Jessica", 5, 0, new string[5], new string[5], 0.4, 0, 0, TimeSpan.Zero, false, 0, false), "Tree");
magicalSchoolService.EnrollStudent(new IceMagician("Tom", 0, 30, new string[5], new string[5], 0, 0, 0, TimeSpan.Zero, false, 0, false), "Astra Heights");
magicalSchoolService.EnrollStudent(new IceMagician("Tom", 10, 50, new string[5], new string[5], 0, 0, 0, TimeSpan.Zero, false, 0, false), "Harbor Haven");
>>>>>>> 6ad5d5d (tried to make the functions for unit tests runnable)
