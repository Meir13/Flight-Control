namespace WebTerminalServer.Models
{
    public class Leg
    {
        public int Id { get; set; }
        public int Number { get; set; }
        //public virtual Flight? Flight { get; set; }
        public bool IsOccupied { get; set; }
        public int CrossingTime { get; set; }
        public LegNumber CurrentLeg { get; set; }
        public LegNumber NextLegs { get; set; }
        public bool IsChangeStatus { get; set; }
        public LegType Type { get; set; }
    }

    [Flags]
    public enum LegNumber
    {
        Departure = 1024,
        One = 1,
        Two = 2,
        Three = 4,
        Four = 8,
        Five = 16,
        Six = 32,
        Seven = 64,
        Eight = 128,
    }
    [Flags]
    public enum LegType
    {
        Landing = 1,
        Daparture = 2
    }
}