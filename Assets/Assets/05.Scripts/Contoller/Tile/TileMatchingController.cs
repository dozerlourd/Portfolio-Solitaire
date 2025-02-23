using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMatchingController : MonoBehaviour
{
    [SerializeField] ControllerManagementSystem controllerManagementSystem;
    
    [SerializeField] AudioClip correctSound;
    [SerializeField] AudioClip wrongSound;

    private int[] dx = { 0, 0, 1, -1 }; //Right, left, down, up
    private int[] dy = { 1, -1, 0, 0 };

    [HideInInspector] public Vector2Int startVec2Int;
    [HideInInspector] public Vector2Int endVec2Int;
    [HideInInspector] bool setStartInt = false;

    TileManagementController tileManagementController;

    private void Awake() => tileManagementController = controllerManagementSystem.TileManagementController;

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
        if (tileManagementController.board[start.x, start.y] == null || tileManagementController.board[end.x, end.y] == null)
        {
            controllerManagementSystem.AudioController.VfxSource.Stop();
            controllerManagementSystem.AudioController.PlayVFXSound(wrongSound, 0.5f);

            tileManagementController.board[start.x, start.y].GetComponent<Tile>().WrongAnimation();
            tileManagementController.board[end.x, end.y].GetComponent<Tile>().WrongAnimation();

            return false;
        }

        if (tileManagementController.board[start.x, start.y].name != tileManagementController.board[end.x, end.y].name)
        {
            controllerManagementSystem.AudioController.VfxSource.Stop();
            controllerManagementSystem.AudioController.PlayVFXSound(wrongSound, 0.5f);

            tileManagementController.board[start.x, start.y].GetComponent<Tile>().WrongAnimation();
            tileManagementController.board[end.x, end.y].GetComponent<Tile>().WrongAnimation();

            return false; //Check if they're on the same page
        }

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
        return tileManagementController.board[x, y] == null || (x == end.x && y == end.y);
    }

    public void SetMatchingVec2Int(Vector2Int vector2Int)
    {
        if (!setStartInt)
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
        isMatching = false;
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

            //Arrival point reached and deflected no more than 3 times
            if ((node.x == end.x || node.x == start.x) && (node.y == end.y || node.y == start.y) && node.turn <= 2)
            {
                InputInfo.SetApplyMouseInput = false;

                GameObject startTile = tileManagementController.board[start.x, start.y];
                GameObject endTile = tileManagementController.board[end.x, end.y];

                yield return new WaitForSeconds(0.3f);

                controllerManagementSystem.AudioController.ClickSource.Stop();
                controllerManagementSystem.AudioController.PlayVFXSound(correctSound, 0.5f);

                yield return new WaitForSeconds(0.2f);

                startTile.GetComponent<Tile>().CorrectAnimation();
                startTile.GetComponent<Tile>().CorrectBoard();

                endTile.GetComponent<Tile>().CorrectAnimation();
                endTile.GetComponent<Tile>().CorrectBoard();

                yield return new WaitForSeconds(0.4f);

                InputInfo.SetApplyMouseInput = true;

                startTile.SetActive(false);
                endTile.SetActive(false);

                tileManagementController.board[start.x, start.y] = null;
                tileManagementController.board[end.x, end.y] = null;

                startVec2Int = new Vector2Int(-1, -1);
                endVec2Int = new Vector2Int(-1, -1);

                isMatching = true;

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

        //Failed sound output if matching fails
        if (!isMatching)
        {
            controllerManagementSystem.AudioController.VfxSource.Stop();
            controllerManagementSystem.AudioController.PlayVFXSound(wrongSound, 0.5f);

            if (HasMatchingPair() == false)
            {
                controllerManagementSystem.TileShuffleController.ShuffleBoard(tileManagementController.board);
            }
        }
    }

    bool isMatching = false;

    //구현하려다 시간 너무 오래 잡아먹어서 일단 패스(I tried to implement it, but I ate too much time, so I passed it first)
    public bool HasMatchingPair()
    {
        return true;
    }
}
