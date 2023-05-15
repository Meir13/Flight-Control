using Microsoft.EntityFrameworkCore;
using WebTerminalServer.DAL;
using WebTerminalServer.Models;

namespace WebTerminalServer.Repositories
{
    public class AirPortRepository : IAirPortRepository
    {
        private readonly DataContext? _context;
        public AirPortRepository(DataContext data)
        {
            _context = data;
        }

        public async Task<int> AddFlightAsync(Flight flight)
        {
            if (_context == null)
            {
                throw new Exception("DataContext is null");
            }

            try
            {
                await _context.Flights.AddAsync(flight);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add flight", ex);
            }
        }

        public async Task<Leg> GetFirstLegAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                if (_context != null)
                {
                    var firstLeg = await _context.Legs.AsTracking().FirstOrDefaultAsync(l => l.Number == 1);
                    if (firstLeg == null)
                    {
                        throw new Exception($"The Legs collection is empty or does not contain a leg with Number = 1");
                    }
                    return firstLeg;
                }
                throw new Exception($"Failed to get first leg");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get first leg: {ex.Message}", ex);
            }
        }

        public async Task AddLogAsync(Logger log)
        {
            try
            {
                if (_context != null)
                {
                    await _context.Loggers.AddAsync(log);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to log {ex}");
            }
        }

        public async Task<IEnumerable<Leg>> GetNextLegAsync(LegNumber nextLegs)
        {
            await _context.SaveChangesAsync();
            if (_context == null)
            {
                throw new Exception("DataContext is null");
            }
            try
            {     
                return await _context.Legs.AsTracking().Where(leg => nextLegs.HasFlag(leg.CurrentLeg)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to find next leg {ex}");
            }
        }

        public async Task SaveChangesAsync()
        {
            if (_context == null)
            {
                throw new Exception("DataContext is null");
            }
            try
            {
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save {ex}");
            }
        }

        public async Task<IEnumerable<Leg>> GetAllLegsAsync()
        {
            return await _context.Legs.AsTracking().ToListAsync();
        }

        public async Task ResetLegsAsync()
        {
            await _context.Legs.ForEachAsync(l => l.IsOccupied = false);
            await _context.SaveChangesAsync();
        }
    }
}
