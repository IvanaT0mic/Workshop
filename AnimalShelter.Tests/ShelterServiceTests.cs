using AnimalShelter.Models;
using AnimalShelter.Services;
using System.Reflection;

namespace AnimalShelter.Tests;

public class ShelterServiceTests
{
    [Fact]
    public void ShelterService_Constructor_InitializesCorrectly()
    {
        var shelter = new ShelterService("Test Shelter", 5);
        
        Assert.Equal(0, shelter.Count);
        Assert.False(shelter.IsFull);
        
        var nameField = shelter.GetType().GetField("shelterName", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.Equal("Test Shelter", nameField?.GetValue(shelter));
    }

    [Fact]
    public void ShelterService_AddCat_IncreasesCount()
    {
        var shelter = new ShelterService("Cat Shelter", 5);
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        shelter.AddCat("fluffy", 3, 4.5, "Persian");
        
        Assert.Equal(1, shelter.Count);
        Assert.False(shelter.IsFull);
        
        var output = stringWriter.ToString();
        Assert.Contains("Added cat to Cat Shelter", output);
    }

    [Fact]
    public void ShelterService_AddDog_IncreasesCount()
    {
        var shelter = new ShelterService("Dog Shelter", 5);
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        shelter.AddDog("buddy", 2, 15.0, "Golden Retriever");
        
        Assert.Equal(1, shelter.Count);
        Assert.False(shelter.IsFull);
        
        var output = stringWriter.ToString();
        Assert.Contains("Added dog to Dog Shelter", output);
    }

    [Fact]
    public void ShelterService_CapacityReached_RejectsNewAnimals()
    {
        var shelter = new ShelterService("Small Shelter", 2);
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        shelter.AddCat("cat1", 2, 4.0, "Siamese");
        shelter.AddDog("dog1", 3, 12.0, "Beagle");
        shelter.AddCat("cat2", 1, 3.5, "Persian");
        
        var output = stringWriter.ToString();
        
        Assert.Equal(2, shelter.Count);
        Assert.True(shelter.IsFull);
        Assert.Contains("Shelter Small Shelter is full!", output);
    }

    [Fact]
    public void ShelterService_FeedAllAnimals_CallsEatOnAll()
    {
        var shelter = new ShelterService("Feeding Shelter", 5);
        
        shelter.AddCat("hungry_cat", 2, 4.0, "Tabby");
        shelter.AddDog("hungry_dog", 3, 12.0, "Labrador");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        shelter.FeedAllAnimals();
        var output = stringWriter.ToString();
        
        Assert.Contains("Feeding time at Feeding Shelter!", output);
        Assert.Contains("delicately eats cat food", output);
        Assert.Contains("eagerly devours dog food", output);
        Assert.Contains("hungry_cat is eating", output);
        Assert.Contains("hungry_dog is eating", output);
    }

    [Fact]
    public void ShelterService_MakeAllAnimalsSounds_CallsMakeSoundOnAll()
    {
        var shelter = new ShelterService("Noisy Shelter", 5);
        
        shelter.AddCat("meower", 2, 4.0, "Siamese");
        shelter.AddDog("barker", 3, 12.0, "German Shepherd");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        shelter.MakeAllAnimalsSounds();
        var output = stringWriter.ToString();
        
        Assert.Contains("Animals at Noisy Shelter are making sounds:", output);
        Assert.Contains("MEOWER purrs and says: Meow", output);
        Assert.Contains("barker barks loudly: Woof Woof!", output);
    }

    [Fact]
    public void ShelterService_DisplayAllAnimals_ShowsBasicInfo()
    {
        var shelter = new ShelterService("Display Shelter", 5);
        
        shelter.AddCat("display_cat", 3, 4.5, "Persian");
        shelter.AddDog("display_dog", 2, 15.0, "Golden Retriever");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        shelter.DisplayAllAnimals();
        var output = stringWriter.ToString();
        
        Assert.Contains("=== Animals at Display Shelter ===", output);
        Assert.Contains("Name: display_cat", output);
        Assert.Contains("Name: display_dog", output);
        Assert.Contains("Species: Cat", output);
        Assert.Contains("Species: Dog", output);
    }

    [Fact]
    public void ShelterService_DisplayAnimalDetails_ShowsShadowedInfo()
    {
        var shelter = new ShelterService("Detail Shelter", 5);
        
        shelter.AddCat("detail_cat", 3, 4.5, "Persian");
        shelter.AddDog("detail_dog", 2, 15.0, "Golden Retriever");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        shelter.DisplayAnimalDetails();
        var output = stringWriter.ToString();
        
        Assert.Contains("=== Detailed info at Detail Shelter ===", output);
        Assert.Contains("Cat Name: DETAIL_CAT", output);
        Assert.Contains("Dog Name: detail_dog", output);
        Assert.Contains("Dog Years:", output);
        Assert.Contains("Human Age:", output);
        Assert.Contains("is grooming itself", output);
        Assert.Contains("is playing", output);
        Assert.Contains("Cat Internal:", output);
        Assert.Contains("Dog Internal:", output);
    }

    [Fact]
    public void ShelterService_TrainDogs_OnlyAffectsDogs()
    {
        var shelter = new ShelterService("Training Shelter", 5);
        
        shelter.AddCat("untrained_cat", 2, 4.0, "Tabby");
        shelter.AddDog("trainee_dog", 3, 12.0, "Border Collie");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        shelter.TrainDogs("rollover");
        var output = stringWriter.ToString();
        
        Assert.Contains("Training session at Training Shelter:", output);
        Assert.Contains("trainee_dog learned to rollover!", output);
        Assert.DoesNotContain("untrained_cat", output);
    }

    [Fact]
    public void ShelterService_LetCatsClimb_OnlyAffectsCats()
    {
        var shelter = new ShelterService("Climbing Shelter", 5);
        
        shelter.AddCat("climber_cat", 2, 4.0, "Tabby", false);
        shelter.AddDog("non_climber_dog", 3, 12.0, "Bulldog");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        shelter.LetCatsClimb();
        var output = stringWriter.ToString();
        
        Assert.Contains("Cats climbing at Climbing Shelter:", output);
        Assert.Contains("CLIMBER_CAT climbs up high", output);
        Assert.Contains("used a life!", output);
        Assert.DoesNotContain("non_climber_dog", output);
    }

    [Fact]
    public void ShelterService_ShowShelterStats_CountsAnimalsCorrectly()
    {
        var shelter = new ShelterService("Stats Shelter", 10);
        
        shelter.AddCat("cat1", 2, 4.0, "Persian");
        shelter.AddCat("cat2", 3, 4.5, "Siamese");
        shelter.AddDog("dog1", 2, 12.0, "Beagle");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        shelter.ShowShelterStats();
        var output = stringWriter.ToString();
        
        Assert.Contains("Stats Shelter Stats: 2 cats, 1 dogs, 3/10 total", output);
    }

    [Fact]
    public void ShelterService_ReferenceComparison_DifferentShelters()
    {
        var shelter1 = new ShelterService("Shelter A", 5);
        var shelter2 = new ShelterService("Shelter B", 5);
        
        Assert.False(ReferenceEquals(shelter1, shelter2));
        Assert.NotSame(shelter1, shelter2);
    }

    [Fact]
    public void ShelterService_ReferenceComparison_SameShelter()
    {
        var shelter1 = new ShelterService("Same Shelter", 5);
        var shelter2 = shelter1;
        
        Assert.True(ReferenceEquals(shelter1, shelter2));
        Assert.Same(shelter1, shelter2);
    }

    [Fact]
    public void ShelterService_ValueComparison_Properties()
    {
        var shelter1 = new ShelterService("Compare Shelter", 5);
        var shelter2 = new ShelterService("Compare Shelter", 5);
        
        shelter1.AddCat("same_cat", 2, 4.0, "Persian");
        shelter2.AddCat("same_cat", 2, 4.0, "Persian");
        
        Assert.Equal(shelter1.Count, shelter2.Count);
        Assert.Equal(shelter1.IsFull, shelter2.IsFull);
    }

    [Fact]
    public void ShelterService_AnimalPolymorphism_WorksWithBaseType()
    {
        var shelter = new ShelterService("Polymorphic Shelter", 5);
        
        shelter.AddCat("poly_cat", 2, 4.0, "Tabby");
        shelter.AddDog("poly_dog", 3, 12.0, "Retriever");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        shelter.MakeAllAnimalsSounds();
        var output = stringWriter.ToString();
        
        Assert.Contains("POLY_CAT purrs and says: Meow", output);
        Assert.Contains("poly_dog barks loudly: Woof Woof!", output);
    }
}