public interface IAction
{
    public void Execute(Party party, ICharacter character);
}
public class SkipTurnAction : IAction
{
    public void Execute(Party party, ICharacter character)
    {
        Console.WriteLine($"{character.Name} is taking a rest.");
    }
}

public class PunchAction : IAction
{
    private int _target;
    public void Execute(Party party, ICharacter character)
    {
        Console.Write($"Choose your target: ");

        foreach (ICharacter monster in party.MonsterParty)
        {
            _target++;
            Console.Write($"({_target} = {monster.Name + " " + monster.Number}) ");
        }

        Console.Write("~> ");
        int choice = Convert.ToInt32(Console.ReadLine());

        foreach (ICharacter monster in party.MonsterParty)
        {
            if (choice == monster.Number)
            {
                Console.WriteLine($"{character.Name} used PUNCH against {monster.Name + " " + monster.Number}");
            }
        }
    }
}

public class BoneCrunchAction : IAction
{
    public void Execute(Party party, ICharacter character)
    {
        foreach (ICharacter hero in party.HeroParty)
        {
            Console.WriteLine($"{character.Name} {character.Number} used BONE CRUNCH against {hero.Name}");
        }
    }
}