Intro intro = new();
intro.PrintIntroText();
GameFactory.CreateGame().Run();

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

// Game class is in charge of bringing all other classes together and running the logic to make the game loop until it ends via win or loss.
// It also holds player actions, and player senses.

public class Game
{
    public Map Map { get; }
    public Player Player { get; }
    public Monster[] Monsters { get; }
    public IAction? Action { get; }
    public bool IsFountainOn { get; set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public TimeSpan RunTime { get; private set; }

    public Game(Map map, Location startLocation, Monster[] monsters)
    {
        Player = new Player();
        Player.Location = startLocation;
        Map = map;
        Monsters = monsters;
    }

    public bool HasWon() => GetPlayerRoom() == Room.Entrance && IsFountainOn == true;
    public Room GetPlayerRoom() => Map.GetRoom(Player.Location.Row, Player.Location.Column);
    public bool HasFallenIntoPit() => GetPlayerRoom() == Room.Pit;

    public void Run()
    {
        StartTime = DateTime.Now;

        while (Player.IsAlive && !HasWon() && !HasFallenIntoPit())
        {
            ConsoleHelper.WriteLine("_.~^`^~.~^`^~.~^`^~.~^`^~.~^`^~.~^`^~._", ConsoleColor.Gray);
            ConsoleHelper.WriteLine($"Current Arrows: {Player.ArrowCount}", ConsoleColor.Gray);
            Console.WriteLine($"Current {Player.Location}");
            PrintPlayerSenses();
            PlayerAction();

            foreach (Monster monster in Monsters)
            {
                if (Player.Location == monster.Location)
                {
                    monster.InteractWithPlayer(this);
                }
            }

            Console.WriteLine();
        }

        if (HasWon())
        {
            EndTime = DateTime.Now;
            ConsoleHelper.WriteLine("You enabled the fountain and escaped with your life. You win!", ConsoleColor.Magenta);
            PrintTotalRuntime();
        }
        else if (!Player.IsAlive || HasFallenIntoPit())
        {
            EndTime = DateTime.Now;
            Console.WriteLine($"{PrintCauseOfDeath()}");
            Console.ForegroundColor = ConsoleColor.White;
            PrintTotalRuntime();
        }
    }

    public string PrintCauseOfDeath()
    {
        if (GetPlayerRoom() == Room.Pit)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            return "You fell into a pit and died.";
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            return "You were devoured by an amarok.";
        }
    }

    public void PrintPlayerSenses()
    {
        if (GetPlayerRoom() == Room.Entrance)
        {
            ConsoleHelper.WriteLine("You see light coming in from a nearby tunnel.", ConsoleColor.Yellow);
        }
        else if (GetPlayerRoom() == Room.Fountain && IsFountainOn == true)
        {
            ConsoleHelper.WriteLine("You hear rushing water.", ConsoleColor.Blue);
        }
        else if (GetPlayerRoom() == Room.Fountain)
        {
            ConsoleHelper.WriteLine("You hear dripping water.", ConsoleColor.Blue);
        }

        if (Map.IsNearDanger(Player, Room.Pit))
        {
            ConsoleHelper.WriteLine("You feel a draft coming from a nearby room.", ConsoleColor.DarkRed);
        }

        foreach (Monster monster in Monsters)
        {
            if (Map.IsNearDanger(Player, monster, this) && monster.IsAlive)
            {
                if (monster is Maelstrom)
                {
                    ConsoleHelper.WriteLine("You hear the violent thrashing of vicious winds nearby.", ConsoleColor.DarkMagenta);
                }
                else if (monster is Amarok)
                {
                    ConsoleHelper.WriteLine("You smell the scent of decaying flesh from a nearby room.", ConsoleColor.DarkGreen);
                }
            }
        }

        foreach (Monster monster in Monsters)
        {
            if (monster.Location == Player.Location && !monster.IsAlive)
            {
                if (monster is Maelstrom)
                {
                    ConsoleHelper.WriteLine("The corpse of a maelstrom lies here.", ConsoleColor.DarkGray);
                }
                else if (monster is Amarok)
                {
                    ConsoleHelper.WriteLine("The corpse of an amarok lies here.", ConsoleColor.DarkGray);
                }
            }
        }
    }

    public Location PlayerAction()
    {
        string? choice = ConsoleHelper.Prompt("What would you like to do?");
        IAction? newAction = choice switch
        {
            "move north" => new MoveAction(Direction.North),
            "move south" => new MoveAction(Direction.South),
            "move east" => new MoveAction(Direction.East),
            "move west" => new MoveAction(Direction.West),
            "shoot north" => new ShootAction(Direction.North),
            "shoot south" => new ShootAction(Direction.South),
            "shoot east" => new ShootAction(Direction.East),
            "shoot west" => new ShootAction(Direction.West),
            "enable fountain" => new EnableFountainAction(),
            "help" => new HelpAction(),
            _ => new InvalidAction(),
        };

        newAction?.Execute(this);
        return Player.Location;
    }

    public void PrintTotalRuntime()
    {
        RunTime = EndTime - StartTime;
        ConsoleHelper.WriteLine($"Game Time: {RunTime.Minutes}m {RunTime.Seconds}s", ConsoleColor.White);
    }
}

// Map class is in charge of creating the blank map, helping add rooms to the map, and helping the player sense stuff.

