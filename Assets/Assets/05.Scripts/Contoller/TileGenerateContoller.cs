using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerateContoller : MonoBehaviour
{
    [SerializeField] ControllerManagementSystem controllerManagementSystem;

    [SerializeField] GameObject[] tilePrefabs;

    private int rows = 12;
    private int cols = 8;

    private bool isGenerateEven = false;
    private int generateTileNum = -1;

    public int Rows => rows;
    public int Cols => cols;

    void Start()
    {
        GenerateTiles();
    }

    void GenerateTiles()
    {
        controllerManagementSystem.TileMatchingContoller.board = new GameObject[rows, cols];

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                if (!isGenerateEven)
                    generateTileNum = Random.Range(0, tilePrefabs.Length - 1);

                Vector3 position = new Vector3(x * TileInfo.TileSizeX, y * TileInfo.TileSizeY, 0);
                GameObject tile = Instantiate(tilePrefabs[generateTileNum], position, Quaternion.identity);
                tile.name = $"Tile_{tile.GetComponent<Tile>().TileName}";
                controllerManagementSystem.TileMatchingContoller.board[x, y] = tile;
            }
        }
    }
}
