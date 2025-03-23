using System.Numerics;

namespace Structs
{
    public struct Position
    {
        public int x;
        public int y;
        public int floor;

        public Position(int x, int y,int floor=1)
        {
            this.x = x;
            this.y = y;
            this.floor = floor;
        }
        public static Position operator +(Position lhs, Position rhs)
        {
            return new Position(lhs.x + rhs.x, lhs.y + rhs.y, lhs.floor);
        }
    }

    public struct TileInfo
    {
        public int tileID;
        public char tileChar;
        public ConsoleColor tileColor;
        public bool tileMovable;

        public TileInfo(int tileID, char tileChar, ConsoleColor tileColor, bool tileMovable)
        {
            this.tileID = tileID;
            this.tileChar = tileChar;
            this.tileColor = tileColor;
            this.tileMovable = tileMovable;
        }
    }
}