namespace FountainOfObjectsGame
{
    internal partial class Program
    {
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
    }
}