using System.ComponentModel.DataAnnotations;

namespace WebTerminalServer.Models
{
    public class Flight
    {
        public int Id { get; set; }
        [Required]
        public string? Number { get; set; }
        [Display(Name = "Passenger count")]
        public int PassengerCount { get; set; }
        [Display(Name = "Is critical")]
        public bool IsCritical { get; set; }
        public string? Brand { get; set; }
        [Display(Name = "Current leg")]
        public DateTime LandingTime { get; set; }
        [Display(Name = "Departure time")]
        public DateTime DepartureTime { get; set; }
        public FlightStatus Status { get; set; }
        [Required]
        public virtual Pilot? Pilot { get; set; }
    }

    public enum FlightStatus
    {
        InAir,
        Landed,
        WaitingForDeparture,
        Departed,
        Canceled
    }
}
