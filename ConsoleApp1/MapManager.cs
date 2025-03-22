using System;
using Enums;

public class MapManager
{
    // 싱클톤
    private static MapManager managerSingleton;
    public static MapManager Instance()
    {
        if(managerSingleton == null)
        {
            managerSingleton = new MapManager();
        }
        return managerSingleton;
    }

    // Tiles Data
    Tile tile = null;

    public void SetTiles()
    {

    }
}
