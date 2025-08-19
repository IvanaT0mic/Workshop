namespace AnimalShelter.Models;

public class Human
{
    private string firstName;
    protected string lastName;
    internal int age;
    private double height;
    private double weight;
    
    public string FullName => $"{firstName} {lastName}";
    public string Gender { get; private set; }
    public string BloodType { get; internal set; }
    public bool IsHealthy { get; protected set; }
    
    private string medicalHistory;
    protected List<string> allergies;
    internal string emergencyContact;
    
    public Human(string firstName, string lastName, int age, string gender, double height, double weight, string bloodType = "O+")
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
        Gender = gender;
        this.height = height;
        this.weight = weight;
        BloodType = bloodType;
        IsHealthy = true;
        medicalHistory = "No significant medical history";
        allergies = new List<string>();
        emergencyContact = "Not provided";
    }
    
    public virtual void DisplayBasicInfo()
    {
        Console.WriteLine($"Patient: {FullName}, Age: {age}, Gender: {Gender}");
        Console.WriteLine($"Height: {height}cm, Weight: {weight}kg, Blood Type: {BloodType}");
    }
    
    public virtual void DisplayMedicalInfo()
    {
        Console.WriteLine($"Health Status: {(IsHealthy ? "Healthy" : "Requires attention")}");
        Console.WriteLine($"Medical History: {medicalHistory}");
        Console.WriteLine($"Allergies: {(allergies.Count > 0 ? string.Join(", ", allergies) : "None known")}");
        Console.WriteLine($"Emergency Contact: {emergencyContact}");
    }
    
    protected virtual double CalculateBMI()
    {
        double heightInMeters = height / 100.0;
        return weight / (heightInMeters * heightInMeters);
    }
    
    public void CheckHealth()
    {
        double bmi = CalculateBMI();
        Console.WriteLine($"{firstName} BMI: {bmi:F1}");
        
        if (bmi < 18.5 || bmi > 30.0 || age > 65)
        {
            IsHealthy = false;
            Console.WriteLine($"{firstName} requires medical attention");
        }
        else
        {
            IsHealthy = true;
            Console.WriteLine($"{firstName} is in good health");
        }
    }
    
    public virtual void UpdateWeight(double newWeight)
    {
        weight = newWeight;
        Console.WriteLine($"{firstName}'s weight updated to {weight}kg");
        CheckHealth();
    }
    
    public void AddAllergy(string allergy)
    {
        if (!allergies.Contains(allergy))
        {
            allergies.Add(allergy);
            Console.WriteLine($"Added allergy '{allergy}' to {firstName}'s record");
        }
    }
    
    internal void UpdateMedicalHistory(string newHistory)
    {
        medicalHistory = newHistory;
        Console.WriteLine($"Medical history updated for {firstName}");
    }
    
    internal void SetEmergencyContact(string contact)
    {
        emergencyContact = contact;
        Console.WriteLine($"Emergency contact set for {firstName}: {contact}");
    }
    
    private void LogActivity(string activity)
    {
        Console.WriteLine($"[LOG] {firstName}: {activity} at {DateTime.Now:HH:mm}");
    }
    
    public void Exercise(string exerciseType, int minutes)
    {
        LogActivity($"Started {exerciseType} for {minutes} minutes");
        
        if (minutes > 30)
        {
            weight -= 0.1;
            Console.WriteLine($"{firstName} burned calories during {exerciseType}");
        }
        
        CheckHealth();
    }
    
    protected virtual string GetAgeCategory()
    {
        return age switch
        {
            < 18 => "Minor",
            >= 18 and < 65 => "Adult",
            _ => "Senior"
        };
    }
    
    public virtual void Celebrate()
    {
        age++;
        Console.WriteLine($"Happy Birthday {firstName}! Now {age} years old ({GetAgeCategory()})");
        CheckHealth();
    }
}