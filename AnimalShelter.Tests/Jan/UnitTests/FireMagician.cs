namespace AnimalShelter.Jan.UnitTests;

public class TestableFireMagician : FireMagician
{
    public void ShowMagic() => ShowMagic();
}
public class FireMagician
{
    [Fact]
    public void ActivateFlamingAura()
    {
        var fireMagician = new TestableFireMagician("Blaze", 7, 90.0, new[] { "Fireball", "Flame Wave", "Inferno", "Heat Shield", "Phoenix Fire" }, new[] { "Fire Staff", "Flame Ring", "Ember Cloak", "Heat Amulet", "Blazing Boots" }, 0.8, 5, TimeSpan.FromSeconds(10));

        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        fireMagician.ActivateFlamingAura();

        var output = stringWriter.ToString();
        Assert.Contains("activate the flame aura!", output);
    }

    [Fact]
    public void ShowMagic_Overrides_Behavior()
    {
        var fireMagician = new TestableFireMagician("Blaze", 7, 90.0, new[] { "Fireball", "Flame Wave", "Inferno", "Heat Shield", "Phoenix Fire" }, new[] { "Fire Staff", "Flame Ring", "Ember Cloak", "Heat Amulet", "Blazing Boots" }, 0.8, 5, TimeSpan.FromSeconds(10));

        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        fireMagician.ShowMagic();

        var output = stringWriter.ToString();
        Assert.Contains("Flame intensity: 5, Fire resistance: 80%", output);
    }
}

