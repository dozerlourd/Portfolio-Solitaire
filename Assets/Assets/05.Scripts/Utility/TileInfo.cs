using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileInfo
{
    private static float tileSizeX = 0.7714f;
    private static float tileSizeY = 1f;

    public static float TileSizeX => tileSizeX;
    public static float TileSizeY => tileSizeY;

    public static Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x / (tileSizeX));
        int y = Mathf.RoundToInt(worldPos.y / (tileSizeY));
        return new Vector2Int(x, y);
    }
}
