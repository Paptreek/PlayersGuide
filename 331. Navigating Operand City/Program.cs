
/*
 * 
 * Add an addition (+) operator to BlockCoordinate that takes a BlockCoordinate and a BlockOffset as arguments and produces a new BlockCoordinate
 * that refers to the one you would arrive at by starting at the origianl coordinate and moving by the offset. That is, if we started at (4, 3)
 * and had an offset of (2, 0), we should end up at (6, 3).
 * 
 * Add another addition (+) operator to BlockCoordinate that takes a BlockCoordinate and a Direction as arguments and produces a new BlockCoordinate
 * that is a block in the direction indicated. If we start at (4, 3) and went east, we should end up at (4, 4).
 * 
 * Write code to ensure that both operators work correctly.
 * 
 */

BlockCoordinate start = new BlockCoordinate(4, 3);
BlockOffset offset = new BlockOffset(2, 0);
BlockCoordinate result = start + offset;

Console.WriteLine($"Result: {result}");

Console.WriteLine(result + Direction.North);
Console.WriteLine(result + Direction.East);
Console.WriteLine(result + Direction.South);
Console.WriteLine(result + Direction.West);

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
            Direction.North => new BlockOffset(1, 0),
            Direction.East => new BlockOffset(0, 1),
            Direction.South => new BlockOffset(-1, 0),
            Direction.West => new BlockOffset(0, -1)
        };
    }
}
public record BlockOffset(int RowOffset, int ColumnOffset);
public enum Direction { North, East, South, West }