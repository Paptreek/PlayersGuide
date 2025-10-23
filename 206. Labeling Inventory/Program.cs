Pack pack = new Pack(10, 15, 10);

Arrow arrow = new Arrow();

while (true)
{
    Console.Write("What would you like to add to your pack? (arrow, bow, rope, water, food, sword): ");

    string choice = Console.ReadLine();

    InventoryItem? newItem = choice switch
    {
        "arrow" => new Arrow(),
        "bow" => new Bow(),
        "rope" => new Rope(),
        "water" => new Water(),
        "food" => new Food(),
        "sword" => new Sword(),
    };

    if (pack.Add(newItem))
    {
        pack.PrintItemAdded(choice);
    }
    else if (!pack.Add(newItem))
    {
        pack.PrintItemNotAdded(choice);
    }

    Console.WriteLine("\n--------------------------------------------------------");
    Console.WriteLine("Pack Status:");
    Console.WriteLine($"\nItems: {pack.CurrentItems}/{pack.Items.Length}, Weight: {pack.CurrentWeight}/{pack.MaxWeight}, Volume: {pack.CurrentVolume}/{pack.MaxVolume}\n");

    Console.WriteLine($"Item List:\n");

    foreach (InventoryItem? item in pack.Items)
    {
        if (item != null)
        {
            Console.WriteLine($"Item: {item.ToString()}\t Weight: {item.Weight}\t Volume: {item.Volume}");
        }
    }

    Console.WriteLine("--------------------------------------------------------");
    Console.WriteLine();
}

public class Pack
{
    public float MaxWeight { get; }
    public float MaxVolume { get; }

    public InventoryItem[] Items;

    public int CurrentItems { get; private set; }
    public float CurrentWeight { get; private set; }
    public float CurrentVolume { get; private set; }

    public Pack(int maxItems, float maxWeight, float maxVolume)
    {
        Items = new InventoryItem[maxItems];
        MaxWeight = maxWeight;
        MaxVolume = maxVolume;
    }

    public bool Add(InventoryItem item)
    {
        if (CurrentItems + 1 > Items.Length) return false;
        if (CurrentWeight + item.Weight >= MaxWeight) return false;
        if (CurrentVolume + item.Volume >= MaxVolume) return false;

        Items[CurrentItems] = item;
        CurrentItems++;
        CurrentWeight += item.Weight;
        CurrentVolume += item.Volume;
        return true;
    }

    public override string ToString()
    {
        foreach (InventoryItem item in Items)
        {
            if (item != null)
            {
                Console.Write(item.ToString());
            }
        }

        return "fail";
    }

    public void PrintItemAdded(string choice)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{choice} added!");
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public void PrintItemNotAdded(string choice)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nCould not add {choice}. Pack may be too full, or you entered an invalid selection.");
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}

public class InventoryItem
{
    public string Name { get; }
    public float Weight { get; }
    public float Volume { get; }

    public InventoryItem(string name, float weight, float volume)
    {
        Name = name;
        Weight = weight;
        Volume = volume;
    }
}

public class Arrow() : InventoryItem("arrow", 0.1f, 0.05f)
{
    public override string ToString()
    {
        return "Arrow";
    }
}

public class Bow() : InventoryItem("bow", 1, 4)
{
    public override string ToString()
    {
        return "Bow";
    }
}

public class Rope() : InventoryItem("rope", 1, 1.5f)
{
    public override string ToString()
    {
        return "Rope";
    }
}

public class Water() : InventoryItem("water", 2, 3)
{
    public override string ToString()
    {
        return "Water";
    }
}

public class Food() : InventoryItem("food", 1, 0.5f)
{
    public override string ToString()
    {
        return "Food";
    }
}

public class Sword() : InventoryItem("sword", 5, 3)
{
    public override string ToString()
    {
        return "Sword";
    }
}