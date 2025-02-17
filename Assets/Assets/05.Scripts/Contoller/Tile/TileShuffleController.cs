using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileShuffleController : MonoBehaviour
{
    /// <summary>
    /// Method for Tile Shuffle
    /// </summary>
    /// <param name="board"></param>
    public void ShuffleBoard(GameObject[,] board)
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
