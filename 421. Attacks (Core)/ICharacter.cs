public interface ICharacter
{
    public string Name { get; }
    public int Number { get; }

    public void GetPlayerAction(Game game);
}

public class Hero : ICharacter
{
    public string Name { get; private set; } = "";
    public int Number { get; }

    public Hero()
    {
        Console.Write($"Enter your name: ");
        Name = Console.ReadLine();
        Console.WriteLine();
    }

    public void GetPlayerAction(Game game)
    {
        Console.WriteLine($"It is {Name}'s turn...");
        Console.Write("Choose an action: (1 = Skip Turn) (2 = Punch) ~> ");

        int choice = Convert.ToInt32(Console.ReadLine());
        IAction action = choice switch
        {
            1 => new SkipTurnAction(),
            2 => new PunchAction(),
        };

        action.Execute(game.Party, this);
        Console.WriteLine();
    }
}

public class Skeleton : ICharacter
{
    public string Name { get; } = "SKELETON";
    public int Number { get; }
    private Random _random = new();

    public Skeleton(int number)
    {
        Number = number;
    }

    public void GetPlayerAction(Game game)
    {
        Console.WriteLine($"It is {Name} {Number}'s turn...");

        int choice = _random.Next(1, 3);
        IAction action = choice switch
        {
            1 => new SkipTurnAction(),
            2 => new BoneCrunchAction(),
        };

        action.Execute(game.Party, this);
        Console.WriteLine();
    }
}