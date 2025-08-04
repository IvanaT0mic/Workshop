using AnimalShelter.Models;

namespace AnimalShelter.Tests;

public class TestableAnimal : Animal
{
    public TestableAnimal(string name, int age, double weight, string species) 
        : base(name, age, weight, species)
    {
    }
    
    public double PublicGetWeight() => GetWeight();
    public string PublicGetInternalInfo() => GetInternalInfo();
    public void PublicSetAge(int newAge) => age = newAge;
    public string PublicGetName() => name;
}

public class AnimalTests
{
    [Fact]
    public void Animal_Constructor_SetsPropertiesCorrectly()
    {
        var animal = new TestableAnimal("test", 5, 20.0, "TestSpecies");
        
        Assert.Equal("TestSpecies", animal.Species);
        Assert.Equal("Unknown sound", animal.Sound);
        Assert.Equal(5, animal.age);
        Assert.Equal("test", animal.PublicGetName());
    }

    [Fact]
    public void Animal_MakeSound_DisplaysCorrectFormat()
    {
        var animal = new TestableAnimal("sonic", 3, 15.0, "TestAnimal");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        animal.MakeSound();
        var output = stringWriter.ToString();
        
        Assert.Contains("sonic makes: Unknown sound", output);
    }

    [Fact]
    public void Animal_DisplayInfo_ShowsAllBasicInfo()
    {
        var animal = new TestableAnimal("display", 4, 18.5, "TestCreature");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        animal.DisplayInfo();
        var output = stringWriter.ToString();
        
        Assert.Contains("Name: display", output);
        Assert.Contains("Age: 4", output);
        Assert.Contains("Species: TestCreature", output);
        Assert.Contains("Weight: 18.5kg", output);
    }

    [Fact]
    public void Animal_Eat_IncreasesWeight()
    {
        var animal = new TestableAnimal("eater", 2, 10.0, "TestAnimal");
        var initialWeight = animal.PublicGetWeight();
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        animal.Eat();
        var newWeight = animal.PublicGetWeight();
        
        Assert.True(newWeight > initialWeight);
        Assert.Equal(initialWeight + 0.1, newWeight, 1);
    }

    [Fact]
    public void Animal_GetWeight_ReturnsCorrectValue()
    {
        var animal = new TestableAnimal("weighty", 3, 25.7, "TestAnimal");
        
        Assert.Equal(25.7, animal.PublicGetWeight());
    }

    [Fact]
    public void Animal_GetInternalInfo_ContainsBasicData()
    {
        var animal = new TestableAnimal("internal", 3, 12.5, "TestAnimal");
        
        var internalInfo = animal.PublicGetInternalInfo();
        
        Assert.Contains("Internal:", internalInfo);
        Assert.Contains("internal", internalInfo);
        Assert.Contains("12.5kg", internalInfo);
    }

    [Fact]
    public void Animal_InternalAgeAccess_CanBeModified()
    {
        var animal = new TestableAnimal("aging", 2, 10.0, "TestAnimal");
        
        animal.PublicSetAge(5);
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        animal.DisplayInfo();
        var output = stringWriter.ToString();
        
        Assert.Contains("Age: 5", output);
    }

    [Fact]
    public void Animal_ProtectedNameAccess_WorksWithinInheritance()
    {
        var animal = new TestableAnimal("protected", 3, 15.0, "TestAnimal");
        
        Assert.Equal("protected", animal.PublicGetName());
    }

    [Fact]
    public void Animal_ReferenceVsValueComparison_DifferentInstances()
    {
        var animal1 = new TestableAnimal("same", 3, 15.0, "TestAnimal");
        var animal2 = new TestableAnimal("same", 3, 15.0, "TestAnimal");
        
        Assert.False(ReferenceEquals(animal1, animal2));
        Assert.NotSame(animal1, animal2);
        Assert.Equal(animal1.Species, animal2.Species);
        Assert.Equal(animal1.PublicGetName(), animal2.PublicGetName());
    }

    [Fact]
    public void Animal_ReferenceComparison_SameInstance()
    {
        var animal1 = new TestableAnimal("reference", 3, 15.0, "TestAnimal");
        var animal2 = animal1;
        
        Assert.True(ReferenceEquals(animal1, animal2));
        Assert.Same(animal1, animal2);
    }

    [Fact]
    public void Animal_PolymorphicBehavior_VirtualMethods()
    {
        Animal animal = new TestableAnimal("polymorphic", 3, 15.0, "TestAnimal");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        animal.MakeSound();
        var output = stringWriter.ToString();
        
        Assert.Contains("polymorphic makes: Unknown sound", output);
    }
}