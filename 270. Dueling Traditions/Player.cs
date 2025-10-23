namespace FountainOfObjectsGame
{
    internal partial class Program
    {
        public class Player
        {
            public Location Location { get; set; } = new Location(0, 0);
            public int ArrowCount { get; private set; } = 5;
            public bool IsAlive { get; set; } = true;

            public bool ConsumeArrow()
            {
                if (ArrowCount > 0)
                {
                    ArrowCount--;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}