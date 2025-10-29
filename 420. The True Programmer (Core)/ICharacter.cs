public interface ICharacter
{
    public string Name { get; }

    public void GetPlayerAction();
}

public class Hero : ICharacter
{
    public string Name { get; private set; } = "";

    public Hero()
    {
        Console.Write("Enter your name: ");
        Name = Console.ReadLine();
        Console.WriteLine();
    }

    public void GetPlayerAction()
    {
        Console.WriteLine($"It is {Name}'s turn...");
        Console.Write("Choose an action (1 = Skip Turn): ");

        int choice = Convert.ToInt32(Console.ReadLine());
        IAction action = choice switch
        {
            1 => new SkipTurnAction(),
        };

        action.Execute(this);
        Console.WriteLine();
    }
}

public class Skeleton : ICharacter
{
    public string Name { get; } = "SKELETON";

    public void GetPlayerAction()
    {
        Console.WriteLine($"It is {Name}'s turn...");
        Console.Write("Choose an action (1 = Skip Turn): ");

        int choice = Convert.ToInt32(Console.ReadLine());
        IAction action = choice switch
        {
            1 => new SkipTurnAction(),
        };

        action.Execute(this);
        Console.WriteLine();
    }
}