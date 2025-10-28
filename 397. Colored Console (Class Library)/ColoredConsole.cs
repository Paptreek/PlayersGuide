namespace _397._Colored_Console__Class_Library_
{
    public static class ColoredConsole
    {
        public static string Prompt(string? question)
        {
            Console.WriteLine(question + " ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string? response = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            return response;
        }

        public static void WriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void Write(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
