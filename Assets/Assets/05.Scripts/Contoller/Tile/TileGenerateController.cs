using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerateController : MonoBehaviour
{
    [SerializeField] ControllerManagementSystem controllerManagementSystem;

    [SerializeField] GameObject[] tilePrefabs;

    private int rows = 14;
    private int cols = 6;

    private bool isGenerateEven = false;
    private int generateTileNum = -1;

    public int Rows => rows;
    public int Cols => cols;

    /// <summary>
    /// Method for Tile Generation
    /// </summary>
    public void GenerateTiles()
    {
        controllerManagementSystem.TileMatchingContoller.board = new GameObject[rows, cols];

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                if (!isGenerateEven)
                    generateTileNum = Random.Range(0, tilePrefabs.Length);

                if (x == 0 || y == 0 || x == rows - 1 || y == cols - 1)
                {
                    Vector3 position = new Vector3(x * TileInfo.TileSizeX, y * TileInfo.TileSizeY, 0);
                    controllerManagementSystem.TileMatchingContoller.board[x, y] = null;
                }
                else
                {
                    Vector3 position = new Vector3(x * TileInfo.TileSizeX, y * TileInfo.TileSizeY, 0);
                    GameObject tile = Instantiate(tilePrefabs[generateTileNum], position, Quaternion.identity);
                    tile.name = $"Tile_{tile.GetComponent<Tile>().TileName}";
                    isGenerateEven = !isGenerateEven;
                    controllerManagementSystem.TileMatchingContoller.board[x, y] = tile;
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            controllerManagementSystem.TileShuffleController.ShuffleBoard(controllerManagementSystem.TileMatchingContoller.board);
        }

        // Place the mixed array in a new location
        for (int x = 1; x < rows - 1; x++) // Excluding the Edge
        {
            for (int y = 1; y < cols - 1; y++)
            {
                if (controllerManagementSystem.TileMatchingContoller.board[x, y] != null)
                {
                    controllerManagementSystem.TileMatchingContoller.board[x, y].transform.position =
                        new Vector3(x * TileInfo.TileSizeX, y * TileInfo.TileSizeY, 0);
                }
            }
        }
    }
}
