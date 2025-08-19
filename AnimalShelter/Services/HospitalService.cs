using AnimalShelter.Models;

namespace AnimalShelter.Services;

public class HospitalService
{
    private List<Human> patients;
    protected int capacity;
    internal string hospitalName;
    private int nextPatientId;
    
    public int PatientCount => patients.Count;
    public bool IsFull => patients.Count >= capacity;
    public bool IsOpen { get; private set; }
    
    private Dictionary<string, int> departmentCounts;
    protected List<string> availableDepartments;
    internal int emergencyWaitTime;
    
    public HospitalService(string name, int maxCapacity = 50)
    {
        patients = new List<Human>();
        capacity = maxCapacity;
        hospitalName = name;
        nextPatientId = 1001;
        IsOpen = true;
        emergencyWaitTime = 15;
        
        departmentCounts = new Dictionary<string, int>();
        availableDepartments = new List<string> 
        { 
            "General Medicine", 
            "Emergency", 
            "Cardiology", 
            "Pediatrics", 
            "Orthopedics" 
        };
        
        foreach (var dept in availableDepartments)
        {
            departmentCounts[dept] = 0;
        }
    }
    
    public void AdmitPatient(string firstName, string lastName, int age, string gender, 
                           double height, double weight, string bloodType = "O+", 
                           string department = "General Medicine")
    {
        if (!IsOpen)
        {
            Console.WriteLine($"{hospitalName} is currently closed for admissions");
            return;
        }
        
        if (IsFull)
        {
            Console.WriteLine($"{hospitalName} is at full capacity ({capacity} patients)");
            return;
        }
        
        if (!availableDepartments.Contains(department))
        {
            Console.WriteLine($"Department '{department}' not available. Using General Medicine instead.");
            department = "General Medicine";
        }
        
        var patient = new Human(firstName, lastName, age, gender, height, weight, bloodType);
        patients.Add(patient);
        departmentCounts[department]++;
        
        Console.WriteLine($"Patient {patient.FullName} admitted to {hospitalName} - {department} Department");
        Console.WriteLine($"Patient ID: {nextPatientId++}");
        
        if (department == "Emergency")
        {
            Console.WriteLine($"Emergency patient - estimated wait time: {emergencyWaitTime} minutes");
        }
    }
    
    public void DischargePatient(string firstName, string lastName)
    {
        var patient = patients.FirstOrDefault(p => p.FullName.Contains(firstName) && p.FullName.Contains(lastName));
        
        if (patient == null)
        {
            Console.WriteLine($"Patient {firstName} {lastName} not found in {hospitalName}");
            return;
        }
        
        patients.Remove(patient);
        Console.WriteLine($"Patient {patient.FullName} discharged from {hospitalName}");
        
        patient.CheckHealth();
        if (patient.IsHealthy)
        {
            Console.WriteLine($"{patient.FullName} discharged in good health");
        }
        else
        {
            Console.WriteLine($"{patient.FullName} requires follow-up care");
        }
    }
    
    public void PerformHealthChecks()
    {
        Console.WriteLine($"\n=== Health Checks at {hospitalName} ===");
        foreach (var patient in patients)
        {
            patient.CheckHealth();
        }
    }
    
    public void DisplayAllPatients()
    {
        Console.WriteLine($"\n=== Patients at {hospitalName} ===");
        if (patients.Count == 0)
        {
            Console.WriteLine("No patients currently admitted");
            return;
        }
        
        foreach (var patient in patients)
        {
            patient.DisplayBasicInfo();
            Console.WriteLine();
        }
    }
    
    public void DisplayDetailedPatientInfo()
    {
        Console.WriteLine($"\n=== Detailed Patient Information - {hospitalName} ===");
        foreach (var patient in patients)
        {
            patient.DisplayBasicInfo();
            patient.DisplayMedicalInfo();
            Console.WriteLine($"Current Health Status: {(patient.IsHealthy ? "Stable" : "Needs Attention")}");
            Console.WriteLine(new string('-', 40));
        }
    }
    
    public void UpdatePatientWeight(string firstName, string lastName, double newWeight)
    {
        var patient = patients.FirstOrDefault(p => p.FullName.Contains(firstName) && p.FullName.Contains(lastName));
        
        if (patient != null)
        {
            patient.UpdateWeight(newWeight);
        }
        else
        {
            Console.WriteLine($"Patient {firstName} {lastName} not found");
        }
    }
    
    public void OrganizeExerciseSession(string exerciseType, int minutes)
    {
        Console.WriteLine($"\n=== {exerciseType} Session at {hospitalName} ({minutes} minutes) ===");
        foreach (var patient in patients)
        {
            if (patient.IsHealthy)
            {
                patient.Exercise(exerciseType, minutes);
            }
            else
            {
                Console.WriteLine($"{patient.FullName} is excused from exercise for medical reasons");
            }
        }
    }
    
    public void CelebrateBirthdays()
    {
        Console.WriteLine($"\n=== Birthday Celebrations at {hospitalName} ===");
        foreach (var patient in patients)
        {
            patient.Celebrate();
        }
    }
    
    internal void ShowHospitalStats()
    {
        Console.WriteLine($"\n=== {hospitalName} Statistics ===");
        Console.WriteLine($"Total Patients: {PatientCount}/{capacity}");
        Console.WriteLine($"Hospital Status: {(IsOpen ? "Open" : "Closed")}");
        Console.WriteLine($"Emergency Wait Time: {emergencyWaitTime} minutes");
        
        Console.WriteLine("\nDepartment Distribution:");
        foreach (var dept in departmentCounts)
        {
            Console.WriteLine($"  {dept.Key}: {dept.Value} patients");
        }
        
        var healthyCount = patients.Count(p => p.IsHealthy);
        var needsAttentionCount = patients.Count - healthyCount;
        Console.WriteLine($"\nHealth Overview:");
        Console.WriteLine($"  Healthy: {healthyCount}");
        Console.WriteLine($"  Needs Attention: {needsAttentionCount}");
    }
    
    protected virtual void ProcessEmergency(Human patient)
    {
        Console.WriteLine($"Processing emergency for {patient.FullName}");
        emergencyWaitTime = Math.Max(5, emergencyWaitTime - 5);
    }
    
    private void CloseHospital()
    {
        IsOpen = false;
        Console.WriteLine($"{hospitalName} is now closed for new admissions");
    }
    
    private void OpenHospital()
    {
        IsOpen = true;
        Console.WriteLine($"{hospitalName} is now open for admissions");
    }
    
    public void ToggleHospitalStatus()
    {
        if (IsOpen)
        {
            CloseHospital();
        }
        else
        {
            OpenHospital();
        }
    }
    
    internal void AddAllergies()
    {
        Console.WriteLine($"\n=== Allergy Update Session at {hospitalName} ===");
        Random random = new Random();
        string[] commonAllergies = { "Peanuts", "Shellfish", "Penicillin", "Latex", "Pollen", "Dust" };
        
        foreach (var patient in patients)
        {
            if (random.Next(100) < 30) // 30% chance to have an allergy
            {
                var allergy = commonAllergies[random.Next(commonAllergies.Length)];
                patient.AddAllergy(allergy);
            }
        }
    }
}