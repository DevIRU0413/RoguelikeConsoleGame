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
    private int step = 0;

    // Tiles Data
    public TileInfo[] TileInfos { get; private set; }

    public void Init(TileInfo[] tileInfos, Dictionary<int, int[,]> mapTileNums, int step = 1)
    {
        TileInfos = tileInfos;
        floorMaps = mapTileNums;

        // 존재 하지않은 층 넣어줬을 때
        if (!floorMaps.ContainsKey(step))
        {
            foreach (int key in floorMaps.Keys)
            {
                step = key;
                break;
            }
        }
        // 넣어주기
        this.step = step;
    }

    public bool IsInMap(int step, Position pos)
    {
        if (!floorMaps.ContainsKey(step)) return false;
        int[,] currentMap = floorMaps[step];
        return (pos.x >= 0 && pos.y >= 0
            && pos.x < currentMap.GetLength(1) && pos.y < currentMap.GetLength(0));
    }
    public bool IsInMap(Position pos)
    {
        return IsInMap(this.step, pos);
    }
    public bool IsPosMovable(int step, Position pos)
    {
        if (!floorMaps.ContainsKey(step)) return false;
        int[,] currentMap = floorMaps[step];
        return TileInfos[currentMap[pos.y, pos.x]].tileMovable;
    }
    public bool IsPosMovable(Position pos)
    {
        return IsPosMovable(this.step, pos);
    }

    public TileInfo GetTileInfo(int step, Position pos)
    {
        return TileInfos[floorMaps[step][pos.y, pos.x]];
    }
    public TileInfo GetTileInfo(Position pos)
    {
        return GetTileInfo(this.step, pos);
    }

    // 맵 특수문자 출력(그림용)
    public void DrawMapTile(int step, Position pos)
    {
        if (!floorMaps.ContainsKey(step)) return;
        int[,] currentMap = floorMaps[step];
        Console.SetCursorPosition(pos.x, pos.y);
        for (int y = 0; y < currentMap.GetLength(0); y++)
        {
            for (int x = 0; x < currentMap.GetLength(1); x++)
            {
                int tileNum = currentMap[y,x];

                Console.ForegroundColor = TileInfos[tileNum].tileColor;
                Console.Write($" {TileInfos[tileNum].tileChar}");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }

    public void DrawMapTile(Position pos)
    {
        DrawMapTile(this.step, pos);
    }

    // 맵 번호 출력(디버그용)

    public void PrintMapTileNum(int step, Position pos)
    {
        Console.SetCursorPosition(pos.x, pos.y);
        int[,] currentMap = floorMaps[step];
        Console.SetCursorPosition(pos.x, pos.y);
        for (int y = 0; y < currentMap.GetLength(0); y++)
        {
            for (int x = 0; x < currentMap.GetLength(1); x++)
            {
                int tileNum = currentMap[y,x];
                Console.Write($" {tileNum}");
            }
            Console.WriteLine();
        }
    }
    public void PrintMapTileNum(Position pos)
    {
        PrintMapTileNum(this.step, pos);
    }
    // 층수변경
    public void ChangeFloor(int newFloor)
    {
        if(floorMaps.ContainsKey(newFloor))
        {
            this.step = newFloor;
        }
        else
        {
            Console.WriteLine("오류");
        }
    }
}
