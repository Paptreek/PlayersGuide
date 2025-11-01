public class Game
{
    public Party Party = new();
    private int _round;

    public void CreateGame()
    {
        Party.CreateHeroParty();
        Party.CreateMonsterParty();
    }

    public void Run()
    {
        _round++;
        Console.WriteLine($"Round: {_round}\n");

        foreach (ICharacter character in Party.HeroParty)
        {
            //Console.WriteLine("Hero Party:");
            character.GetPlayerAction(this);
        }

        foreach (ICharacter character in Party.MonsterParty)
        {
            //Console.WriteLine("Monster Party:");
            character.GetPlayerAction(this);
        }
    }
}