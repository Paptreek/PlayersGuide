namespace FountainOfObjectsGame
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            Intro intro = new();
            intro.PrintIntroText();
            GameFactory.CreateGame().Run();
        }

        public enum Direction { North, South, East, West }
        public enum Room { Empty, Entrance, Fountain, Pit, OffMap }
    }
}