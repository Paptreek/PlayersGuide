public class Party
{
    public List<ICharacter> HeroParty { get; } = new List<ICharacter>();
    public List<ICharacter> MonsterParty { get; } = new List<ICharacter>();

    // For now, these CreateParty methods will only add 1 thing. Later, they can add more, either specifics or randoms.
    public void CreateHeroParty()
    {
        HeroParty?.Add(new Hero()); // this will need to change to a Hero at some point
    }

    public void CreateMonsterParty()
    {
        MonsterParty?.Add(new Skeleton());
    }
}