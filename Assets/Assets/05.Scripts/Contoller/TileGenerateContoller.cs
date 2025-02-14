using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerateContoller : MonoBehaviour
{
    [SerializeField] GameObject[] tilePrefabs;

    private int rows = 12;
    private int cols = 8;
    public float tileSizeX = 1.2f;
    public float tileSizeY = 1.2f;

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
                Vector3 position = new Vector3(x * tileSizeX, y * tileSizeY, 0);
                GameObject tile = Instantiate(tilePrefabs[Random.Range(0, tilePrefabs.Length - 1)], position, Quaternion.identity);
                tile.name = $"Tile_{x}_{y}";
                tile.AddComponent<Tile>();
            }
        }
    }
}
