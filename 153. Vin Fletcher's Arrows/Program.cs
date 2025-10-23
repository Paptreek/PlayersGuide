Arrow arrow = new Arrow(GetArrowHeadType(), GetFletchingType(), GetLength());

Console.WriteLine(arrow.GetArrowHeadType());

Console.WriteLine($"\nHere's your {arrow._shaftLength}cm arrow with {arrow._fletchingType} fletching and {arrow._arrowHeadType} arrowhead!");
Console.WriteLine($"\nThat's gonna run ya {arrow.GetArrowCost()} gold.");


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
    public ArrowHeadType _arrowHeadType { get; private set; }
    public FletchingType _fletchingType {get; private set; }
    public float _shaftLength {get; private set; }


    public Arrow(ArrowHeadType arrowHeadType, FletchingType fletchingType, float shaftLength)
    {
        _arrowHeadType = arrowHeadType;
        _fletchingType = fletchingType;
        _shaftLength = shaftLength;
    }

    public ArrowHeadType GetArrowHeadType() => _arrowHeadType;
    public FletchingType GetFletchingType() => _fletchingType;
    public float GetLength() => _shaftLength;

    public float GetArrowCost()
    {
        float arrowHeadCost = _arrowHeadType switch
        {
            ArrowHeadType.Steel => 10,
            ArrowHeadType.Wood => 3,
            ArrowHeadType.Obsidian => 5,
        };

        float fletchingCost = _fletchingType switch
        {
            FletchingType.Plastic => 10,
            FletchingType.GooseFeather => 3,
            FletchingType.TurkeyFeather => 5,
        };

        float shaftCost = _shaftLength * 0.05f;

        return arrowHeadCost + fletchingCost + shaftCost;
    }
}

enum ArrowHeadType { Steel, Wood, Obsidian }
enum FletchingType { Plastic, TurkeyFeather, GooseFeather }