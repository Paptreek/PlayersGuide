namespace FountainOfObjectsGame
{
    //internal partial class Program
    //{
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
}
//}