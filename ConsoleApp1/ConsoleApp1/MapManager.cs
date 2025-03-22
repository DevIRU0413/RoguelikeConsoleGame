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
            {
                initialized = new MapManager();
            }
            return initialized;
        }
    }

    // Tiles Data
    TileInfo[] tileInfos;

    // Tile num Array
    int[,] mapTiles;

    public void Init(TileInfo[] tileInfos, int[,] mapTileNums)
    {
        SetTileInfoDatas(tileInfos);
        SetMapData(mapTileNums);
    }

    // 각 번호에 대한 타일 정보 받기
    private void SetTileInfoDatas(TileInfo[] tileInfos)
    {
        this.tileInfos = tileInfos;
    }
    // 타일 숫자로된 배일 받기
    private void SetMapData(int[,] mapTileNums)
    {
        mapTiles = mapTileNums;
    }

    // 맵 특수문자 출력(그림용)
    public void DrawMapTile(Position pos)
    {
        Console.SetCursorPosition(pos.x, pos.y);
        for (int y = 0; y < mapTiles.GetLength(0); y++)
        {
            for (int x = 0; x < mapTiles.GetLength(1); x++)
            {
                int tileNum = mapTiles[y,x];

                Console.ForegroundColor = tileInfos[tileNum].tileColor;
                Console.Write($" {tileInfos[tileNum].tileChar}");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }
    // 맵 번호 출력(디버그용)
    public void PrintMapTileNum(Position pos)
    {
        Console.SetCursorPosition(pos.x, pos.y);
        for (int y = 0; y < mapTiles.GetLength(0); y++)
        {
            for (int x = 0; x < mapTiles.GetLength(1); x++)
            {
                int tileNum = mapTiles[y,x];
                Console.Write($" {tileNum}");
            }
            Console.WriteLine();
        }
    }
}
