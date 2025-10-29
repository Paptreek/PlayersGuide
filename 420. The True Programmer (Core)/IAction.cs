public interface IAction
{
    public void Execute(ICharacter character);
}
public class SkipTurnAction : IAction
{
    public void Execute(ICharacter character)
    {
        Console.WriteLine($"{character.Name} is taking a rest.");
    }
}