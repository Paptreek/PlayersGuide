public class Game
{
    private Party _party = new();
    private int _round;

    public void CreateGame()
    {
        _party.CreateHeroParty();
        _party.CreateMonsterParty();
    }

    public void Run()
    {
        _round++;
        Console.WriteLine($"Round: {_round}");

        foreach (ICharacter character in _party.HeroParty)
        {
            Console.WriteLine("Hero Party:");
            character.GetPlayerAction();
        }

        foreach (ICharacter character in _party.MonsterParty)
        {
            Console.WriteLine("Monster Party:");
            character.GetPlayerAction();
        }
    }
}