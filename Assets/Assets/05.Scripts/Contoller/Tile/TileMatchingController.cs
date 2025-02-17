using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMatchingController : MonoBehaviour
{
    [SerializeField] ControllerManagementSystem controllerManagementSystem;
    public GameObject[,] board;  //2D Arrangements to Manage Pads

    private int[] dx = { 0, 0, 1, -1 }; //Right, left, down, up
    private int[] dy = { 1, -1, 0, 0 };

    [HideInInspector] public Vector2Int startVec2Int;
    [HideInInspector] public Vector2Int endVec2Int;
    [HideInInspector] bool setStartInt = false;

    private class Node
    {
        public int x, y, turn, dir;
        public Node(int x, int y, int turn, int dir)
        {
            this.x = x;
            this.y = y;
            this.turn = turn;
            this.dir = dir;
        }
    }

    public bool CanConnect(Vector2Int start, Vector2Int end)
    {
        //Check if the cards exist
        if (board[start.x, start.y] == null || board[end.x, end.y] == null) return false;
        if (board[start.x, start.y].name != board[end.x, end.y].name) return false; //Check if they're on the same page

        Queue<Node> queue = new Queue<Node>();
        bool[,,] visited = new bool[controllerManagementSystem.TileGenerateContoller.Rows, controllerManagementSystem.TileGenerateContoller.Cols, 4];

        StartCoroutine(CanConnectCoroutine(queue, visited, start, end));

        return false;
    }

    private bool IsValid(int x, int y, Vector2Int end)
    {
        //Range check
        if (x < 0 || x >= controllerManagementSystem.TileGenerateContoller.Rows || y < 0 || y >= controllerManagementSystem.TileGenerateContoller.Cols)
            return false;

        //Moveable if blank or point of arrival
        return board[x, y] == null || (x == end.x && y == end.y);
    }

    public void SetMatchingVec2Int(Vector2Int vector2Int)
    {
        if(!setStartInt)
        {
            startVec2Int = vector2Int;
            setStartInt = true;
        }
        else
        {
            endVec2Int = vector2Int;

            //Prevent continuous recognition of tiles at the same point
            if (endVec2Int == startVec2Int) return;
            
            setStartInt = false;
            CanConnect(startVec2Int, endVec2Int);
        }
    }

    IEnumerator CanConnectCoroutine(Queue<Node> queue, bool[,,] visited, Vector2Int start, Vector2Int end)
    {
        //four-way initial navigation
        for (int i = 0; i < 4; i++)
        {
            int nx = start.x + dx[i];
            int ny = start.y + dy[i];
            if (IsValid(nx, ny, end))
            {
                queue.Enqueue(new Node(nx, ny, 0, i));
                visited[nx, ny, i] = true;
            }
        }

        while (queue.Count > 0)
        {
            Node node = queue.Dequeue();

            print(node.turn <= 2);
            print(node.x == end.x);
            print(node.y == end.y);

            //Arrival point reached and deflected no more than 3 times
            if ((node.x == end.x || node.x == start.x) && (node.y == end.y || node.y == start.y) && node.turn <= 2)
            {
                InputInfo.SetApplyMouseInput = false;

                GameObject startTile = board[start.x, start.y];
                GameObject endTile = board[end.x, end.y];

                yield return new WaitForSeconds(0.9f);

                InputInfo.SetApplyMouseInput = true;

                startTile.SetActive(false);
                endTile.SetActive(false);

                board[start.x, start.y] = null;
                board[end.x, end.y] = null;

                startVec2Int = new Vector2Int(-1, -1);
                endVec2Int = new Vector2Int(-1, -1);

                //HasAvailableMove();

                break;
            }

            for (int i = 0; i < 4; i++)
            {
                int nx = node.x + dx[i];
                int ny = node.y + dy[i];
                int newTurn = (i == node.dir) ? node.turn : node.turn + 1;

                if (IsValid(nx, ny, end) && newTurn <= 2 && !visited[nx, ny, i])
                {
                    queue.Enqueue(new Node(nx, ny, newTurn, i));
                    visited[nx, ny, i] = true;
                }
            }
        }
    }
}
