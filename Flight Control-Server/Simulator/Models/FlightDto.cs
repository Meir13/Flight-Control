using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Models
{
    internal partial class FlightDto
    { 
        public string? Number { get; set; }
        public int PassengerCount { get; set; }
        public bool IsCritical { get; set; }
        public string? Brand { get; set; }
        public PilotDto? Pilot { get; set; }

        public FlightDto()
        {
            Number = Guid.NewGuid().ToString().Substring(0, 8);
            PassengerCount = new Random().Next(40, 300);
            Brand = GetRandomBrand();
            IsCritical = PassengerCount > 200;
        }

        public string GetRandomBrand()
        {
            var brands = Enum.GetValues(typeof(Brands));
            var randomBrand = brands.GetValue(new Random().Next(brands.Length)).ToString();
            return randomBrand ?? "Unknown";
        }
    }
}
