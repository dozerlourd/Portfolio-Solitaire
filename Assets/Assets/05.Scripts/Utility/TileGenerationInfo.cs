using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileGenerationInfo
{
    private static float tileSizeX = 0.5143229f;
    private static float tileSizeY = 0.6666667f;

    public static float TileSizeX => tileSizeX;
    public static float TileSizeY => tileSizeY;

    public static Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x / (tileSizeX));
        int y = Mathf.RoundToInt(worldPos.y / (tileSizeY));
        return new Vector2Int(x, y);
    }
}

public static class InputInfo
{
    private static bool isApplyMouseInput = true;

    public static bool IsApplyMouseInput => isApplyMouseInput;
    public static bool SetApplyMouseInput { set => isApplyMouseInput = value; }
}
