namespace FountainOfObjectsGame
{
    internal partial class Program
    {
        // Map class is in charge of creating the blank map, helping add rooms to the map, and helping the player sense stuff.

        public class Map
        {
            private Room[,] _rooms;
            public int Rows { get; set; }
            public int Columns { get; set; }

            public Map(int rows, int columns)
            {
                Rows = rows;
                Columns = columns;

                _rooms = new Room[Rows, Columns];

                for (int row = 0; row < Rows; row++)
                {
                    for (int column = 0; column < Columns; column++)
                    {
                        _rooms[row, column] = Room.Empty;
                    }
                }
            }

            public void SetRoom(int row, int column, Room room) => _rooms[row, column] = room;

            public Room GetRoom(int row, int column)
            {
                if (row >= 0 && row < Rows && column >= 0 && column < Columns)
                {
                    return _rooms[row, column];
                }
                else
                {
                    return Room.OffMap;
                }
            }

            public bool IsOnMap(Location location)
            {
                if (location.Row >= 0
                    && location.Row < _rooms.GetLength(0)
                    && location.Column >= 0
                    && location.Column < _rooms.GetLength(1))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool IsNearDanger(Player player, Room room)
            {
                if (GetRoom(player.Location.Row - 1, player.Location.Column) == room) { return true; }
                if (GetRoom(player.Location.Row + 1, player.Location.Column) == room) { return true; }
                if (GetRoom(player.Location.Row, player.Location.Column - 1) == room) { return true; }
                if (GetRoom(player.Location.Row, player.Location.Column + 1) == room) { return true; }
                if (GetRoom(player.Location.Row - 1, player.Location.Column - 1) == room) { return true; }
                if (GetRoom(player.Location.Row + 1, player.Location.Column + 1) == room) { return true; }
                if (GetRoom(player.Location.Row - 1, player.Location.Column + 1) == room) { return true; }
                if (GetRoom(player.Location.Row + 1, player.Location.Column - 1) == room) { return true; }
                return false;
            }

            public bool IsNearDanger(Player player, Monster monster, Game game)
            {
                if (player.Location.Row - 1 == monster.Location.Row && player.Location.Column == monster.Location.Column) { return true; }
                if (player.Location.Row + 1 == monster.Location.Row && player.Location.Column == monster.Location.Column) { return true; }
                if (player.Location.Row == monster.Location.Row && player.Location.Column - 1 == monster.Location.Column) { return true; }
                if (player.Location.Row == monster.Location.Row && player.Location.Column + 1 == monster.Location.Column) { return true; }
                if (player.Location.Row - 1 == monster.Location.Row && player.Location.Column - 1 == monster.Location.Column) { return true; }
                if (player.Location.Row + 1 == monster.Location.Row && player.Location.Column + 1 == monster.Location.Column) { return true; }
                if (player.Location.Row - 1 == monster.Location.Row && player.Location.Column + 1 == monster.Location.Column) { return true; }
                if (player.Location.Row + 1 == monster.Location.Row && player.Location.Column - 1 == monster.Location.Column) { return true; }
                return false;
            }
        }
    }
}