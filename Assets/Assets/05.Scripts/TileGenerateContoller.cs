using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerateContoller : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] tiles;

    private int boardWidth = 8;
    private int boardHeight = 6;

    void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
    {
        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                int randomIndex = Random.Range(0, tiles.Length);
                tilemap.SetTile(new Vector3Int(x, y, 0), tiles[randomIndex]);
            }
        }
    }
}
