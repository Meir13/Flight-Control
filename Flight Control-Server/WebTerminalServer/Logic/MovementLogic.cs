using WebTerminalServer.Hubs;
using WebTerminalServer.Models;
using WebTerminalServer.Repositories;

namespace WebTerminalServer.Logic
{
    public class MovementLogic
    {
        private static object? objLock;
        private readonly IAirPortRepository _airPortRepository;
        private readonly FlightHub _flightHub;
        Queue<Flight> flights = new Queue<Flight>();

        public MovementLogic(IAirPortRepository airPortRepository, FlightHub flightHub)
        {
            _airPortRepository = airPortRepository;
            _flightHub = flightHub;
        }

        internal async Task AddFlightAsync(Flight flight)
        {
            if (objLock == null)
                await FirstTimeAsync();//instantiate objlock and reset all legs in DB

            if (!flights.Contains(flight))
                flights.Enqueue(flight);

            await ProcessFlightAsync(flight);
        }

        private async Task ProcessFlightAsync(Flight flight)
        {
            try
            {
                var startLeg = await _airPortRepository.GetFirstLegAsync();
                bool isAvailable = CheckLegOccupation(startLeg);

                if (isAvailable)
                {
                    await _airPortRepository.AddFlightAsync(flight);
                    await _airPortRepository.SaveChangesAsync();
                    await NextLegAsync(flights.Dequeue(), startLeg);
                }
                else
                {
                    await Task.Delay(3000);
                    await ProcessFlightAsync(flight);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"failed to process flight", e);
            }
        }

        private async Task NextLegAsync(Flight flight, Leg leg)
        {

            if (leg.Type.HasFlag(LegType.Daparture) && flight.Status == FlightStatus.WaitingForDeparture)
            {
                await DepartAsync(flight, leg);
                return;
            }

            if (flight.Status == FlightStatus.InAir && leg.Number > 3)
                FlightLanded(flight, leg);//change status and update landing time


            Leg nextLeg = await ChooseNextLegAsync(leg);

            if (leg.IsChangeStatus)
                ChangeDepartStatus(flight, leg);

            bool isAvailable;
            lock (nextLeg) //insure only one flight can change availability, prevents flights from entering the same leg together
                isAvailable = nextLeg.IsOccupied == false;

            if (isAvailable)
                await MoveToNextLegAsync(flight, leg, nextLeg);

            else
                await WaitSomeTimeAndTryAgainAsync(flight, leg);
        }

        private static bool CheckLegOccupation(Leg startLeg)
        {
            bool isAvailable;
            lock (objLock!)
            {
                isAvailable = startLeg.IsOccupied == false;
                if (isAvailable)
                {
                    startLeg.IsOccupied = true;
                }
            }

            return isAvailable;
        }

        private async Task FirstTimeAsync()
        {
            objLock = new object();
            await _airPortRepository.ResetLegsAsync();
        }

        private async Task WaitSomeTimeAndTryAgainAsync(Flight flight, Leg leg)
        {
            await _airPortRepository.SaveChangesAsync();
            Thread.Sleep(3000);
            await NextLegAsync(flight, leg);
        }

        private async Task MoveToNextLegAsync(Flight flight, Leg leg, Leg nextLeg)
        {
            Logger log = await AddToLogAsync(flight, leg);
            log.In = DateTime.Now;
            await NotifyFlightUpdateAsync(flight, leg);
            UpdateLegOccupancy(leg, nextLeg);
            await PerformLegCrossingAsync(leg);
            await UpdateLogAndSaveChangesAsync(log);
            await NextLegAsync(flight, nextLeg);
        }

        private async Task UpdateLogAndSaveChangesAsync(Logger log)
        {
            log.Out = DateTime.Now;

            await _airPortRepository.SaveChangesAsync();
        }

        private static async Task PerformLegCrossingAsync(Leg leg)
        {
            await Task.Delay(leg.CrossingTime * 1000);
        }

        private async Task NotifyFlightUpdateAsync(Flight flight, Leg leg)
        {
            await _flightHub.AddFlightToClient(flight, leg.Number);
        }

        private static void UpdateLegOccupancy(Leg leg, Leg nextLeg)
        {
            lock (objLock)
            {
                leg.IsOccupied = false;
                nextLeg.IsOccupied = true;
            }
        }

        private static void ChangeDepartStatus(Flight flight, Leg leg)
        {
            if (leg.IsChangeStatus)
                flight.Status = FlightStatus.WaitingForDeparture;
        }

        private async Task<Leg> ChooseNextLegAsync(Leg leg)
        {
            var nextLegList = await _airPortRepository.GetNextLegAsync(leg.NextLegs);
            var nextLeg = nextLegList.First();

            if (nextLegList.Count() > 1 && nextLeg.IsOccupied == true)
            {
                foreach (var l in nextLegList)
                {
                    if (l.IsOccupied == false)
                    {
                        nextLeg = l;
                        break;
                    }
                }
            }

            return nextLeg;
        }

        private static void FlightLanded(Flight flight, Leg leg)
        {
            flight.LandingTime = DateTime.Now;
            flight.Status = FlightStatus.Landed;
        }

        private async Task DepartAsync(Flight flight, Leg leg)
        {
            Logger log = await AddToLogAsync(flight, leg);
            log.In = DateTime.Now;

            await _flightHub.AddFlightToClient(flight, leg.Number);
            Thread.Sleep(leg.CrossingTime * 1000);
            log.Out = DateTime.Now;
            flight.Status = FlightStatus.Departed;
            flight.DepartureTime = DateTime.Now;

            lock (objLock)
                leg.IsOccupied = false;


            await _airPortRepository.SaveChangesAsync();
            await _flightHub.RemoveFlightClient(flight.Id);
        }

        private async Task<Logger> AddToLogAsync(Flight flight, Leg leg)
        {
            var log = new Logger { Leg = leg, Flight = flight };
            await _airPortRepository.AddLogAsync(log);
            return log;
        }

        internal async Task<IEnumerable<Leg>> GetAllLegsAsync()
        {
            return await _airPortRepository.GetAllLegsAsync();

        }
    }
}
