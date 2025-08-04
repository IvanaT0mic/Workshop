using AnimalShelter.Jan.Models;

namespace AnimalShelter.Jan.Services;

internal class MagicalSchoolService
{
    private Dictionary<string, Magician> Students { get; }

    private Dictionary<string, Magician[]> StudentHouses;

    private int MaxStudents;

    private static int sId = 0;

    private string? NextStudentId;

    private string SchoolName { get; }

    public MagicalSchoolService(int maxStudents = 100, string schoolName = null)
    {
        SchoolName = schoolName ??= GenerateRandomSchoolName();
        MaxStudents = maxStudents;
        Students = new Dictionary<string, Magician>();
        StudentHouses = new Dictionary<string, Magician[]>();
    }

    private static readonly string[] Prefixes = { "Arcane", "Mystic", "Shadow", "Celestial", "Ancient", "Ethereal" };
    private static readonly string[] Nouns = { "Academy", "Institute", "Sanctum", "Guild", "School", "Order" };
    private static readonly string[] Elements = { "Fire", "Ice", "Storm", "Dragons", "Wizards", "Alchemy" };

    private static Random rng = new Random();

    private string GenerateRandomSchoolName()
    {
        var prefix = Prefixes[rng.Next(Prefixes.Length)];
        var element = Elements[rng.Next(Elements.Length)];
        var noun = Nouns[rng.Next(Nouns.Length)];

        return $"{prefix} {element} {noun}";
    }

    private string GetSchoolAbbreviation(string schoolName)
    {
        return string.Join("", schoolName
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(word => word[0]))
            .ToUpper();
    }

    public void EnrollStudent(Magician newStudent, string apartmentName)
    {
        NextStudentId = $"{sId}:{GetSchoolAbbreviation(SchoolName)}";
        if (Students.Count >= MaxStudents)
        {
            Console.WriteLine("Enrollment full. Cannot add more Students.");
            return;
        }
        Students.Add(NextStudentId, newStudent);
        Magician[] magicians1 = [newStudent];
        StudentHouses[apartmentName] = StudentHouses.ContainsKey(apartmentName)
        ? StudentHouses[apartmentName].Append(newStudent).ToArray()
        : magicians1;

        Console.WriteLine($"Enrolled {newStudent.Name} in apartmentName {apartmentName} with ID {NextStudentId}");
        sId++;
    }

    public void DisplayStudents()
    {
        Console.WriteLine("Current Students:");
        foreach (var student in Students)
        {
            Console.WriteLine($"{student.Value} - House: {StudentHouses[student.Key]}");
        }
    }

    public void DisplayHouses()
    {
        Console.WriteLine("Current Houses:");
        foreach (var house in StudentHouses)
        {
            Console.WriteLine($"{house.Key}: {string.Join(", ", house.Value.Select(s => s.Name))}");
        }
    }

    public void DisplaySchoolInfo()
    {
        Console.WriteLine($"School Name: {SchoolName}");
        Console.WriteLine($"School Abbrevation: {SchoolName}");
        Console.WriteLine($"Max Students: {MaxStudents}");
        Console.WriteLine($"Current Enrollment: {Students.Count}");
        Console.WriteLine($"Next Student ID: {NextStudentId}");
        Console.WriteLine("Houses and their students:");
        DisplayHouses();
    }

    public void DisplayStudentDetails(string studentId)
    {
        if (Students.TryGetValue(studentId, out var student))
        {
            Console.WriteLine($"Student ID: {studentId}");
            student.DisplayMagicianInfo();
        }
        else
        {
            Console.WriteLine($"No student found with ID: {studentId}");
        }
    }
}
