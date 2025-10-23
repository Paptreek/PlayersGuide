namespace FountainOfObjectsGame
{
    internal partial class Program
    {
        public interface IAction
        {
            void Execute(Game game);
        }

        // ShootAction handles logic for shooting an arrow one room in any direction and kills a monster if their locations are the same.

        public class ShootAction : IAction
        {
            public Direction Direction { get; set; }
            public Location Location { get; set; }

            public ShootAction(Direction direction)
            {
                Direction = direction;
            }

            public void Execute(Game game)
            {
                Location newLocation = Direction switch
                {
                    Direction.North => new Location(game.Player.Location.Row - 1, game.Player.Location.Column),
                    Direction.South => new Location(game.Player.Location.Row + 1, game.Player.Location.Column),
                    Direction.East => new Location(game.Player.Location.Row, game.Player.Location.Column + 1),
                    Direction.West => new Location(game.Player.Location.Row, game.Player.Location.Column - 1),
                };

                if (game.Player.ArrowCount > 0)
                {
                    if (game.Map.IsOnMap(newLocation))
                    {
                        Location = newLocation;
                        game.Player.ConsumeArrow();
                        Monster? deadMonster = null;

                        for (int index = 0; index < game.Monsters.Length; index++)
                        {
                            if (game.Monsters[index].Location == Location)
                            {
                                deadMonster = game.Monsters[index];
                                break;
                            }
                        }

                        deadMonster?.Kill();
                    }
                    else
                    {
                        ConsoleHelper.WriteLine("\nThere's a wall there!", ConsoleColor.Red);
                    }
                }
                else
                {
                    ConsoleHelper.WriteLine($"\nYou're out of arrows!", ConsoleColor.Red);
                }
            }
        }

        // MoveAction is used for player movement. Moves one room in the chosen direction, as long as a room exists there.

        public class MoveAction : IAction
        {
            public Direction Direction { get; set; }

            public MoveAction(Direction direction)
            {
                Direction = direction;
            }

            public void Execute(Game game)
            {
                Location newLocation = Direction switch
                {
                    Direction.North => new Location(game.Player.Location.Row - 1, game.Player.Location.Column),
                    Direction.South => new Location(game.Player.Location.Row + 1, game.Player.Location.Column),
                    Direction.East => new Location(game.Player.Location.Row, game.Player.Location.Column + 1),
                    Direction.West => new Location(game.Player.Location.Row, game.Player.Location.Column - 1),
                    _ => new Location(game.Player.Location.Row, game.Player.Location.Column)
                };

                if (game.Map.IsOnMap(newLocation))
                {
                    game.Player.Location = newLocation;
                }
                else
                {
                    ConsoleHelper.WriteLine("\nThere's a wall there!", ConsoleColor.Red);
                }
            }
        }

        // Enables the fountain if the player is in the correct room. Tells the player if the fountain was enabled or not.

        public class EnableFountainAction : IAction
        {
            public void Execute(Game game)
            {
                if (game.GetPlayerRoom() == Room.Fountain)
                {
                    ConsoleHelper.WriteLine("\nYou've activated the fountain. Well done!", ConsoleColor.Blue);
                    game.IsFountainOn = true;
                }
                else
                {
                    ConsoleHelper.WriteLine("\nYou can't do that unless you're in the fountain room!", ConsoleColor.Red);
                }
            }
        }

        // Provides a list of usable commands to the player if requested.

        public class HelpAction : IAction
        {
            public void Execute(Game game)
            {
                ConsoleHelper.WriteLine("\n-------------------\n", ConsoleColor.DarkGray);
                ConsoleHelper.WriteLine("Command List:\n", ConsoleColor.White);
                ConsoleHelper.WriteLine("move north\nmove south\nmove east\nmove west\n", ConsoleColor.Cyan);
                ConsoleHelper.WriteLine("shoot north\nshoot south\nshoot east\nshoot west\n", ConsoleColor.Green);
                ConsoleHelper.WriteLine("enable fountain", ConsoleColor.Blue);
                ConsoleHelper.WriteLine("\n-------------------", ConsoleColor.DarkGray);
            }
        }

        public class InvalidAction : IAction
        {
            public void Execute(Game game)
            {
                ConsoleHelper.WriteLine("\nYou can't do that. Try something else.", ConsoleColor.Red);
            }
        }
    }
}