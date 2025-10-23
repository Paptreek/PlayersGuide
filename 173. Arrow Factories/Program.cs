// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
while (true)
{

Console.Write("Welcome to Vin Fletcher's Arrows! Would you like a premade arrow, or custom? (premade, custom): ");
string choice = Console.ReadLine();

if (choice == "premade")
{
GetPremadeArrow();
break;
}
else if (choice == "custom")
{
GetCustomArrow();
break;
}
}


Arrow GetCustomArrow()
{
Arrow arrow = new Arrow(GetArrowHeadType(), GetFletchingType(), GetLength());

Console.WriteLine($"\nHere's your {arrow.ShaftLength}cm arrow with {arrow.FletchingType} fletching and {arrow.ArrowHeadType} arrowhead!");
Console.WriteLine($"\nThat's gonna run ya {arrow.GetArrowCost} gold.");

return arrow;
}

Arrow GetPremadeArrow()
{
Console.Write("\nGreat! Which arrow would you like? (beginner, marksman, elite): ");

string choice = Console.ReadLine();
Arrow response;

response = choice switch
{
"beginner" => Arrow.CreateBeginnerArrow(),
"marksman" => Arrow.CreateMarksmanArrow(),
"elite" => Arrow.CreateEliteArrow(),
_ => Arrow.CreateMarksmanArrow()
};

Arrow arrow = response;

Console.WriteLine($"\nYou chose the {choice} arrow. This {arrow.ShaftLength}cm arrow has {arrow.FletchingType} fletching and a {arrow.ArrowHeadType} arrowhead.");
Console.WriteLine($"\nThat's gonna run ya {arrow.GetArrowCost} gold.");

return arrow;
}

ArrowHeadType GetArrowHeadType()
{
Console.Write("Choose an arrowhead type (steel, wood, obsidian): ");
string input = Console.ReadLine();
return input switch
{
"steel" => ArrowHeadType.Steel,
"wood" => ArrowHeadType.Wood,
"obsidian" => ArrowHeadType.Obsidian,
_ => ArrowHeadType.Steel
};
}

FletchingType GetFletchingType()
{
Console.Write("Choose a fletching type (plastic, turkey feathers, goose feathers): ");
string input = Console.ReadLine();
return input switch
{
"plastic" => FletchingType.Plastic,
"turkey feathers" => FletchingType.TurkeyFeather,
"goose feathers" => FletchingType.GooseFeather,
_ => FletchingType.Plastic
};
}

float GetLength()
{
float shaftLength = 0;

while (shaftLength < 60 || shaftLength > 100)
{
Console.Write("Choose an arrow length in centimeters (enter a number between 60 and 100): ");
shaftLength = Convert.ToSingle(Console.ReadLine());
}

return shaftLength;
}


class Arrow
{
    public ArrowHeadType ArrowHeadType { get; }
    public FletchingType FletchingType { get; }
    public float ShaftLength { get; }


    public Arrow(ArrowHeadType arrowHeadType, FletchingType fletchingType, float shaftLength)
    {
        ArrowHeadType = arrowHeadType;
        FletchingType = fletchingType;
        ShaftLength = shaftLength;
    }

    public float GetArrowCost
    {
        get
        {
            float arrowHeadCost = ArrowHeadType switch
            {
                ArrowHeadType.Steel => 10,
                ArrowHeadType.Wood => 3,
                ArrowHeadType.Obsidian => 5,
            };

            float fletchingCost = FletchingType switch
            {
                FletchingType.Plastic => 10,
                FletchingType.GooseFeather => 3,
                FletchingType.TurkeyFeather => 5,
            };

            float shaftCost = ShaftLength * 0.05f;

            return arrowHeadCost + fletchingCost + shaftCost;
        }
    }

    public static Arrow CreateEliteArrow() => new Arrow(ArrowHeadType.Steel, FletchingType.Plastic, 90);
    public static Arrow CreateBeginnerArrow() => new Arrow(ArrowHeadType.Wood, FletchingType.GooseFeather, 75);
    public static Arrow CreateMarksmanArrow() => new Arrow(ArrowHeadType.Steel, FletchingType.GooseFeather, 65);
}

enum ArrowHeadType { Steel, Wood, Obsidian }
enum FletchingType { Plastic, TurkeyFeather, GooseFeather }