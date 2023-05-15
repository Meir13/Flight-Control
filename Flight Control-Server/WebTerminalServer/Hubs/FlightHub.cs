using Microsoft.AspNetCore.SignalR;
using WebTerminalServer.Models;

namespace WebTerminalServer.Hubs
{
    public class FlightHub : Hub
    {
        private readonly IHubContext<FlightHub> _hubContext;

        public FlightHub(IHubContext<FlightHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task AddFlightToClient(Flight flight, int currentLeg)
        {
            var flightDto = new FlightForClientDto
            {
                Id = flight.Id,
                Number = flight.Number,
                PassengerCount = flight.PassengerCount,
                Brand = flight.Brand,
                LandingTime = flight.LandingTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Status = flight.Status.ToString(),
                PilotName = flight.Pilot.Name,
                CurrentLeg = currentLeg
            };

            if (_hubContext.Clients != null)
            {
                await _hubContext.Clients.All.SendAsync("flightAdded", flightDto);
            }
        }

        public async Task RemoveFlightClient(int id)
        {
            if (_hubContext.Clients != null)
            {
                await _hubContext.Clients.All.SendAsync("removeFlight", id);
            }
        }
    }
}
