namespace AnimalShelter.Jan.Models;

public class IceMagician : Magician
{
    public double ChillFactor { get; private set; }
    public int FrozenTargets { get; private set; }
    public int IceShardCount { get; private set; }
    public TimeSpan SnowVeilDuration { get; private set; }
    public bool IsFrozenShieldActive { get; private set; }
    public double FrostArmorStrength { get; private set; }
    public bool CanSummonIceGolem { get; private set; }

    public IceMagician(string name, int experienceLevel, double magicPower, string[] spells, string[] magicalItems, double chillFactor, int frozenTargets, int iceShardCount, TimeSpan snowVeilDuration,
        bool isFrozenShieldActive, double frostArmorStrength, bool canSummonIceGolem)
        : base(name, experienceLevel, magicPower, spells, magicalItems)
    {
        ChillFactor = chillFactor;
        FrozenTargets = frozenTargets;
        IceShardCount = iceShardCount;
        SnowVeilDuration = snowVeilDuration;
        IsFrozenShieldActive = isFrozenShieldActive;
        FrostArmorStrength = frostArmorStrength;
        CanSummonIceGolem = canSummonIceGolem;
    }

    public void ActivateFrozenShield()
    {
        IsFrozenShieldActive = true;
        Console.WriteLine($"Ice Magician activates the frozen shield!");
    }

    public void DeactivateFrozenShield()
    {
        IsFrozenShieldActive = false;
        Console.WriteLine($"Ice Magician deactivates the frozen shield!");
    }

    public void SummonIceGolem()
    {
        if (CanSummonIceGolem)
        {
            Console.WriteLine($"Ice Magician summons an Ice Golem!");
        }
        else
        {
            Console.WriteLine($"Ice Magician cannot summon an Ice Golem at this time.");
        }
    }

    protected override void ShowMagic()
    {
        Console.WriteLine($"Ice Magician's Chill Factor: {ChillFactor}");
        Console.WriteLine($"Frozen Targets: {FrozenTargets}");
        Console.WriteLine($"Ice Shard Count: {IceShardCount}");
        Console.WriteLine($"Snow Veil Duration: {SnowVeilDuration.TotalSeconds} seconds");
        Console.WriteLine($"Is Frozen Shield Active: {IsFrozenShieldActive}");
        Console.WriteLine($"Frost Armor Strength: {FrostArmorStrength}");
        Console.WriteLine($"Can Summon Ice Golem: {CanSummonIceGolem}");
    }

    public void ShowIceMagic()
    {
        ShowMagic();
    }
}

