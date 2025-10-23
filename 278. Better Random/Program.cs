// [x] Create a new static class to add extension methods for Random
// [x] As described above, add a NextDouble extension method that gives a maximum value for a randomly generated double
// [x] Add a NextString extension method for Random that allows you to pass in any number of string values (using params) and randomly pick one of them
// [x] Add a CoinFlip method that randomly picks a bool value. It should have an optional parameter that indicates the frequency of heads, which should default to 0.5 or 50%
// [x] Answer this question: In your opinion, would it be better to make a derived AdvancedRandom class that adds these methods or use extension methods and why?

// My Answer: From what I can tell, it seems more straightforward and clean to just use extension methods inside a normal class, rather than having to worry about
// all the stuff that goes along with derived classes.

while (true)
{
    Console.WriteLine("Press [ENTER] to print a new block of stuff.");
    Console.ReadLine();

    Console.WriteLine(RandomExtensions.NextDouble(50));
    Console.WriteLine(RandomExtensions.NextDouble(5));
    Console.WriteLine(RandomExtensions.NextDouble(100));
    Console.WriteLine(RandomExtensions.NextDouble(5));
    Console.WriteLine(RandomExtensions.NextDouble(1));

    Console.WriteLine(RandomExtensions.NextString("attack", "defend", "use spell", "flee", "move up", "move down", "move left", "move right"));

    RandomExtensions.CoinFlip();

    Console.WriteLine();
}

public static class RandomExtensions
{
    private static Random _random = new Random();

    public static double NextDouble(this double maxValue)
    {
        double result = _random.NextDouble();

        result = result * maxValue;
        return result;
    }

    public static string NextString(params string[] strings)
    {
        return strings[_random.Next(strings.Length)];
    }

    public static void CoinFlip(float chanceOfHeads = 0.5f)
    {
        double headsOrTails = _random.NextDouble();
        if (headsOrTails < chanceOfHeads)
        {
            Console.WriteLine("heads!");
        }
        else
        {
            Console.WriteLine("tails!");
        }
    }
}