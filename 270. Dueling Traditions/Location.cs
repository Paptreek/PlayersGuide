namespace FountainOfObjectsGame
{
    internal partial class Program
    {
        // Location is used by player, monsters, and arrows, which allows the game to know where everything is.

        public record Location(int Row, int Column)
        {
            public int Row { get; set; } = Row;
            public int Column { get; set; } = Column;
        }
    }
}