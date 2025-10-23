// Create a program that asks the user for an int value. Use the static int.TryParse(string s, out int result) method to parse the number. Loop until they enter a valid value.
// Extend the program to do the same for both double and bool.

while (true)
{
    Console.Write("Please enter an integer, a number, or a bool: ");
    string? input = Console.ReadLine();

    if (int.TryParse(input, out int intResult))
    {
        Console.WriteLine($"You entered {intResult}");
        break;
    }
    else if (double.TryParse(input, out double doubleResult))
    {
        Console.WriteLine($"You entered {doubleResult}");
        break;
    }
    else if (bool.TryParse(input, out bool boolResult))
    {
        Console.WriteLine($"You entered {boolResult}");
        break;
    }
    else
    {
        Console.WriteLine("That is not a number!");
        continue;
    }
}