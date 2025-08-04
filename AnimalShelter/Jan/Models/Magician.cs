namespace AnimalShelter.Jan.Models;

public abstract class Magician
{
    internal string Name { get; }

    internal int ExperienceLevel { get; set; }

    internal string[] Spells { get; set; } = new string[5];

    internal string[] MagicalItems { get; set; } = new string[5];
    internal double MagicPower { get; private set; }


    protected Magician(string name, int experienceLevel, double magicPower, string[] spells, string[] magicalItems)
    {
        if (spells == null || spells.Length != 5)
            throw new ArgumentException("The 'spells' array must contain exactly 5 elements.", nameof(spells));

        if (magicalItems == null || magicalItems.Length != 5)
            throw new ArgumentException("The 'magicalItems' array must contain exactly 5 elements.", nameof(magicalItems));

        Name = name;
        ExperienceLevel = experienceLevel;
        MagicPower = magicPower;

        Spells = spells;
        MagicalItems = magicalItems;
    }


    protected void CastSpell()
    {
        Console.WriteLine($"{Name} casts a spell!");

        foreach (var spell in Spells.Where(s => s != null))
        {
            Console.WriteLine($"- {Name} casts: {spell}");
        }

        MagicPower -= 10.0;
        Console.WriteLine($"{Name}'s remaining magic power: {MagicPower}");
    }

    protected virtual void ShowMagic()
    {
        Console.WriteLine($"{Name} performs a magical act!");

        foreach (var item in MagicalItems.Where(i => i != null))
        {
            Console.WriteLine($"- Using magical item: {item}");
        }

        Console.WriteLine($"{Name}'s experience level: {ExperienceLevel}");
    }

    protected void PrepareSpell()
    {
        Console.WriteLine($"{Name} is preparing a spell...");
        MagicPower += 5.0;
    }

    public void PerformMagic()
    {
        PrepareSpell();
        CastSpell();
        ShowMagic();
    }

    public void ShowMagicianDetails()
    {
        Console.WriteLine($"Magician Details: {Name}");
        Console.WriteLine($"Experience Level: {ExperienceLevel}");
        Console.WriteLine($"Magic Power: {MagicPower}");

        Console.WriteLine("Known Spells:");
        foreach (var spell in Spells.Where(s => s != null))
        {
            Console.WriteLine($"- {spell}");
        }

        Console.WriteLine("Magical Items:");
        foreach (var item in MagicalItems.Where(i => i != null))
        {
            Console.WriteLine($"- {item}");
        }

        ShowMagic();
    }

    public void DisplayMagicianInfo()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Experience Level: {ExperienceLevel}");
        Console.WriteLine($"Magic Power: {MagicPower}");
        Console.WriteLine("Spells:");
        foreach (var spell in Spells.Where(s => s != null))
        {
            Console.WriteLine($"- {spell}");
        }
        Console.WriteLine("Magical Items:");
        foreach (var item in MagicalItems.Where(i => i != null))
        {
            Console.WriteLine($"- {item}");
        }
    }

}