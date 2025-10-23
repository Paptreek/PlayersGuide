// when the program starts, ask the user to enter their name
// by default, the player starts with a score of 0
// add 1 point to their score for every keypress they make
// display the player's score after each keypress
// when the player presses ENTER, end the game (there's a hint for this)
// also upon ENTER, save their score in a file (there's a hint for this too)
// when a user enters a name at the start, if they already have a previously saved score, start with that score instead of 0

Console.Write("Welcome. Please enter your name: ");
string? name = Console.ReadLine();
int score = 0;

if (File.Exists($"{name}.txt"))
{
    string readScore = File.ReadAllText($"{name}.txt");
    score = Convert.ToInt32(readScore);
}

Console.WriteLine($"Your starting score is: {score}");

while (true)
{
    Console.Write("Key pressed: ");

    if (Console.ReadKey().Key == ConsoleKey.Enter)
    {
        Console.Clear();
        File.WriteAllText($"{name}.txt", Convert.ToString(score));
        Console.WriteLine($"Total Score: {score}");
        break;
    }
    else
    {
        score++;
        Console.Write($" | New Score: {score}\n");
    }
}