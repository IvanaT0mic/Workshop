namespace AnimalShelter.Jan.assoziations.assoziations;

class Engine
{
    public void Start()
    {
        Console.WriteLine("Engine is starting...");
    }

    public void Stop()
    {
        Console.WriteLine("Engine is stopping...");
    }
}

class Car
{
    private Engine carEngine;
    public Car()
    {
        carEngine = new Engine();
    }
    
    public void StartCar()
    {
        carEngine.Start();
        Console.WriteLine("Car is now running.");
    }

    public void StopCar()
    {
        carEngine.Stop();
        Console.WriteLine("Car has stopped.");
    }
}
