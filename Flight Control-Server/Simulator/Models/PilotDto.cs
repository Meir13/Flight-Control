using RandomNameGeneratorLibrary;

namespace Simulator.Models
{
    public class PilotDto
    {
       public string? Name { get; set; }
       public PilotDto() => Name = new PersonNameGenerator().GenerateRandomFirstAndLastName();
    }
}