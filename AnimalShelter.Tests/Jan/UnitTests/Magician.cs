using AnimalShelter.Jan.Models;

namespace AnimalShelter.Jan.UnitTests;

public class TestableMagician : Magician
{
    public void PublicCastSpell() => CastSpell();

    public void PublicPrepareSpell() => PrepareSpell();

    public void PublicShowMagic() => ShowMagic();
}

public class MagicianTests
{
    [Fact]
    public void Magician_Constructor_SetsPropertiesCorrectly()
    {
        var spells = new[] { "Fireball", "Ice Shard", "Lightning Bolt", "Heal", "Shield" };
        var items = new[] { "Wand", "Staff", "Amulet", "Ring", "Cloak" };
        var magician = new TestableMagician("Gandalf", 10, 100.0, spells, items);

        Assert.Equal("Gandalf", magician.Name);
        Assert.Equal(10, magician.ExperienceLevel);
        Assert.Equal(100.0, magician.MagicPower);
        Assert.Equal(spells, magician.Spells);
        Assert.Equal(items, magician.MagicalItems);
    }

    [Fact]
    public void Magician_CastSpell_ReducesMagicPower()
    {
        var spells = new[] { "Fireball", "Ice Shard", "Lightning Bolt", "Heal", "Shield" };
        var items = new[] { "Wand", "Staff", "Amulet", "Ring", "Cloak" };
        var magician = new TestableMagician("Gandalf", 10, 100.0, spells, items);

        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        magician.PublicCastSpell();
        var output = stringWriter.ToString();

        Assert.Contains("casts a spell!", output);
        Assert.Contains("remaining magic power: 90", output);
    }

    [Fact]
    public void Magician_PrepareSpell_IncreasesMagicPower()
    {
        var spells = new[] { "Fireball", "Ice Shard", "Lightning Bolt", "Heal", "Shield" };
        var items = new[] { "Wand", "Staff", "Amulet", "Ring", "Cloak" };
        var magician = new TestableMagician("Gandalf", 10, 100.0, spells, items);

        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        magician.PublicPrepareSpell();
        var output = stringWriter.ToString();

        Assert.Contains("is preparing a spell...", output);
        Assert.Contains("Gandalf's magic power: 105", output);
    }

    [Fact]
    public void Magician_ShowMagic_DisplaysMagicalItemsAndExperience()
    {
        var spells = new[] { "Fireball", "Ice Shard", "Lightning Bolt", "Heal", "Shield" };
        var items = new[] { "Wand", "Staff", "Amulet", "Ring", "Cloak" };
        var magician = new TestableMagician("Gandalf", 10, 100.0, spells, items);

        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        magician.PublicCastSpell();
        magician.ShowMagic();
        var output = stringWriter.ToString();

        Assert.Contains("performs a magical act!", output);
        Assert.Contains("Using magical item: Wand", output);
        Assert.Contains("Using magical item: Staff", output);
        Assert.Contains("Using magical item: Amulet", output);
        Assert.Contains("Using magical item: Ring", output);
        Assert.Contains("Using magical item: Cloak", output);
        Assert.Contains("Gandalf's experience level: 10", output);
    }
}
