// [ ] the game will pick a random number between 0 and 9 to represent the oatmeal raisin cookie
// [ ] the game will allow players to take turns picking between 0 and 9
// [ ] if a player repeats a number that has already been used, the program should make them select another.
// hint: can use List<int> to store previously chosen numbers, can use its Contains method to see if a number has been used before
// [ ] if the number matches the one the game picked initially, an exception should be thrown, terminating the program. run the program at least once like this to see it crash
// [ ] put in a try/catch block to handle the exception and display the results

// question: did you make a custom exception type or use an existing one? why?

// question: you could write this without exceptions, but the challenge demanded it. if you didn't have that req, what would you do? and why?

Random random = new();
List<int> alreadyChosenNumbers = new List<int>();
int oatmealCookie = random.Next(10);
int playerNumber = -1;

while (playerNumber != oatmealCookie)
{
    try
    {
        Console.WriteLine(oatmealCookie); // temporary

        while (playerNumber != oatmealCookie)
        {
            Console.Write("Pick a number between 0 and 9: ");
            string? choice = Console.ReadLine();
            playerNumber = Convert.ToInt32(choice);

            if (playerNumber < 0 || playerNumber > 9)
            {
                Console.WriteLine($"{choice} is not between 0 and 9. Try again.");
                playerNumber = -1;
            }
            else if (alreadyChosenNumbers.Contains(playerNumber))
            {
                Console.WriteLine("That number has already been picked. Try again.");
            }

            alreadyChosenNumbers.Add(playerNumber);

            if (playerNumber == oatmealCookie)
            {
                throw new Exception();
            }
        }
    }
    catch (FormatException)
    {
        Console.WriteLine($"That is not a number. Please try again.");
    }
    catch (Exception)
    {
        Console.WriteLine("You chose the bad cookie. You lose!");
    }
}