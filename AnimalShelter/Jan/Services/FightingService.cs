using AnimalShelter.Jan.Models;

namespace AnimalShelter.Jan.Services;
public class FightingService
{
    private List<Magician> CurrentRound;

    private List<Magician> NextRound;

    private List<Magician> leaderboard = new();

    public int MaxCompetors;

    private int roundNumber = 1;

    public FightingService(List<Magician> magicians, int maxCompeters)
    {
        MaxCompetors = maxCompeters;
        CurrentRound = new List<Magician>(magicians);
        NextRound = new List<Magician>();
    }

    private Magician SimulateFight(Magician m1, Magician m2, out Magician loser)
    {
        if (m1.MagicPower >= m2.MagicPower)
        {
            loser = m2;
            return m1;
        }
        else
        {
            loser = m1;
            return m2;
        }
    }

    public void RunTournament()
    {
        while (CurrentRound.Count > 1)
        {
            Console.WriteLine($"\n--- Round {roundNumber} ---");

            for (int i = 0; i < CurrentRound.Count; i += 2)
            {
                if (i + 1 >= CurrentRound.Count)
                {
                    Console.WriteLine($"{CurrentRound[i].Name} bekommt ein Freilos.");
                    NextRound.Add(CurrentRound[i]);
                    continue;
                }

                Magician m1 = CurrentRound[i];
                Magician m2 = CurrentRound[i + 1];

                Magician winner = SimulateFight(m1, m2, out var loser);

                Console.WriteLine($"{m1.Name} vs {m2.Name} → Winner: {winner.Name}");

                NextRound.Add(winner);
                leaderboard.Insert(0, loser);
            }

            CurrentRound = new List<Magician>(NextRound);
            NextRound.Clear();
            roundNumber++;
        }

        Console.WriteLine($"\n Winner of the tournament: {CurrentRound[0].Name}");
        leaderboard.Insert(0, CurrentRound[0]);
    }

    public void PrintLeaderboard()
    {
        Console.WriteLine("\nLeaderboard:");
        for (int i = 0; i < leaderboard.Count; i++)
        {
            Console.WriteLine($"Leaderboard {i + 1}: {leaderboard[i].Name}");
        }
    }
}
