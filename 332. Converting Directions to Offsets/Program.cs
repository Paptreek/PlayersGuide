
BlockCoordinate start = new BlockCoordinate(0, 0);

BlockOffset offset = new BlockOffset(2, 0);
BlockCoordinate result = start + offset;

Console.WriteLine($"Result: {start}");

Console.WriteLine($"North: {start + Direction.North}");
Console.WriteLine($"East: {start + Direction.East}");
Console.WriteLine($"South: {start + Direction.South}");
Console.WriteLine($"West: {start + Direction.West}");

Console.WriteLine($"Print with indexers: ({result[0]}, {result[1]})");

BlockOffset moveNorth = (BlockOffset)Direction.North; // I chose explicit here to avoid any unexpected issues with reference types.

Console.WriteLine(moveNorth);

public record BlockCoordinate(int Row, int Column)
{
    public static BlockCoordinate operator +(BlockCoordinate start, BlockOffset offset)
    {
        return new BlockCoordinate(start.Row + offset.RowOffset, start.Column + offset.ColumnOffset);
    }

    public static BlockCoordinate operator +(BlockCoordinate start, Direction direction)
    {
        return start + direction switch
        {
            Direction.North => new BlockOffset(-1, 0),
            Direction.East => new BlockOffset(0, 1),
            Direction.South => new BlockOffset(1, 0),
            Direction.West => new BlockOffset(0, -1)
        };
    }

    public int this[int index] => index switch { 0 => Row, 1 => Column };
}
public record BlockOffset(int RowOffset, int ColumnOffset)
{
    public static explicit operator BlockOffset(Direction direction)
    {
        return direction switch
        {
            Direction.North => new BlockOffset(-1, 0),
            Direction.East => new BlockOffset(0, 1),
            Direction.South => new BlockOffset(1, 0),
            Direction.West => new BlockOffset(0, -1)
        };
    }
}
public enum Direction { North, East, South, West }