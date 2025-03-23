using System;
using Enums;
using Structs;

public class MapManager
{
    private static MapManager initialized;
    // 싱클톤
    public static MapManager Singleton
    {
        get
        {
            if (initialized == null)
                initialized = new MapManager();
            return initialized;
        }
    }

    private Dictionary<int, int[,]> floorMaps = new Dictionary<int, int[,]>();
    
    // Tiles Data
    public TileInfo[] TileInfos { get; private set; }

    // Tile num Array
    public int[,] MapTiles { get; private set; }

    public void Init(TileInfo[] tileInfos, Dictionary<int, int[,]> mapTileNums)
    {
        TileInfos = tileInfos;
        floorMaps = mapTileNums;
    }

    public bool IsInMap(Position pos)
    {
        if (!floorMaps.ContainsKey(pos.floor)) return false;
        int[,] currentMap = floorMaps[pos.floor];
        return (pos.x >= 0 && pos.y >= 0
            && pos.x < MapTiles.GetLength(1) && pos.y < MapTiles.GetLength(0));
    }
    public bool IsPosMovable(Position pos)
    {
        if (!floorMaps.ContainsKey(pos.floor)) return false;
        int[,] currentMap = floorMaps[pos.floor];
        return GetTileInfo(pos).tileMovable;
    }

    public TileInfo GetTileInfo(Position pos)
    {
        return TileInfos[MapTiles[pos.y, pos.x]];
    }

    // 맵 특수문자 출력(그림용)
    public void DrawMapTile(Position pos)
    {
        if (!floorMaps.ContainsKey(pos.floor)) return;
        int[,] currentMap = floorMaps[pos.floor];
        Console.SetCursorPosition(pos.x, pos.y);
        for (int y = 0; y < MapTiles.GetLength(0); y++)
        {
            for (int x = 0; x < MapTiles.GetLength(1); x++)
            {
                int tileNum = MapTiles[y,x];

                Console.ForegroundColor = TileInfos[tileNum].tileColor;
                Console.Write($" {TileInfos[tileNum].tileChar}");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }

    public void ChangeFloor(Position pos, int newFloor)
    {
        if (floorMaps.ContainsKey(newFloor))
        {
            pos.floor = newFloor;
        }
    }
    // 맵 번호 출력(디버그용)
    public void PrintMapTileNum(Position pos)
    {
        Console.SetCursorPosition(pos.x, pos.y);
        for (int y = 0; y < MapTiles.GetLength(0); y++)
        {
            for (int x = 0; x < MapTiles.GetLength(1); x++)
            {
                int tileNum = MapTiles[y,x];
                Console.Write($" {tileNum}");
            }
            Console.WriteLine();
        }
    }
}
