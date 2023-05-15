using Simulator.Models;
using System.Net.Http.Json;

internal class Program
{
    static System.Timers.Timer timer = new System.Timers.Timer(10000);
    static HttpClient client = new() { BaseAddress = new Uri("https://localhost:7293") };

    private static void Main(string[] args)
    {
        timer.Elapsed += (s, e) => CreateFlight();
        timer.Start();
        Console.ReadLine();
    }
    async static void CreateFlight()
    {
        var flight = new FlightDto { Pilot = new PilotDto() };
        await client.PostAsJsonAsync("api/Flights", flight);
    }
}