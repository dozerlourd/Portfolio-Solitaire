using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerateContoller : MonoBehaviour
{
    [SerializeField] GameObject[] tilePrefabs;

    private int rows = 12;
    private int cols = 8;
    private float tileSizeX = 0.7714f;
    private float tileSizeY = 1f;

    private bool isGenerateEven = false;
    private int generateTileNum = -1;

    void Start()
    {
        GenerateTiles();
    }

    void GenerateTiles()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                if (!isGenerateEven)
                    generateTileNum = Random.Range(0, tilePrefabs.Length - 1);

                Vector3 position = new Vector3(x * tileSizeX, y * tileSizeY, 0);
                GameObject tile = Instantiate(tilePrefabs[generateTileNum], position, Quaternion.identity);
                tile.name = $"Tile_{x}_{y}";
                tile.AddComponent<Tile>();
            }
        }
    }
}
