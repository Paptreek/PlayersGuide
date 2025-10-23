namespace FountainOfObjectsGame
{
    internal partial class Program
    {
        // Monster is the base class for Maelstroms and Amaroks. It knows their locations and tracks whether they're alive or dead.

        public abstract class Monster
        {
            public bool IsAlive { get; set; } = true;
            public Location Location { get; set; } = new Location(0, 0);

            public abstract void InteractWithPlayer(Game game);

            public void Kill()
            {
                if (IsAlive)
                {
                    ConsoleHelper.WriteLine($"\nYou hit a monster!", ConsoleColor.Green);
                    IsAlive = false;
                }
                else
                {
                    ConsoleHelper.WriteLine($"\nYou fire another arrow into the corpse.", ConsoleColor.DarkGreen);
                }
            }
        }

        // Maelstrom holds the logic for moving the player and itself whenever the player steps on its location.

        public class Maelstrom : Monster
        {
            public override void InteractWithPlayer(Game game)
            {
                if (IsAlive)
                {
                    ConsoleHelper.WriteLine("\nWoosh! You were swept to a new location by a maelstrom!", ConsoleColor.DarkMagenta);

                    Location playerLocation = game.Player.Location;
                    Location newLocation = new Location(playerLocation.Row - 1, playerLocation.Column + 2);

                    if (newLocation.Row < 0) newLocation.Row += game.Map.Rows;
                    if (newLocation.Column < 0) newLocation.Column += game.Map.Columns;
                    if (newLocation.Row >= game.Map.Rows) newLocation.Row -= game.Map.Rows;
                    if (newLocation.Column >= game.Map.Columns) newLocation.Column -= game.Map.Columns;

                    game.Player.Location = newLocation;

                    Location.Row += 1;
                    Location.Column -= 2;

                    if (Location.Row < 0) Location.Row += game.Map.Rows;
                    if (Location.Column < 0) Location.Column += game.Map.Columns;
                    if (Location.Row >= game.Map.Rows) Location.Row -= game.Map.Rows;
                    if (Location.Column >= game.Map.Columns) Location.Column -= game.Map.Columns;
                }
            }
        }

        // Amarok just kills the player if their locations are the same.

        public class Amarok : Monster
        {
            public override void InteractWithPlayer(Game game)
            {
                if (IsAlive)
                {
                    game.Player.IsAlive = false;
                }
            }
        }
    }
}