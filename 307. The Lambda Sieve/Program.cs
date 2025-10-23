// user must pick one of the three filters
// their choice should be passed in as a parameter to create a new instance of Sieve
// then the user should be asked to enter a number, and a method should decide if the number is "good" or "bad"
// UPDATE: changed the three methods to lambda expressions

// Question: Does this change make the program shorter or longer? => Shorter, as it removed the three methods and placed them directly into the constructor calls

// Question: Does this change make the program easier to read, or harder? => A bit of both. On one hand, you don't have to check multiple parts of the program to see
//           what is happening. On the other hand, method names can be very helpful in describing what's happening without having to think much.

Console.Write("Choose one (1=Even, 2=Positive, 3=MultipleOfTen): "); // user is asked to pick a function
int? input = Convert.ToInt32(Console.ReadLine());

Sieve choice = input switch
{
    1 => new Sieve(x => x % 2 == 0),
    2 => new Sieve(x => x > 0),
    3 => new Sieve(x => x % 10 == 0)
};

Sieve sieve = choice; // function is passed as a paramter to Sieve class

while (true)
{
    Console.Write("Enter a number: ");
    int chosenNumber = Convert.ToInt32(Console.ReadLine());

    string goodOrEvil = sieve.IsGood(chosenNumber) ? "good" : "evil"; // feeding the user's choice to IsGood, which now knows what to do with it
    Console.WriteLine($"That number is {goodOrEvil}!");
}

public class Sieve
{
    private Func<int, bool> _decisionFunction;

    public Sieve(Func<int, bool> decisionFunction)
    {
        _decisionFunction = decisionFunction;
    }

    public bool IsGood(int number)
    {
        return _decisionFunction(number); // chosen function (method) is used here, using a number that the user provides
    }
}