/*
 * 
 * [x] 1. The game needs to be able to represent characters who have a name and the ability to take a turn.
 * [x] 2. The game should have skeleton characters with the name SKELETON.
 * [x] 3. The game should be able to represent a party with a collection of characters.
 * [x] 4. The game should be able to run a battle composed of two parties - heroes and monsters.
 *        A battle needs to run a series of rounds where each character in each party can take a turn.
 * [x] 5. Before a character takes their turn, the game should report to the user whose turn it is.
 *        For example, "It is SKELETON's turn..."
 * [x] 6. The only action the game needs to support at this point is the action of doing nothing (skipping a turn).
 *        This action is done by displaying text about doing nothing, resting, or skipping a turn in the console window.
 *        For example, "SKELETON did NOTHING."
 * [x] 7. The game must run a battle with a single skeleton in both the hero and the monster party. At this point, the
 *        two skeletons should be doing nothing repeatedly.
 *        
 */

/*
 * 
 * Classes:
 * 
 * 1. Game
 * 2. Character
 * 3. Skeleton : Character
 * 4. Party - Has characters inside. Maybe Lists.
 * 5. Action
 * 6. Battle
 *
 */

Game game = new Game();

game.CreateGame();

while (true)
{
    game.Run();
}


public class Game
{
    private Party _party = new Party();
    private int _rounds;

    public void CreateGame()
    {
        _party.CreateHeroParty();
        _party.CreateMonsterParty();
    }
    
    public void Run()
    {
        _rounds++;
        Console.WriteLine($"Round: {_rounds}");

        foreach (Character character in _party.HeroParty)
        {
            Console.WriteLine("Hero Party:");
            character.GetPlayerAction();
        }

        foreach (Character character in _party.MonsterParty)
        {
            Console.WriteLine("Monster Party:");
            character.GetPlayerAction();
        }
    }
}

public class Party
{
    public List<Character> HeroParty { get; } = new List<Character>();
    public List<Character> MonsterParty { get; } = new List<Character>();

    // For now, these CreateParty methods will only add 1 thing. Later, they can add more, either specifics or randoms.
    public void CreateHeroParty()
    {
        HeroParty?.Add(new Skeleton()); // this will need to change to a Hero at some point
    }
    
    public void CreateMonsterParty()
    {
        MonsterParty?.Add(new Skeleton());
    }
}

public abstract class Character
{
    public abstract string Name { get; }

    public abstract void GetPlayerAction();
}

public class Skeleton : Character
{
    public override string Name { get; } = "SKELETON";

    public override void GetPlayerAction()
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

public interface IAction
{
    public void Execute(Character character);
}

public class SkipTurnAction : IAction
{
    public void Execute(Character character)
    {
        Console.WriteLine($"{character.Name} is taking a rest.");
    }
}