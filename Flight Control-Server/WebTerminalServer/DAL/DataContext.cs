using Microsoft.EntityFrameworkCore;
using WebTerminalServer.Models;

namespace WebTerminalServer.DAL
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Logger> Loggers { get; set; }
        public virtual DbSet<Leg> Legs { get; set; }
        public virtual DbSet<Pilot> Pilots { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
