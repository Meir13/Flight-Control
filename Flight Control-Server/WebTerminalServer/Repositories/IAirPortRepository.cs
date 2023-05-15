using WebTerminalServer.Models;

namespace WebTerminalServer.Repositories
{
    public interface IAirPortRepository
    {
        Task<int> AddFlightAsync(Flight flight);
        Task<Leg> GetFirstLegAsync();
        Task AddLogAsync(Logger log);
        Task<IEnumerable<Leg>> GetNextLegAsync(LegNumber nextLegs);
        Task SaveChangesAsync();
        Task<IEnumerable<Leg>> GetAllLegsAsync();
        Task ResetLegsAsync();
    }
}
