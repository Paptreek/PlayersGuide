namespace FountainOfObjectsGame
{
    internal partial class Program
    {
        // Asks the user to choose a map size, then creates all necessary rooms for the chosen size.
        public class GameFactory
        {
            public static Game CreateGame()
            {
                while (true)
                {
                    string choice = ConsoleHelper.Prompt("Select your map size (small, medium, large):");
                    Console.WriteLine();

                    Game? game = choice switch
                    {
                        "small" => CreateSmallGame(),
                        "medium" => CreateMediumGame(),
                        "large" => CreateLargeGame(),
                        _ => null
                    };

                    if (game != null)
                    {
                        return game;
                    }
                }
            }

            public static Game CreateSmallGame()
            {
                Map map = new Map(4, 4);
                map.SetRoom(0, 0, Room.Entrance);
                map.SetRoom(0, 2, Room.Fountain);
                map.SetRoom(2, 1, Room.Pit);

                Monster[] monsters =
                {
            new Maelstrom() { Location = new Location(3, 1) },
            new Amarok() { Location = new Location(1, 3) }
        };

                return new(map, new Location(0, 0), monsters);
            }

            public static Game CreateMediumGame()
            {
                Map map = new Map(6, 6);
                map.SetRoom(3, 2, Room.Entrance);
                map.SetRoom(1, 4, Room.Fountain);
                map.SetRoom(1, 1, Room.Pit);
                map.SetRoom(4, 4, Room.Pit);

                Monster[] monster =
                {
            new Maelstrom() { Location = new Location(5, 1) },
            new Amarok() { Location = new Location(0, 3) },
            new Amarok() { Location = new Location(5, 3) }
        };

                return new(map, new Location(3, 2), monster);
            }

            public static Game CreateLargeGame()
            {
                Map map = new Map(8, 8);
                map.SetRoom(4, 6, Room.Entrance);
                map.SetRoom(2, 0, Room.Fountain);
                map.SetRoom(1, 0, Room.Pit);
                map.SetRoom(4, 3, Room.Pit);
                map.SetRoom(2, 5, Room.Pit);
                map.SetRoom(6, 1, Room.Pit);

                Monster[] maelstroms =
                {
            new Maelstrom() { Location = new Location(1, 3) },
            new Maelstrom() { Location = new Location(6, 5) },
            new Amarok() { Location = new Location(0, 6) },
            new Amarok() { Location = new Location(3, 1) },
            new Amarok() { Location = new Location(7, 3) }
        };

                return new(map, new Location(4, 6), maelstroms);
            }
        }
    }
}