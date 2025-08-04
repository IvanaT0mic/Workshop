using AnimalShelter.Models;
using AnimalShelter.Services;
using System.Reflection;

namespace AnimalShelter.Tests;

public class CatTests
{
    [Fact]
    public void Cat_Constructor_SetsPropertiesCorrectly()
    {
        var cat = new Cat("fluffy", 3, 4.5, "Persian", true);
        
        Assert.Equal("Cat", cat.Species);
        Assert.Equal("Meow", cat.Sound);
        Assert.Equal("Persian", cat.Breed);
        Assert.True(cat.GetType().GetField("isIndoor", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(cat) as bool?);
    }

    [Fact]
    public void Cat_NameShadowing_ShowsUppercaseName()
    {
        var cat = new Cat("fluffy", 3, 4.5, "Persian");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        cat.DisplayInfo();
        var output = stringWriter.ToString();
        
        Assert.Contains("FLUFFY", output);
        Assert.DoesNotContain("fluffy", output);
    }

    [Fact]
    public void Cat_WeightCalculation_AddsIndoorBonus()
    {
        var indoorCat = new Cat("indoor", 2, 4.0, "Siamese", true);
        var outdoorCat = new Cat("outdoor", 2, 4.0, "Siamese", false);
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        indoorCat.DisplayInfo();
        var indoorOutput = stringWriter.ToString();
        
        stringWriter.GetStringBuilder().Clear();
        
        outdoorCat.DisplayInfo();
        var outdoorOutput = stringWriter.ToString();
        
        Assert.Contains("4.5", indoorOutput);
        Assert.Contains("4", outdoorOutput);
    }

    [Fact]
    public void Cat_Climbing_ReducesLivesForOutdoorCats()
    {
        var outdoorCat = new Cat("climber", 2, 4.0, "Tabby", false);
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        outdoorCat.Climb();
        var output = stringWriter.ToString();
        
        Assert.Contains("used a life", output);
        Assert.Contains("Lives left: 8", output);
    }

    [Fact]
    public void Cat_Climbing_DoesNotReduceLivesForIndoorCats()
    {
        var indoorCat = new Cat("safe", 2, 4.0, "Persian", true);
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        indoorCat.Climb();
        var output = stringWriter.ToString();
        
        Assert.DoesNotContain("used a life", output);
    }

    [Fact]
    public void Cat_MakeSound_OverridesBehavior()
    {
        var cat = new Cat("meower", 2, 4.0, "Siamese");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        cat.MakeSound();
        var output = stringWriter.ToString();
        
        Assert.Contains("MEOWER purrs and says: Meow", output);
    }

    [Fact]
    public void Cat_DisplayInfo_UsesShadowedMethod()
    {
        var cat = new Cat("test", 3, 4.0, "Persian");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        cat.DisplayInfo();
        var output = stringWriter.ToString();
        
        Assert.Contains("Cat Name:", output);
        Assert.Contains("Breed:", output);
        Assert.Contains("Lives:", output);
        Assert.DoesNotContain("Species:", output);
    }

    [Fact]
    public void Cat_Eat_CallsBaseAndChangesWeight()
    {
        var cat = new Cat("eater", 2, 4.0, "Tabby");
        
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        cat.Eat();
        var output = stringWriter.ToString();
        
        Assert.Contains("delicately eats cat food", output);
        Assert.Contains("eater is eating", output);
    }
}