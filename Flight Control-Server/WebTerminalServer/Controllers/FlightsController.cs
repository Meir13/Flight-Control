using Microsoft.AspNetCore.Mvc;
using WebTerminalServer.Logic;
using WebTerminalServer.Models;

namespace WebTerminalServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(3);
        private readonly MovementLogic _movement;

        public FlightsController(MovementLogic movement)
        {
            _movement = movement;
        }

        [HttpPost]
        public async Task Post(Flight flight)
        {
            await semaphoreSlim.WaitAsync(3);
            try
            {
                await _movement.AddFlightAsync(flight);
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        [HttpGet("legs")]
        public async Task<IEnumerable<LegDto>> GetLegs()
        {

            var legs = await _movement.GetAllLegsAsync();
            var legsDtos = legs.Select(l => new LegDto
            {
                Id = l.Id,
                LegNumber = l.Number,
                LegType = l.Type.ToString(),
            });
            return legsDtos;
        }
    }
}
