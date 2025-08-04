namespace AnimalShelter.Jan.Models;

public class FireMagician : Magician
{
    public double FireResistance { get; private set; }
    public int FlameIntensity { get; private set; }
    public TimeSpan BurnDuration { get; private set; }
    public double HeatEnergy { get; private set; }
    public bool IsFlamingAuraActive { get; private set; }

    public FireMagician(string name, int experienceLevel, double magicPower, string[] spells, string[] magicalItems,
        double fireResistance, int flameIntensity, TimeSpan burnDuration)
        : base(name, experienceLevel, magicPower, spells, magicalItems)
    {
        FireResistance = fireResistance;
        FlameIntensity = flameIntensity;
        BurnDuration = burnDuration;
        HeatEnergy = 0;
        IsFlamingAuraActive = false;
    }

    public void ActivateFlamingAura()
    {
        IsFlamingAuraActive = true;
        Console.WriteLine($"{Name} activate the flame aura!");
    }

    protected override void ShowMagic()
    {
        base.ShowMagic();
        Console.WriteLine($"Flame intensity: {FlameIntensity}, Fire resistance: {FireResistance * 100}%");
    }
}

