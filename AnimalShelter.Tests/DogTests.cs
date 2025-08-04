using AnimalShelter.Models;
using System.Reflection;

namespace AnimalShelter.Tests;

public class DogTests
{
    [Fact]
    public void Dog_Constructor_SetsPropertiesCorrectly()
    {
        var dog = new Dog("buddy", 3, 15.0, "Golden Retriever", true);
        
        Assert.Equal("Dog", dog.Species);
        Assert.Equal("Woof", dog.Sound);
        Assert.Equal("Golden Retriever", dog.Breed);
        Assert.False(dog.IsTrained);
        
        var isVaccinatedField = dog.GetType().GetField("isVaccinated", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.True((bool)isVaccinatedField!.GetValue(dog)!);
    }

    [Fact]
    public void Dog_AgeShadowing_ShowsDogYears()
    {
        var dog = new Dog("test", 3, 15.0, "Beagle");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        dog.DisplayInfo();
        var output = stringWriter.ToString();
        
        Assert.Contains("Dog Years: 21", output);
        Assert.Contains("Human Age: 3", output);
    }

    [Fact]
    public void Dog_WeightCalculation_AdjustsForTraining()
    {
        var untrainedDog = new Dog("untrained", 2, 10.0, "Terrier");
        var trainedDog = new Dog("trained", 2, 10.0, "Terrier");
        trainedDog.Train("sit");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        untrainedDog.DisplayInfo();
        var untrainedOutput = stringWriter.ToString();
        
        stringWriter.GetStringBuilder().Clear();
        
        trainedDog.DisplayInfo();
        var trainedOutput = stringWriter.ToString();
        
        Assert.Contains("10.3", untrainedOutput);
        Assert.Contains("9.8", trainedOutput);
    }

    [Fact]
    public void Dog_Train_ChangesStatusAndActivity()
    {
        var dog = new Dog("learner", 2, 12.0, "Labrador");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        dog.Train("rollover");
        var output = stringWriter.ToString();
        
        Assert.True(dog.IsTrained);
        Assert.Contains("learner learned to rollover!", output);
    }

    [Fact]
    public void Dog_Play_ShowsCurrentActivity()
    {
        var dog = new Dog("player", 2, 12.0, "Poodle");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        dog.Play();
        var output = stringWriter.ToString();
        
        Assert.Contains("player is playing fetch", output);
        Assert.Contains("brings the ball back!", output);
    }

    [Fact]
    public void Dog_Eat_ChangesActivityToSleeping()
    {
        var dog = new Dog("sleepy", 2, 12.0, "Bulldog");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        dog.Eat();
        
        stringWriter.GetStringBuilder().Clear();
        
        dog.Play();
        var output = stringWriter.ToString();
        
        Assert.Contains("sleepy is playing sleeping", output);
        Assert.DoesNotContain("brings the ball back!", output);
    }

    [Fact]
    public void Dog_MakeSound_OverridesBehavior()
    {
        var dog = new Dog("barker", 2, 12.0, "German Shepherd");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        dog.MakeSound();
        var output = stringWriter.ToString();
        
        Assert.Contains("barker barks loudly: Woof Woof!", output);
    }

    [Fact]
    public void Dog_DisplayInfo_UsesShadowedMethod()
    {
        var dog = new Dog("display", 3, 15.0, "Husky");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        dog.DisplayInfo();
        var output = stringWriter.ToString();
        
        Assert.Contains("Dog Name:", output);
        Assert.Contains("Breed:", output);
        Assert.Contains("Dog Years:", output);
        Assert.Contains("Human Age:", output);
        Assert.Contains("Vaccinated:", output);
        Assert.Contains("Trained:", output);
        Assert.DoesNotContain("Species:", output);
    }

    [Fact]
    public void Dog_ReferenceComparison_SameInstance()
    {
        var dog1 = new Dog("same", 2, 10.0, "Boxer");
        var dog2 = dog1;
        
        Assert.True(ReferenceEquals(dog1, dog2));
        Assert.Same(dog1, dog2);
    }

    [Fact]
    public void Dog_ReferenceComparison_DifferentInstances()
    {
        var dog1 = new Dog("different1", 2, 10.0, "Boxer");
        var dog2 = new Dog("different2", 2, 10.0, "Boxer");
        
        Assert.False(ReferenceEquals(dog1, dog2));
        Assert.NotSame(dog1, dog2);
    }

    [Fact]
    public void Dog_ValueComparison_Properties()
    {
        var dog1 = new Dog("test", 2, 10.0, "Boxer");
        var dog2 = new Dog("test", 2, 10.0, "Boxer");
        
        Assert.Equal(dog1.Species, dog2.Species);
        Assert.Equal(dog1.Sound, dog2.Sound);
        Assert.Equal(dog1.Breed, dog2.Breed);
        Assert.Equal(dog1.IsTrained, dog2.IsTrained);
    }
}