public class Map
{
    private Room[,] _rooms;
    public int Rows { get; set; }
    public int Columns { get; set; }

    public Map(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;

        _rooms = new Room[Rows, Columns];

        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                _rooms[row, column] = Room.Empty;
            }
        }
    }

    public void SetRoom(int row, int column, Room room) => _rooms[row, column] = room;

    public Room GetRoom(int row, int column)
    {
        if (row >= 0 && row < Rows && column >= 0 && column < Columns)
        {
            return _rooms[row, column];
        }
        else
        {
            return Room.OffMap;
        }
    }

    public bool IsOnMap(Location location)
    {
        if (location.Row >= 0
            && location.Row < _rooms.GetLength(0)
            && location.Column >= 0
            && location.Column < _rooms.GetLength(1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsNearDanger(Player player, Room room)
    {
        if (GetRoom(player.Location.Row - 1, player.Location.Column) == room) { return true; }
        if (GetRoom(player.Location.Row + 1, player.Location.Column) == room) { return true; }
        if (GetRoom(player.Location.Row, player.Location.Column - 1) == room) { return true; }
        if (GetRoom(player.Location.Row, player.Location.Column + 1) == room) { return true; }
        if (GetRoom(player.Location.Row - 1, player.Location.Column - 1) == room) { return true; }
        if (GetRoom(player.Location.Row + 1, player.Location.Column + 1) == room) { return true; }
        if (GetRoom(player.Location.Row - 1, player.Location.Column + 1) == room) { return true; }
        if (GetRoom(player.Location.Row + 1, player.Location.Column - 1) == room) { return true; }
        return false;
    }

    public bool IsNearDanger(Player player, Monster monster, Game game)
    {
        if (player.Location.Row - 1 == monster.Location.Row && player.Location.Column == monster.Location.Column) { return true; }
        if (player.Location.Row + 1 == monster.Location.Row && player.Location.Column == monster.Location.Column) { return true; }
        if (player.Location.Row == monster.Location.Row && player.Location.Column - 1 == monster.Location.Column) { return true; }
        if (player.Location.Row == monster.Location.Row && player.Location.Column + 1 == monster.Location.Column) { return true; }
        if (player.Location.Row - 1 == monster.Location.Row && player.Location.Column - 1 == monster.Location.Column) { return true; }
        if (player.Location.Row + 1 == monster.Location.Row && player.Location.Column + 1 == monster.Location.Column) { return true; }
        if (player.Location.Row - 1 == monster.Location.Row && player.Location.Column + 1 == monster.Location.Column) { return true; }
        if (player.Location.Row + 1 == monster.Location.Row && player.Location.Column - 1 == monster.Location.Column) { return true; }
        return false;
    }
}

// Location is used by player, monsters, and arrows, which allows the game to know where everything is.

public record Location(int Row, int Column)
{
    public int Row { get; set; } = Row;
    public int Column { get; set; } = Column;
}

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

// Makes it easier to change text color for WriteLine statements.

public class ConsoleHelper
{
    public static string Prompt(string text)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(text + " ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        return Console.ReadLine();
    }

    public static void WriteLine(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White;
    }
}

// Used at the start of the game to tell the user what to do.

public class Intro
{
    public void PrintIntroText()
    {
        ConsoleHelper.WriteLine("Welcome to the Cavern of Objects, a maze of rooms filled with many dangers.", ConsoleColor.White);
        ConsoleHelper.WriteLine("Other than the entrance room, there is no light here. You must use your other senses to navigate the cavern.", ConsoleColor.White);
        ConsoleHelper.WriteLine("Your goal is to find the Fountain of Objects, activate it, and return to the entrance unharmed.\n", ConsoleColor.White);

        ConsoleHelper.WriteLine("Watch out for Pits! You will feel a breeze if there is a Pit in a nearby room. If you step into a Pit, you die.\n", ConsoleColor.DarkRed);

        ConsoleHelper.WriteLine("Some rooms have Maelstroms in them. You will hear violent thrashing of winds if there is a Maelstrom near you.", ConsoleColor.DarkMagenta);
        ConsoleHelper.WriteLine("If you are caught by a Maelstrom, it will throw you into a different room, and it will move to a new room as well.\n", ConsoleColor.DarkMagenta);

        ConsoleHelper.WriteLine("Amaroks, rotting wolf-like creatures, also roam the cavern. You will smell their decaying flesh if one is nearby.", ConsoleColor.DarkGreen);
        ConsoleHelper.WriteLine("If you step into a room with an Amarok, you will be killed.\n", ConsoleColor.DarkGreen);

        ConsoleHelper.WriteLine("You carry a bow and arrows. You can use them to shoot monsters.", ConsoleColor.White);
        ConsoleHelper.WriteLine("You must be right next to them and aim at the correct room.", ConsoleColor.White);
        ConsoleHelper.WriteLine("Be warned, your arrow supply is limited.\n", ConsoleColor.White);

        ConsoleHelper.WriteLine("Good luck, brave warrior. We're counting on you.\n", ConsoleColor.Magenta);

        ConsoleHelper.WriteLine("For a list of commands, type 'help' after choosing a map size.\n", ConsoleColor.Cyan);

        Console.WriteLine("------------------------------------------------\n");
    }
}

public enum Direction { North, South, East, West }
public enum Room { Empty, Entrance, Fountain, Pit, OffMap }