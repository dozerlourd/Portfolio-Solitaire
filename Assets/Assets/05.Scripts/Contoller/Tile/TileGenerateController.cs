using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerateController : MonoBehaviour
{
    [SerializeField] ControllerManagementSystem controllerManagementSystem;

    [SerializeField] GameObject[] tilePrefabs;

    private int rows = 14 + 2; // Horizontal Tiles number + Margin(2)
    private int cols = 6 + 2; // Vertical Tiles number + Margin(2)

    private bool isGenerateEven = false;
    private int generateTileNum = -1;

    public int Rows => rows;
    public int Cols => cols;

    TileManagementController tileManagementController;

    private void Awake() => tileManagementController = controllerManagementSystem.TileManagementController;

    /// <summary>
    /// Method for Tile Generation
    /// </summary>
    public void GenerateTiles()
    {
        tileManagementController.board = new GameObject[rows, cols];

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                if (!isGenerateEven)
                    generateTileNum = Random.Range(0, tilePrefabs.Length);

                if (x == 0 || y == 0 || x == rows - 1 || y == cols - 1)
                {
                    Vector3 position = new Vector3(x * TileGenerationInfo.TileSizeX, y * TileGenerationInfo.TileSizeY, 0);
                    tileManagementController.board[x, y] = null;
                }
                else
                {
                    Vector3 position = new Vector3(x * TileGenerationInfo.TileSizeX, y * TileGenerationInfo.TileSizeY, 0);
                    GameObject tile = Instantiate(tilePrefabs[generateTileNum], position, Quaternion.identity);
                    Tile tileComponent = tile.GetComponent<Tile>();
                    tile.name = $"Tile_{tileComponent.TileName}";
                    tileComponent.SetAudioController(controllerManagementSystem.AudioController);
                    isGenerateEven = !isGenerateEven;
                    tileManagementController.board[x, y] = tile;

                    TileManagementController.boardCount++;
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            controllerManagementSystem.TileShuffleController.ShuffleBoard(tileManagementController.board);
        }

        // Place the mixed array in a new location
        for (int x = 1; x < rows - 1; x++) // Excluding the Edge
        {
            for (int y = 1; y < cols - 1; y++)
            {
                if (tileManagementController.board[x, y] != null)
                {
                    tileManagementController.board[x, y].transform.position =
                        new Vector3(x * TileGenerationInfo.TileSizeX, y * TileGenerationInfo.TileSizeY, 0);
                }
            }
        }
    }
}
