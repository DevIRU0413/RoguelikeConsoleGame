using System.Numerics;

namespace Structs
{
    public struct Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public static Position operator +(Position lhs, Position rhs)
        {
            Position nv = new Position();
            nv.x = lhs.x + rhs.x;
            nv.y = lhs.y + rhs.y;

            return nv;
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