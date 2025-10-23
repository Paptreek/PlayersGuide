namespace FountainOfObjectsGame
{
    internal partial class Program
    {
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
    }
}