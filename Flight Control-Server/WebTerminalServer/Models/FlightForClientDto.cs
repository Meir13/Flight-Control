namespace WebTerminalServer.Models
{
    public class FlightForClientDto
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public int PassengerCount { get; set; }
        public string? Brand { get; set; }
        public string? LandingTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Status { get; set; }
        public string? PilotName { get; set; }
        public int CurrentLeg { get; set; }
    }
}