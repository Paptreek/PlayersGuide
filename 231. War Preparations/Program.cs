Sword sword = new Sword(Material.Iron, Gemstone.None, 30, 7);
Sword sword2 = sword with { Material = Material.Steel, Gemstone = Gemstone.Diamond };
Sword sword3 = sword with { Material = Material.Binarium, Gemstone = Gemstone.Bitstone };

Console.WriteLine(sword);
Console.WriteLine(sword2);
Console.WriteLine(sword3);

public record Sword(Material Material, Gemstone Gemstone, int Length, int CrossguardWidth);

public enum Material { Wood, Bronze, Iron, Steel, Binarium }
public enum Gemstone { Emerald, Amber, Sapphire, Diamond, Bitstone, None }