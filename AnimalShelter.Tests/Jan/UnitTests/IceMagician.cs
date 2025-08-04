using AnimalShelter.Jan.Models;

namespace AnimalShelter.Jan.UnitTests;

public class TestableIceMagician : IceMagician
{
    public void PublicCastSpell() => CastSpell();

    public void PublicPrepareSpell() => PrepareSpell();

    public void PublicShowMagic() => ShowMagic();
}
public class IceMagicianTests
{
    [Fact]
    public void ActivateFrozenShield()
    {
        var iceMagician = new TestableIceMagician("Frosty", 5, 80.0, new[] { "Ice Spike", "Frost Nova", "Blizzard", "Ice Barrier", "Snowstorm" }, new[] { "Ice Wand", "Frost Staff", "Snow Amulet", "Glacial Ring", "Chill Cloak" });

        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        iceMagician.ActivateFrozenShield();

        var output = stringWriter.ToString();
        Assert.Contains("activates the frozen shield!", output);
    }

    [Fact]
    public void DeactivateFrozenShield()
    {
        var iceMagician = new TestableIceMagician("Frosty", 5, 80.0, new[] { "Ice Spike", "Frost Nova", "Blizzard", "Ice Barrier", "Snowstorm" }, new[] { "Ice Wand", "Frost Staff", "Snow Amulet", "Glacial Ring", "Chill Cloak" });

        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        iceMagician.DeactivateFrozenShield();

        var output = stringWriter.ToString();
        Assert.Contains("deactivates the frozen shield!", output);
    }

    [Fact]
    public void SummonIceGolem()
    {
        var iceMagician = new TestableIceMagician("Frosty", 5, 80.0, new[] { "Ice Spike", "Frost Nova", "Blizzard", "Ice Barrier", "Snowstorm" }, new[] { "Ice Wand", "Frost Staff", "Snow Amulet", "Glacial Ring", "Chill Cloak" });

        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        iceMagician.SummonIceGolem();

        var output = stringWriter.ToString();
        Assert.Contains("summons an Ice Golem!", output);
    }

    [Fact]
    public void ShowMagic_Overrides_Behavior()
    {
        var iceMagician = new TestableIceMagician("Frosty", 5, 80.0, new[] { "Ice Spike", "Frost Nova", "Blizzard", "Ice Barrier", "Snowstorm" }, new[] { "Ice Wand", "Frost Staff", "Snow Amulet", "Glacial Ring", "Chill Cloak" });

        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        iceMagician.ShowIceMagic();

        var output = stringWriter.ToString();
        Assert.Contains("Chill Factor:", output);
        Assert.Contains("Frozen Targets:", output);
        Assert.Contains("Ice Shard Count:", output);
        Assert.Contains("Snow Veil Duration:", output);
        Assert.Contains("Is Frozen Shield Active:", output);
        Assert.Contains("Frost Armor Strength:", output);
        Assert.Contains("Can Summon Ice Golem:", output);
    }
}