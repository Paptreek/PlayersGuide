// user must pick one of the three filters
// their choice should be passed in as a parameter to create a new instance of Sieve
// then the user should be asked to enter a number, and a method should decide if the number is "good" or "bad"

Console.Write("Choose one (1=Even, 2=Positive, 3=MultipleOfTen): "); // user is asked to pick a function
int? input = Convert.ToInt32(Console.ReadLine());

Sieve choice = input switch
{
    1 => new Sieve(IsEven),
    2 => new Sieve(IsPositive),
    3 => new Sieve(IsMultipleOfTen)
};

Sieve sieve = choice; // function is passed as a paramter to Sieve class

while (true)
{
    Console.Write("Enter a number: ");
    int chosenNumber = Convert.ToInt32(Console.ReadLine());

    string goodOrEvil = sieve.IsGood(chosenNumber) ? "good" : "evil"; // feeding the user's choice to IsGood, which now knows what to do with it
    Console.WriteLine($"That number is {goodOrEvil}!");
}

bool IsEven(int number) => number % 2 == 0;
bool IsPositive(int number) => number > 0;
bool IsMultipleOfTen(int number) => number % 10 == 0;

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