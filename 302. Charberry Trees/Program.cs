CharberryTree tree = new CharberryTree();
Notifier notifier = new Notifier(tree);
Harvester harvester = new Harvester(tree);

while (true)
{
    tree.MaybeGrow();
}

/* Just made this part for funsies to see how quickly the computer can generate numbers. Hint: It's fast. Like 100 million loops per second or something.

//int count = 0;

//while (!tree.Ripe)
//{
//    count++;
//    tree.MaybeGrow();
//}

//Console.WriteLine($"Attempts made before success: {count}");

*/

public class CharberryTree
{
    private Random _random = new Random();
    public bool Ripe { get; set; }
    public event Action? Ripened;

    public void MaybeGrow()
    {
        if (_random.NextDouble() < 0.00000001 && !Ripe)
        {
            Ripe = true;
            Ripened?.Invoke();
        }
    }
}

public class Notifier
{
    public Notifier(CharberryTree tree)
    {
        tree.Ripened += OnRipened;
    }

    public void OnRipened() => Console.WriteLine("A charberry fruit has ripened!");
}

public class Harvester
{
    private CharberryTree _tree = new();
    private int _harvestCount;

    public Harvester(CharberryTree tree)
    {
        _tree = tree;
        tree.Ripened += OnRipened;
    }

    public void OnRipened()
    {
        _harvestCount++;
        _tree.Ripe = false;
        Console.WriteLine($"A charberry fruit has been harvested! Total fruits harvested: {_harvestCount}\n");
    }
}