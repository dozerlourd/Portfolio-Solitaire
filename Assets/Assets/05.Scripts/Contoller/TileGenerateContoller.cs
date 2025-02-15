using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerateContoller : MonoBehaviour
{
    [SerializeField] ControllerManagementSystem controllerManagementSystem;

    [SerializeField] GameObject[] tilePrefabs;

    private int rows = 14;
    private int cols = 10;

    private bool isGenerateEven = false;
    private int generateTileNum = -1;

    public int Rows => rows;
    public int Cols => cols;

    void Start()
    {
        GenerateTiles();
    }

    /// <summary>
    /// Method for Tile Generation
    /// </summary>
    void GenerateTiles()
    {
        controllerManagementSystem.TileMatchingContoller.board = new GameObject[rows, cols];

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                if (!isGenerateEven)
                    generateTileNum = Random.Range(0, tilePrefabs.Length - 1);

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

        ShuffleBoard(controllerManagementSystem.TileMatchingContoller.board);

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

    /// <summary>
    /// Method for Tile Shuffle
    /// </summary>
    /// <param name="board"></param>
    public static void ShuffleBoard(GameObject[,] board)
    {
        int rows = board.GetLength(0);
        int cols = board.GetLength(1);

        // Convert internal tiles only to 1D arra
        int totalTiles = (rows - 2) * (cols - 2); // Excluding the Edge
        GameObject[] tempList = new GameObject[totalTiles];
        int index = 0;

        for (int x = 1; x < rows - 1; x++)
        {
            for (int y = 1; y < cols - 1; y++)
            {
                tempList[index++] = board[x, y];
            }
        }

        // Mixing Tiles
        for (int i = totalTiles - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (tempList[i], tempList[randomIndex]) = (tempList[randomIndex], tempList[i]); // Swap
        }

        // Convert internal tiles back to 2D array
        index = 0;
        for (int x = 1; x < rows - 1; x++)
        {
            for (int y = 1; y < cols - 1; y++)
            {
                board[x, y] = tempList[index++];
            }
        }
    }
}
